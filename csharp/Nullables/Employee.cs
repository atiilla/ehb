using System;
/*
A company with three departments (Sales, Support and Administration) gives its employees a bonus of 2% per year in their salary,
if the employee has worked in at least 2 of the three departments.

For each year there must be a full year of work (not round up).

Ask for an employee 

the name of the employee
how long he has worked for the three departments (if he has not worked for a department, you will not enter any value for that department).
Calculate how large his bonus percentage is.
*/
namespace Nullables
{
    internal class Employee
    {
        // Departments
        enum Departments
        {
            Sales,
            Support,
            Administration
        }

        // Employee's information
        public string Name { get; set; }
        public float SalesDuration { get; set; } = 0;
        public float SupportDuration { get; set; } = 0;
        public float AdminDuration { get; set; } = 0;

        public float CalculateBonusPercentage()
        {
            int departmentCount = 0;

            // if the employee has worked in at least 2 of the three departments.
            if (SalesDuration >= 1)
                departmentCount++;
            if (SupportDuration >= 1)
                departmentCount++;
            if (AdminDuration >= 1)
                departmentCount++;

            // if the employee has worked in at least 2 of the three departments and a full year of work
            if (departmentCount >= 2)
            {

                float totalDays = SalesDuration + SupportDuration + AdminDuration;
                if(totalDays >= 200) {
                    return departmentCount * 2;
                }
            }

            return 0; // No bonus if not worked in 2 departments
        }

        public void DisplayEmployeeDetails()
        {
            float bonusPercentage = CalculateBonusPercentage();
            Console.WriteLine($"Employee: {Name}");
            Console.WriteLine($"Sales Duration: {SalesDuration} years");
            Console.WriteLine($"Support Duration: {SupportDuration} years");
            Console.WriteLine($"Administration Duration: {AdminDuration} years");
            Console.WriteLine($"Bonus Percentage: {bonusPercentage}%");
        }
    }
}
