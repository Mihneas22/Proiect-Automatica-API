#include <stdio.h>

// Funcție pentru calcularea sumei
int suma(int a, int b) {
    return a + b;
}

// Funcție pentru calcularea mediei aritmetice
float media(int a, int b) {
    return (float)(a + b) / 2;
}

int main() {
    int x, y;

    // Citirea celor două numere întregi
    if (scanf("%d %d", &x, &y) == 2) {
        // Afișarea rezultatelor cu formatare
        printf("Suma: %d Media: %.2f", suma(x, y), media(x, y));
    }

    return 0;
}