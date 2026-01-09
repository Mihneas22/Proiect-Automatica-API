#include <stdio.h>

// Funcție pentru calculul sumei
int suma(int a, int b) {
    return a + b;
}

// Funcție pentru calculul mediei aritmetice
float media(int a, int b) {
    return (a + b) / 2.0;
}

int main() {
    int x, y;
    int s;
    float m;

    printf("Introduceti doua numere intregi: ");
    scanf("%d %d", &x, &y);

    s = suma(x, y);
    m = media(x, y);

    printf("Suma = %d", s);
    printf("Media aritmetica = %.2f", m);

    return 0;
}
