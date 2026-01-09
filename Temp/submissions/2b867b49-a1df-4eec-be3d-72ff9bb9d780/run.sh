#!/bin/bash
# Compilăm toate fișierele .c
gcc *.c -o program 2> compile_error.txt

# Verificăm dacă compilarea a reușit
if [ $? -ne 0 ]; then
    exit 1
fi

# Rulăm programul cu input și redirecționăm output-ul
# Folosim timeout 2s ca să nu blocăm API-ul dacă userul pune un loop infinit
timeout 2s ./program < input.txt > output.txt 2> runtime_error.txt