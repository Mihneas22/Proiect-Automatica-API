main.c
#include <stdio.h>

int main() {
    int a, b;
    int suma;
    float media;

    // Citim cele doua numere
    if (scanf("%d %d", &a, &b) == 2) {
        // Calculam suma
        suma = a + b;
        
        // Calculam media (folosim 2.0 pentru a forța rezultatul sa fie float)
        media = (float)suma / 2;

        // Afisam rezultatul conform formatului cerut
        printf("Suma: %d Media: %.2f", suma, media);
    }

    return 0;
}