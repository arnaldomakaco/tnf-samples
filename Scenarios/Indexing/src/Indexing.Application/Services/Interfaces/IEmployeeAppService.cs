using Indexing.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;

namespace Indexing.Application.Services.Interfaces
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<Employee> GetAsync(Guid id);
        Task<Employee> CreateAsync(Employee.Builder employeeBuilder);
        Task<Employee> UpdateAsync(Guid id, Employee.Builder employeeBuilder);
        Task DeleteAsync(Guid id);
    }
}
