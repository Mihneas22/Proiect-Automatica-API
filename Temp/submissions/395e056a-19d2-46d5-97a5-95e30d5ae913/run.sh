
if [ -f "Makefile" ] || [ -f "makefile" ]; then
    echo "S-a detectat un Makefile. Se compilează cu 'make'..."
    make > /dev/null 2> compile_error.txt
    
    if [ $? -ne 0 ]; then
        echo "Eroare la execuția Makefile-ului."
        exit 1
    fi
    i.
    EXECUTABLE="./program"
else
    echo "Nu s-a găsit Makefile. Se compilează manual cu clang..."
    clang *.c -o program -Wall 2> compile_error.txt
    
    if [ $? -ne 0 ]; then
        echo "Eroare de compilare (clang)."
        exit 1
    fi
    EXECUTABLE="./program"
fi

if [ -f "$EXECUTABLE" ]; then
    timeout 1s $EXECUTABLE < input.txt > output.txt 2> run_error.txt
    EXIT_CODE=$?

    if [ $EXIT_CODE -eq 124 ]; then
        echo "Time Limit Exceeded (TLE)"
        exit 124
    elif [ $EXIT_CODE -ne 0 ]; then
        echo "Runtime Error (RE)"
        exit $EXIT_CODE
    fi
else
    echo "Eroare: Executabilul nu a fost găsit."
    exit 1
fi