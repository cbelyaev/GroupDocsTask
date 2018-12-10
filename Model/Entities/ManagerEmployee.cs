using System;

namespace GroupDocsTask.Model.Entities
{
    public class ManagerEmployee: BossEmployee
    {
        public ManagerEmployee(string name, DateTime enrollmentDate)
            : this(name, enrollmentDate, null)
        {
        }

        public ManagerEmployee(string name, DateTime enrollmentDate, BossEmployee boss)
            : base(name, enrollmentDate, boss)
        {
        }
    }
}