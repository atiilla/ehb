using System;
/*
Excercise
Create the following console application: 

Create a method that fills an array with 4 elements.
Then try to give a value to a fifth element. 
Add code that "increases" the array whenever necessary.
Test this code by adding some additional elements.
Please include the GitHub link to your code with the assignment.
*/
class Solution
{
    static void Main(string[] args)
    {
        int[] array = new int[4];
        FillArray(array);

        array = AddItem(array, 10);
        array = AddItem(array, 20);
        array = AddItem(array, 30); 

        // Print all elements
        Console.WriteLine("Final array elements:");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
    }

    static void FillArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i + 1; // Fill with some values
        }
    }

    static int[] AddItem(int[] array, int newElement)
    {
        int[] newArray = new int[array.Length + 1];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }
        newArray[array.Length] = newElement;

        return newArray;
    }
}