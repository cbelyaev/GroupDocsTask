using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupDocsTask.Model.Entities
{
    public abstract class BossEmployee: Employee
    {
        private readonly List<Employee> _subordinates = new List<Employee>();

        protected BossEmployee(string name, DateTime enrollmentDate, BossEmployee boss)
            : base(name, enrollmentDate, boss)
        {
        }

        public IEnumerable<Employee> Subordinates
            => _subordinates.AsEnumerable();

        public IEnumerable<Employee> AllSubordinates
            => _subordinates.Union(_subordinates.OfType<BossEmployee>().SelectMany(b => b.AllSubordinates));

        internal void AddSubordinate(Employee subordinate)
        {
            if (subordinate == null)
                throw new ArgumentNullException(nameof(subordinate));

            if (subordinate.Boss != this)
                throw new ArgumentException("Invalid boss for subordinate", nameof(subordinate));

            _subordinates.Add(subordinate);
        }
    }
}