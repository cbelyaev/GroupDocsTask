using System;

namespace GroupDocsTask.Model.Entities
{
    public class SalesEmployee: BossEmployee
    {
        public SalesEmployee(string name, DateTime enrollmentDate)
            : this(name, enrollmentDate, null)
        {
        }

        public SalesEmployee(string name, DateTime enrollmentDate, BossEmployee boss)
            : base(name, enrollmentDate, boss)
        {
        }
    }
}