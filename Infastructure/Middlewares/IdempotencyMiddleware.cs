using Domain.Entities.Middlewares;
using Infastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Middlewares
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if(context.Request.Method != HttpMethods.Post && context.Request.Method != HttpMethods.Put)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Idempotency-Key", out var key))
            {
                await _next(context);
                return;
            }

            var keyValue = key.ToString();

            var existing = await dbContext.IdempotencyEntity
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Key == keyValue);

            if (existing != null)
            {
                context.Response.StatusCode = existing.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(existing.ResponseBody);
                return;
            }

            var originalBody = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Position = 0;
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            try
            {
                dbContext.IdempotencyEntity.Add(new IdempotencyRecord
                {
                    Id = Guid.NewGuid(),
                    Key = keyValue,
                    StatusCode = context.Response.StatusCode,
                    ResponseBody = responseBody,
                    CreatedAt = DateTime.UtcNow
                });

                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                var stored = await dbContext.IdempotencyEntity
                    .AsNoTracking()
                    .FirstAsync(x => x.Key == keyValue);

                context.Response.StatusCode = stored.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(stored.ResponseBody);
                return;
            }

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    }
}
