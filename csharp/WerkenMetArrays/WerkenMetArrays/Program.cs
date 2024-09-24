/*
Excercise
Create the following console application: 

Create a method that fills an array with 4 elements.
Then try to give a value to a fifth element. 
Add code that "increases" the array whenever necessary.
Test this code by adding some additional elements.
Please include the GitHub link to your code with the assignment.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;


ArrayList arrayList = new ArrayList();

bool CheckItemIsValid<T>(T item)
{
    if (item == null)
    {
        return false;
    }

    return true;
}

void AddItem<T> (T item)
{
    //arrayList.Add (item);
    if (!CheckItemIsValid(item)) {
        Console.WriteLine("Item Can not be null: {0}",item.ToString());
    }
    

}

AddItem<string>("This is test for string");
AddItem<int> (-1);
AddItem<float> (-6);
AddItem<double> (-4);
AddItem<bool>(true);

//int? test1 = 10;
//test1 = null;

//AddItem<int?>(test1); // This will throw an exception


Console.WriteLine("Total Items: {0}", arrayList.Count);

for (int i =0; i < arrayList.Count; i++)
{
    Console.WriteLine("Array Item Index: {0}" +
        "Array Item Value: {1}", i, arrayList[i]);
}


int? test = 10;
test = null;
int x = 0;

// 1
if (test.HasValue)
{
    x = 7 + (int)test;
}
else
{
    x = 7;
}


// 2 
x = 7+ (test.HasValue ? test.Value : 0);

// 3
x = 7 + test ?? 0;

// 4
x = 7 + test.GetValueOrDefault();



Console.WriteLine(test.GetValueOrDefault());
