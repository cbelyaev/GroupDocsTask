using System;

namespace GroupDocsTask.Model.Entities
{
    public class Employee
    {
        public static readonly DateTime CompanyFoundationDate = new DateTime(2000, 5, 7);

        public Employee(string name, DateTime enrollmentDate, BossEmployee boss)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;

            if (enrollmentDate < CompanyFoundationDate || enrollmentDate > DateTime.Now)
                throw new ArgumentException(nameof(enrollmentDate));
            EnrollmentDate = enrollmentDate;

            // boss CAN be null
            (Boss = boss)?.AddSubordinate(this);
        }

        public string Name { get; }
        public DateTime EnrollmentDate { get; }
        public BossEmployee Boss { get; }

        public int ExperienceYears => (int)((DateTime.Now - EnrollmentDate).TotalDays / 365.0);
    }
}