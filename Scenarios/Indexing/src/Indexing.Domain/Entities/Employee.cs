using System;
using Tnf.Notifications;

namespace Indexing.Domain.Entities
{
    public partial class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? Salary { get; set; }
        public decimal? Score { get; set; }
        public int? Age { get; set; }
        public Address FullAddress { get; set; }

        public static Builder Create(INotificationHandler handler)
            => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Employee instance)
            => new Builder(handler, instance);
    }
}
