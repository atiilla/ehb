// See https://aka.ms/new-console-template for more information
using Les3;

Project projectReis = new Project("Reis", "op vakantie naar SPanje");
Console.WriteLine(projectReis.ToString());
projectReis.EditDescription("Reis","Op vakantie naar Turkije");
Console.WriteLine(projectReis.ToString());