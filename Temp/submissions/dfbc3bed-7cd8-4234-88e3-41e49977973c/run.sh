#!/bin/bash
gcc *.c -o program 2> compile_error.txt
if [ $? -ne 0 ]; then
    exit 1
fi
./program < input.txt > output.txt