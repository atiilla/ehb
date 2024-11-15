#!/bin/bash

if [ -z "$1" ]; then
  echo "Usage: $0 <C file>"
  exit 1
fi

C_FILE=$1
OUT_FILE=$(basename "$C_FILE" .c)

ls "$C_FILE" | entr -c sh -c "gcc -o $OUT_FILE $C_FILE && ./$OUT_FILE"

## USAGE
### chmod +x auto_compile.sh
### ./auto_compile.sh <C file>
