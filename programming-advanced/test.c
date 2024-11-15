#include <stdio.h>

int main() {
    // Print "Hello, World!"
    printf("Hello, World!\n");

    // Declare a variable for the user's name
    char name[50];

    // Ask for the user's name
    printf("Enter your name: ");
    scanf("%49s", name);  // Read the user's name (up to 49 characters)

    // Print a personalized greeting
    printf("Hello, %s! Welcome to the C program.\n", name);

    return 0;
}
