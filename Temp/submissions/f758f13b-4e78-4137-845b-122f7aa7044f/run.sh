#!/bin/bash

# Compilează codul C
gcc main.c -O2 -o main 2> compile_error.txt
if [ $? -ne 0 ]; then
  exit 1
fi

# Rulează programul cu input
./main < input.txt > output.txt

