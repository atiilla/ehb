// Create a new employee
using Nullables;

Employee employee = new Employee();

Console.Write("Enter employee name: ");
employee.Name = Console.ReadLine();

Console.Write("Enter days worked in the year in Sales (if none, press Enter): ");
string salesInput = Console.ReadLine();
employee.SalesDuration = string.IsNullOrWhiteSpace(salesInput) ? 0 : float.Parse(salesInput);

Console.Write("Enter days worked in the year in Support (if none, press Enter): ");
string supportInput = Console.ReadLine();
employee.SupportDuration = string.IsNullOrWhiteSpace(supportInput) ? 0 : float.Parse(supportInput);

Console.Write("Enter days worked in the year in Administration (if none, press Enter): ");
string adminInput = Console.ReadLine();
employee.AdminDuration = string.IsNullOrWhiteSpace(adminInput) ? 0 : float.Parse(adminInput);

employee.DisplayEmployeeDetails();