using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Indexing.Domain.Entities.Specifications
{
    public class EmployeeShouldHaveNameSpecification : Specification<Employee>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Employee.Error.EmployeeShouldHaveName;

        public override Expression<Func<Employee, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
