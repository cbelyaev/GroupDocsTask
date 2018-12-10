using System;
using System.Collections.Generic;
using System.Linq;
using GroupDocsTask.Model.Entities;

namespace GroupDocsTask.Model.Calculators
{
    public sealed class SalaryCalculator
    {
        private readonly Arguments _arguments;

        public SalaryCalculator(Arguments args)
        {
            _arguments = new Arguments(args);
            
            if (_arguments.BaseRate <= 0M)
                throw new ArgumentOutOfRangeException(nameof(args.BaseRate));

            if (_arguments.EmployeeBonusPerYear < 0M)
               throw new ArgumentOutOfRangeException(nameof(args.EmployeeBonusPerYear));

            if (_arguments.EmployeeBonusLimit < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.EmployeeBonusLimit));

            if (_arguments.ManagerBonusPerYear < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.ManagerBonusPerYear));

            if (_arguments.ManagerBonusLimit < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.ManagerBonusLimit));

            if (_arguments.ManagerLeadershipBonus < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.ManagerLeadershipBonus));

            if (_arguments.SalesBonusPerYear < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.SalesBonusPerYear));

            if (_arguments.SalesBonusLimit < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.SalesBonusLimit));

            if (_arguments.SalesLeadershipBonus < 0M)
                throw new ArgumentOutOfRangeException(nameof(args.SalesLeadershipBonus));

        }

        public decimal CalculateSalary(List<Employee> employees)
        {
            if (employees == null)
                throw new ArgumentNullException(nameof(employees));

            return employees.Sum(CalculateSalary);
        }

        public decimal CalculateSalary(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            switch (employee)
            {
                case ManagerEmployee managerEmployee:
                    return CalculateSalary(managerEmployee);
                case SalesEmployee salesEmployee:
                    return CalculateSalary(salesEmployee);
                default:
                    var longevityBonus = _arguments.BaseRate * GetLongevityBonus(employee.ExperienceYears,
                                             _arguments.EmployeeBonusPerYear,
                                             _arguments.EmployeeBonusLimit);

                    return _arguments.BaseRate + longevityBonus;
            }
        }

        private decimal CalculateSalary(ManagerEmployee manager)
        {
            var longevityBonus = _arguments.BaseRate * GetLongevityBonus(manager.ExperienceYears,
                                     _arguments.ManagerBonusPerYear,
                                     _arguments.ManagerBonusLimit);

            var leadershipBonus = manager.Subordinates.Sum(CalculateSalary)
                                  * _arguments.ManagerLeadershipBonus;

            return _arguments.BaseRate + longevityBonus + leadershipBonus;
        }

        private decimal CalculateSalary(SalesEmployee sales)
        {
            var longevityBonus = _arguments.BaseRate * GetLongevityBonus(sales.ExperienceYears,
                                     _arguments.ManagerBonusPerYear,
                                     _arguments.ManagerBonusLimit);

            var leadershipBonus = sales.AllSubordinates.Sum(CalculateSalary)
                                  * _arguments.SalesLeadershipBonus;

            return _arguments.BaseRate + longevityBonus + leadershipBonus;
        }

        internal static decimal GetLongevityBonus(int years, decimal bonusPerYear, decimal maxBonux)
        {
            var bonus = years * bonusPerYear;
            return bonus > maxBonux ? maxBonux : bonus;
        }

        public sealed class Arguments
        {
            public decimal EmployeeBonusPerYear { get; set; }
            public decimal EmployeeBonusLimit { get; set; }
            public decimal ManagerBonusPerYear { get; set; }
            public decimal ManagerBonusLimit { get; set; }
            public decimal ManagerLeadershipBonus { get; set; }
            public decimal SalesBonusPerYear { get; set; }
            public decimal SalesBonusLimit { get; set; }
            public decimal SalesLeadershipBonus { get; set; }
            public decimal BaseRate { get; set; }

            public Arguments()
            {
            }

            public Arguments(Arguments args)
            {
                if (args == null)
                    throw new ArgumentNullException(nameof(args));

                EmployeeBonusPerYear = args.EmployeeBonusPerYear;
                EmployeeBonusLimit = args.EmployeeBonusLimit;
                ManagerBonusPerYear = args.ManagerBonusPerYear;
                ManagerBonusLimit = args.ManagerBonusLimit;
                ManagerLeadershipBonus = args.ManagerLeadershipBonus;
                SalesBonusPerYear = args.SalesBonusPerYear;
                SalesBonusLimit = args.SalesBonusLimit;
                SalesLeadershipBonus = args.SalesLeadershipBonus;
                BaseRate = args.BaseRate;
            }
        }
    }
}