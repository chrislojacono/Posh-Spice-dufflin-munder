using System;
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
            var Benji = new SalesEmployee { EmployeeName = "Benji Palmer" };

            var Oscar = new AccountingEmployee();
            Oscar.EmployeeName = "Oscar";
            var Kevin = new AccountingEmployee();
            Kevin.EmployeeName = "Kevin";
            var Holly = new SalesEmployee { EmployeeName = "Holly Flax" };

            var SalesEmployees = new List<SalesEmployee>
            {
                Jim,
                Dwight,
                Phyllis,
                Holly,
                Benji,
            };
            var Accountants = new List<AccountingEmployee>
            {
                Oscar,
                Kevin
            };
            var dummySale1 = new Sale(Phyllis.EmployeeName, "Taco Hell", 2433, 52000, Recurring.Annually, "5 months");
            var dummySale2 = new Sale(Dwight.EmployeeName, "Bed, Math and Beyond", 0444, 29340, Recurring.Monthly, "10 months");
            var dummySale3 = new Sale(Jim.EmployeeName, "Catalina Wine Mixer", 4444, 125000, Recurring.Annually, "1 month");
            var dummySale4 = new Sale(Phyllis.EmployeeName, "Vance Refridgeration", 5252, 44500, Recurring.Weekly, "1 month");

            Phyllis.SalesDictionary.Add(dummySale1.ClientId, dummySale1);
            Phyllis.SalesDictionary.Add(dummySale4.ClientId, dummySale4);
            Dwight.SalesDictionary.Add(dummySale2.ClientId, dummySale2);
            Jim.SalesDictionary.Add(dummySale3.ClientId, dummySale3);

            var scrantonOffice = new Office("Scranton Office");
            var nashuaOffice = new Office("Nashua Office");

            scrantonOffice.OfficeEmployees.Add(Jim);
            scrantonOffice.OfficeEmployees.Add(Dwight);
            scrantonOffice.OfficeEmployees.Add(Phyllis);

            nashuaOffice.OfficeEmployees.Add(Benji);
            nashuaOffice.OfficeEmployees.Add(Holly);

            var listOfOffices = new List<Office> { scrantonOffice, nashuaOffice };


            var initialSelection = "";
            var chosenOffice = new Office("holder");
            do
            {
                BeginningOfOfficeChoice:
                Console.WriteLine(@"
                        1. Choose an Office.
                        2. Create a new Office.");
                var officeSelection = Console.ReadLine();

                switch (officeSelection)
                {
                    case "1":
                        var counter = 1;

                        foreach (var office in listOfOffices)
                        {
                            Console.WriteLine($"{counter}. {office.Name}");
                            counter++;
                        }

                        var officeInput = Console.ReadLine();
                        chosenOffice = listOfOffices[(Int32.Parse(officeInput) - 1)];

                        Console.Clear();
                        Console.WriteLine($"You chose {chosenOffice.Name}");
                        break;
                    case "2":
                        Console.WriteLine("Please enter new Office name:");
                        var newOfficeName = Console.ReadLine();
                        var newOffice = new Office(newOfficeName);
                        listOfOffices.Add(newOffice);
                        Console.Clear();
                        Console.WriteLine($"Thanks for adding {newOfficeName}");
                        goto BeginningOfOfficeChoice;
                        
                }

                BeginningOfMainMenu:
                Console.WriteLine($@"
                        Welcome to Dufflin/Munder Cardboard Co. 
                        Sales Portal!

                        1.Enter Sales
                        2.Generate Report For Accountant
                        3.Add New Sales Employee
                        4.Find a Sale
                        5.Generate A Report for whole Office
                        6.Go To Office Menu
                        7.Exit 
                        ------------------");

                initialSelection = Console.ReadLine();
                switch (initialSelection)
                {
                    case "1":
                        Console.Clear();
                        var dictionaryOfAllSales = new Dictionary<int, Sale>();
                        foreach (var employee in SalesEmployees)
                        {

                            foreach (var sale in employee.SalesDictionary)
                            {
                                dictionaryOfAllSales.Add(sale.Key, sale.Value);
                            }
                        }
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

                        enterNewClientId:
                        Console.Write("ClientId: ");
                        var clientId = Console.ReadLine();

                        if (dictionaryOfAllSales.ContainsKey(Int32.Parse(clientId)))
                        {
                            Console.WriteLine("Client Id already exists");
                            goto enterNewClientId;
                        }

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

                        chosenEmployee.SalesDictionary.Add(Int32.Parse(clientId), new Sale(chosenEmployee.EmployeeName, clientName, Int32.Parse(clientId), Int32.Parse(salesTotal), passedInput, timeFrame));

                        Console.Clear();
                        Console.WriteLine($"Sale Input Recieved! Good work {chosenEmployee.EmployeeName}");


                       goto BeginningOfMainMenu;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Which accountant are you?");
                        var accountantCounter = 1;
                        foreach (var employee in Accountants)
                        {
                            Console.WriteLine($"{accountantCounter}. {employee.EmployeeName}");
                            accountantCounter++;
                        }
                        var userInput = Console.ReadLine();
                        Console.Clear();
                        var selectedAccountant = Accountants[(Int32.Parse(userInput) - 1)];
                        Console.WriteLine($"Monthly Sales Report For: {selectedAccountant.EmployeeName}");
                        Console.WriteLine();
                        foreach (var employee in SalesEmployees)
                        {
                            Console.WriteLine($"    {employee.EmployeeName}");
                            var total = 0;
                            Console.WriteLine(@"        Clients:");
                            foreach (var (Key, Value) in employee.SalesDictionary)
                            {
                                Console.WriteLine($"        {Value.Client}");
                                total += Value.SalesTotal;
                            }
                            Console.WriteLine($"    Total: ${total}");
                            Console.WriteLine();
                        }
                        goto BeginningOfMainMenu;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Please enter new saleperson's name:");
                        var newSalesperson = Console.ReadLine();
                        var newPerson = new SalesEmployee { EmployeeName = newSalesperson};
                        SalesEmployees.Add(newPerson);

                        Console.WriteLine("Which Office do they belong to?");
                        var OfficeCounter = 1;

                        foreach (var office in listOfOffices)
                        {
                            Console.WriteLine($"{OfficeCounter}. {office.Name}");
                            OfficeCounter++;
                        }

                        var officeInput = Console.ReadLine();
                        chosenOffice = listOfOffices[(Int32.Parse(officeInput) - 1)];
                        chosenOffice.OfficeEmployees.Add(newPerson);

                        Console.Clear();
                        goto BeginningOfMainMenu;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Please enter the client ID number:");
                        var clientNumber = Console.ReadLine();
                        var dictionaryOfAllOfTheSales = new Dictionary<int, Sale>();
                        foreach (var employee in SalesEmployees)
                        {

                            foreach (var sale in employee.SalesDictionary)
                            {
                                dictionaryOfAllOfTheSales.Add(sale.Key, sale.Value);
                            }
                        }
                        Console.Clear();
                        Console.WriteLine($"ClientId: {dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].ClientId}");
                        Console.WriteLine($"Client: {dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].Client}");
                        Console.WriteLine($"Salesperson: {dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].SalesPerson}");
                        Console.WriteLine($"Sales Total: ${dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].SalesTotal}");
                        Console.WriteLine($"Recurring: {dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].Recurring}");
                        Console.WriteLine($"Timeframe: {dictionaryOfAllOfTheSales[Int32.Parse(clientNumber)].TimeFrame}");
                        goto BeginningOfMainMenu;
                    case "5":
                        Console.WriteLine($"Monthly Sales Report For: {chosenOffice.Name}");
                        Console.WriteLine();
                        foreach (var employee in chosenOffice.OfficeEmployees)
                        {
                            Console.WriteLine($"    {employee.EmployeeName}");
                            var total = 0;
                            Console.WriteLine(@"        Clients:");
                            foreach (var (Key, Value) in employee.SalesDictionary)
                            {
                                Console.WriteLine($"        {Value.Client}");
                                total += Value.SalesTotal;
                            }
                            Console.WriteLine($"    Total: ${total}");
                            Console.WriteLine();
                        }
                       goto BeginningOfMainMenu;
                    case "6":
                        goto BeginningOfOfficeChoice;

                    default:
                        Console.WriteLine($"Thank you for visiting {chosenOffice.Name}");
                        break;
                }

            } while (initialSelection != "7");

        }
    }
}
