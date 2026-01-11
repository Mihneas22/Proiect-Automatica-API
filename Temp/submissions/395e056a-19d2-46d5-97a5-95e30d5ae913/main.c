#include <stdio.h>
#include <stdlib.h>

/**
 * Problema: Suma numerelor pare dintr-un sir.
 * Input: Un numar n urmat de n elemente.
 * Output: Suma elementelor x unde x % 2 == 0.
 */

int main() {
    int n;
    long long suma = 0;

    // Citim numarul de elemente
    if (scanf("%d", &n) != 1) {
        return 1;
    }

    if (n <= 0) {
        printf("0\n");
        return 0;
    }

    // Alocam memorie pentru sir
    int *tablou = (int *)malloc(n * sizeof(int));
    if (tablou == NULL) return 1;

    // Citim elementele si calculam suma pentru cele pare
    for (int i = 0; i < n; i++) {
        if (scanf("%d", &tablou[i]) == 1) {
            if (tablou[i] % 2 == 0) {
                suma += tablou[i];
            }
        }
    }

    printf("%lld\n", suma);

    // Eliberam memoria
    free(tablou);

    return 0;
}