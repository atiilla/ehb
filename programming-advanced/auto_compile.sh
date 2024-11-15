#!/bin/bash

if [ -z "$1" ]; then
  echo "Usage: $0 <Java file>"
  exit 1
fi

JAVA_FILE=$1
CLASS_NAME=$(basename "$JAVA_FILE" .java)

ls "$JAVA_FILE" | entr -c sh -c "javac $JAVA_FILE && java $CLASS_NAME"

## USAGE
### chmod +x auto_compile.sh
### ./auto_compile.sh <Java file>