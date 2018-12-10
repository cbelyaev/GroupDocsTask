using System;
using System.Collections.Generic;
using GroupDocsTask.Model.Calculators;
using GroupDocsTask.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroupDocsTask.Tests
{
    [TestClass]
    public class SalaryCalculatorTests
    {
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

        private readonly List<Employee> _staff = new List<Employee>();

        public SalaryCalculatorTests()
        {

        }

        [TestMethod]
        public void TestConstructor()
        {
            var args1 = new SalaryCalculator.Arguments(SalaryArgs) { BaseRate = 0M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args1));

            var args2 = new SalaryCalculator.Arguments(SalaryArgs) { EmployeeBonusPerYear = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args2));

            var args3 = new SalaryCalculator.Arguments(SalaryArgs) { EmployeeBonusLimit = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args3));

            var args4 = new SalaryCalculator.Arguments(SalaryArgs) { ManagerBonusPerYear = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args4));

            var args5 = new SalaryCalculator.Arguments(SalaryArgs) { ManagerBonusLimit = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args5));

            var args6 = new SalaryCalculator.Arguments(SalaryArgs) { ManagerLeadershipBonus = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args6));

            var args7 = new SalaryCalculator.Arguments(SalaryArgs) { SalesBonusPerYear = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args7));

            var args8 = new SalaryCalculator.Arguments(SalaryArgs) { SalesBonusLimit = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args8));

            var args9 = new SalaryCalculator.Arguments(SalaryArgs) { SalesLeadershipBonus = -1M };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SalaryCalculator(args9));
        }

        [TestMethod]
        public void TestNull()
        {
            var c = new SalaryCalculator(SalaryArgs);
            Assert.ThrowsException<ArgumentNullException>(() => c.CalculateSalary((Employee)null));
            Assert.ThrowsException<ArgumentNullException>(() => c.CalculateSalary((ManagerEmployee)null));
            Assert.ThrowsException<ArgumentNullException>(() => c.CalculateSalary((SalesEmployee)null));
            Assert.ThrowsException<ArgumentNullException>(() => c.CalculateSalary((List<Employee>)null));
        }

        [TestMethod]
        public void TestEmployee()
        {
            var c = new SalaryCalculator(SalaryArgs);

            // 0 years
            var e = new Employee("John Doe", DateTime.Now, null);
            var salary = c.CalculateSalary(e);
            Assert.AreEqual(salary, SalaryArgs.BaseRate);

            // 5 years
            e = new Employee("John Doe", DateTime.Now.AddYears(-5), null);
            salary = c.CalculateSalary(e);
            Assert.AreEqual(salary, SalaryArgs.BaseRate * (1M + 5M * SalaryArgs.EmployeeBonusPerYear));

            // 12 years
            e = new Employee("John Doe", DateTime.Now.AddYears(-12), null);
            salary = c.CalculateSalary(e);
            Assert.AreEqual(salary, SalaryArgs.BaseRate * (1M + SalaryArgs.EmployeeBonusLimit));
        }

        [TestMethod]
        public void TestManager()
        {
            var c = new SalaryCalculator(SalaryArgs);
            var m = new ManagerEmployee("John Doe", DateTime.Now, null);
            var e = new Employee("Jane Doe", DateTime.Now, m);
            var eSalary = c.CalculateSalary(e);
            var actualSalary = c.CalculateSalary(m);
            var expectedSalary = SalaryArgs.BaseRate + SalaryArgs.ManagerLeadershipBonus * eSalary;
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TestMethod]
        public void TestSales()
        {
            var c = new SalaryCalculator(SalaryArgs);
            var s = new SalesEmployee("John Doe", DateTime.Now, null);
            var m = new ManagerEmployee("Jane Doe", DateTime.Now, s);
            var e = new Employee("John Smith", DateTime.Now, m);
            var eSalary = c.CalculateSalary(e);
            var mSsalary = c.CalculateSalary(m);
            var actualSalary = c.CalculateSalary(s);
            var expectedSalary = SalaryArgs.BaseRate + SalaryArgs.SalesLeadershipBonus * (eSalary + mSsalary);
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TestMethod]
        public void TestTotal()
        {
            var c = new SalaryCalculator(SalaryArgs);
            var s = new SalesEmployee("John Doe", DateTime.Now, null);
            var m = new ManagerEmployee("Jane Doe", DateTime.Now, s);
            var e = new Employee("John Smith", DateTime.Now, m);
            var eSalary = c.CalculateSalary(e);
            var mSsalary = c.CalculateSalary(m);
            var sSalary = c.CalculateSalary(s);
            var actualSalary = c.CalculateSalary(new List<Employee> {s, m, e});
            var expectedSalary = eSalary + mSsalary + sSalary;
            Assert.AreEqual(expectedSalary, actualSalary);
        }
    }
}
