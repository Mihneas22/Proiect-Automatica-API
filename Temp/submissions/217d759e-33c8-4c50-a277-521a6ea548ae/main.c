#include <stdio.h>

int main() {
    int n, numar;
    long long suma = 0; // Folosim long long pentru a evita overflow-ul la sume mari
e
    if (scanf("%d", &n) != 1) {
        return 0;
    }

    for (int i = 0; i < n; i++) {
        if (scanf("%d", &numar) == 1) {
            if (numar % 2 == 0) {
                suma += numar;
            }
        }
    }
    printf("%lld\n", suma);

    return 0;
}