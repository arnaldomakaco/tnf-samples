using Indexing.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace Indexing.Domain.Entities
{
    public partial class Employee
    {
        public class Builder : Builder<Employee>
        {
            public Builder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public Builder(INotificationHandler notificationHandler, Employee instance)
                : base(notificationHandler, instance)
            {
            }

            public Builder WithId(Guid id)
            {
                Instance.EmployeeId = id;
                return this;
            }

            public Builder WithName(string name)
            {
                Instance.Name = name;
                return this;
            }

            public Builder WithDateOfBirth(DateTime dateOfBirth)
            {
                Instance.DateOfBirth = dateOfBirth;
                Instance.Age = DateTime.Today.Year - dateOfBirth.Year;
                return this;
            }

            public Builder WithSalary(double salary)
            {
                Instance.Salary = salary;
                return this;
            }

            public Builder WithScore(decimal score)
            {
                Instance.Score = score;
                return this;
            }

            public Builder WithAddress(string street, string zipCode)
            {
                Instance.FullAddress = new Address(street, zipCode);
                return this;
            }

            protected override void Specifications()
            {
                AddSpecification<EmployeeShouldHaveNameSpecification>();
            }
        }
    }
}
