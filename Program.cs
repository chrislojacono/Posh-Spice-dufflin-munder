﻿using System;
using System.Collections.Generic;
using DufflinMunder.Employees;

namespace DufflinMunder
{
    class Program
    {
        static void Main(string[] args)
        {
            var Jim = new SalesEmployee();
            Jim.EmployeeName = "Jim Halpert";
            var Dwight = new SalesEmployee();
            Dwight.EmployeeName = "Dwight Schrute";
            var Phyllis = new SalesEmployee { EmployeeName = "Phyllis Leaf" };

            var SalesEmployees = new List<SalesEmployee>
            {
                Jim,
                Dwight,
                Phyllis,
            };
            var initialSelection = "";
            do
            {
                Console.WriteLine($@"
                        Welcome to Dufflin/Munder Cardboard Co. 
                        Sales Portal!

                        1.Enter Sales
                        2.Generate Report For Accountant
                        3.Add New Sales Employee
                        4.Find a Sale
                        5.Exit 
                        ------------------");

                initialSelection = Console.ReadLine();
                switch (initialSelection)
                {
                    case "1":
                        Console.Clear();

                        Console.WriteLine("Which person are you?");

                        var counter = 1;

                        foreach (var employee in SalesEmployees)
                        {
                            Console.WriteLine($"{counter}. {employee.EmployeeName}");
                            counter++;
                        }

                        var employeeInput = Console.ReadLine();
                        var chosenEmployee = SalesEmployees[(Int32.Parse(employeeInput) - 1)];

                        Console.Clear();

                        Console.WriteLine($"Hi, {chosenEmployee.EmployeeName}!! ");
                        Console.WriteLine();
                        Console.WriteLine($"Sales Agent: {chosenEmployee.EmployeeName} ");

                        Console.Write("Client: ");
                        string clientName = Console.ReadLine();

                        Console.Write("ClientId: ");
                        var clientId = Console.ReadLine();  

                        Console.Write("Sale: $");
                        var salesTotal = Console.ReadLine();
                        
                        StartOfRecurring:
                        Console.Write("Recurring (ex: Monthly, Annually, Quarterly): ");
                        var recurringAmount = Console.ReadLine();
                        Recurring passedInput = Recurring.None;
                        if(Enum.IsDefined(typeof(Recurring), recurringAmount))
                        {
                            passedInput = (Recurring)Enum.Parse(typeof(Recurring), recurringAmount);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Input! Try Monthly, Annually, Quarterly, or Weekly.");
                            goto StartOfRecurring;
                        }

                        Console.Write("Time Frame: ");
                        var timeFrame = Console.ReadLine();

                        chosenEmployee.Sales.Add(Int32.Parse(clientId), new Sales(chosenEmployee.EmployeeName, clientName, Int32.Parse(clientId), Int32.Parse(salesTotal), passedInput, timeFrame));

                        Console.Clear();
                        Console.WriteLine($"Sale Input Recieved! Good work {chosenEmployee.EmployeeName}");


                        break;
                    case "2":
                        Console.WriteLine("case 2");
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Please enter new saleperson's name:");
                        var newSalesperson = Console.ReadLine();
                        var newPerson = new SalesEmployee { EmployeeName = newSalesperson};
                        SalesEmployees.Add(newPerson);
                        Console.Clear();
                        foreach (var emp in SalesEmployees)
                        {
                            Console.WriteLine(emp.EmployeeName);
                        }
                        
                        break;
                    case "4":
                        Console.WriteLine("case 4");
                        break;
                    default:
                        Console.WriteLine("bu-bye");
                        break;
                }

            } while (initialSelection != "5");

        }
    }
}
