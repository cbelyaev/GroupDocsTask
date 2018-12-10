using System;
using System.Collections.Generic;
using System.Linq;
using GroupDocsTask.Model;
using GroupDocsTask.Model.Calculators;
using GroupDocsTask.Model.Entities;

namespace GroupDocsTask.ConsoleApp
{
    internal class Program
    {
        private static readonly List<Employee> Staff = new List<Employee>();

        private static readonly SalaryCalculator.Arguments SalaryArgs = new SalaryCalculator.Arguments
        {
            BaseRate = 20000M,
            EmployeeBonusPerYear = 0.03M,
            EmployeeBonusLimit = 0.3M,
            ManagerBonusPerYear = 0.05M,
            ManagerBonusLimit = 0.4M,
            ManagerLeadershipBonus = 0.005M,
            SalesBonusPerYear = 0.01M,
            SalesBonusLimit = 0.35M,
            SalesLeadershipBonus = 0.003M
        };

        private static readonly SalaryCalculator Calculator = new SalaryCalculator(SalaryArgs);

        private static void Main(string[] args)
        {
            Hire();
            Dump();
        }

        private static void Hire()
        {
            var r = new Random();
            var start = Employee.CompanyFoundationDate;
            var totalDays = (DateTime.Now - start).TotalDays;

            var president = new SalesEmployee("John Smith", start);
            Staff.Add(president);

            var productionVisePresident = new ManagerEmployee("Jack Doe", start, president);
            Staff.Add(productionVisePresident);

            for (var i = 0; i < 5; i++)
            {
                var pmName = $"pm{i:D4}";
                var pmDate = start.AddDays(r.NextDouble() * totalDays);
                var pm = new ManagerEmployee(pmName, pmDate, productionVisePresident);
                Staff.Add(pm);
                for (var j = 0; j < 5; j++)
                {
                    var peName = $"pe{i:D2}{j:D2}";
                    var peDate = start.AddDays(r.NextDouble() * totalDays);
                    Staff.Add(new Employee(peName, peDate, pm));
                }
            }

            var salesVicePresident = new SalesEmployee("Mary Good", start, president);
            Staff.Add(salesVicePresident);

            for (var i = 0; i < 5; i++)
            {
                var smName = $"sm{i:D4}";
                var smDate = start.AddDays(r.NextDouble() * totalDays);
                var sm = new ManagerEmployee(smName, smDate, salesVicePresident);
                Staff.Add(sm);
                for (var j = 0; j < 5; j++)
                {
                    var seName = $"se{i:D2}{j:D2}";
                    var seDate = start.AddDays(r.NextDouble() * totalDays);
                    Staff.Add(new Employee(seName, seDate, sm));
                }
            }
        }

        private static void Dump()
        {
            foreach (var boss in Staff.Where(e => e.Boss == null))
                Dump(0, boss);
            Console.WriteLine();
            Console.WriteLine($"TOTAL: {Calculator.CalculateSalary(Staff):F2}");
        }

        private static void Dump(int level, Employee e)
        {
            Console.Write(new string(' ', level));
            Console.Write(e.Name);
            Console.Write($"\t{e.EnrollmentDate:d}");
            Console.WriteLine($"\t{Calculator.CalculateSalary(e):F2}");
            if (!(e is BossEmployee boss))
                return;
            foreach (var subordinate in boss.Subordinates)
                Dump(level + 1, subordinate);
        }
    }
}