using System;
using GroupDocsTask.Model.Calculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroupDocsTask.Tests
{
    [TestClass]
    public class SalaryArgumentsTests
    {
        [TestMethod]
        public void TestCopyConstructorNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SalaryCalculator.Arguments(null));
        }

        [TestMethod]
        public void TestCopyConstructor()
        {
            var orig = new SalaryCalculator.Arguments
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

            var copy = new SalaryCalculator.Arguments(orig);
            Assert.AreEqual(copy.BaseRate, orig.BaseRate);
            Assert.AreEqual(copy.EmployeeBonusPerYear, orig.EmployeeBonusPerYear);
            Assert.AreEqual(copy.EmployeeBonusLimit, orig.EmployeeBonusLimit);
            Assert.AreEqual(copy.ManagerBonusPerYear, orig.ManagerBonusPerYear);
            Assert.AreEqual(copy.ManagerBonusLimit, orig.ManagerBonusLimit);
            Assert.AreEqual(copy.ManagerLeadershipBonus, orig.ManagerLeadershipBonus);
            Assert.AreEqual(copy.SalesBonusPerYear, orig.SalesBonusPerYear);
            Assert.AreEqual(copy.SalesBonusLimit, orig.SalesBonusLimit);
            Assert.AreEqual(copy.SalesLeadershipBonus, orig.SalesLeadershipBonus);
        }
    }
}