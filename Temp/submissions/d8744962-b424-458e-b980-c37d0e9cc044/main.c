#include <stdio.h>
#include <math.h>

int main() {
    int a, b, c;
    float p, aria;

    scanf("%d %d %d", &a, &b, &c);

    // Verificare triunghi valid
    if (a + b > c && a + c > b && b + c > a) {
        p = (a + b + c) / 2.0;
        aria = sqrt(p * (p - a) * (p - b) * (p - c));
        printf("Aria: %.2f", aria);
    } else {
        printf("Nu este un triunghi valid");
    }

    return 0;
}
