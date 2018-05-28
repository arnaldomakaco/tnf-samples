using Indexing.Application.Services.Interfaces;
using Indexing.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Tnf.Repositories;

namespace Indexing.Application.Services
{
    public class EmployeeAppService : ApplicationService, IEmployeeAppService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeAppService(IRepository<Employee> employeeRepository, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateAsync(Employee.Builder employeeBuilder)
        {
            var employeeToCreate = employeeBuilder.Build();

            if (!Notification.HasNotification())
                employeeToCreate = await _employeeRepository.InsertAndSaveChangesAsync(employeeToCreate);

            return employeeToCreate;
        }

        public Task DeleteAsync(Guid id)
        {
            return _employeeRepository.DeleteAsync(w => w.EmployeeId == id);
        }

        public Task<Employee> GetAsync(Guid id)
        {
            return _employeeRepository.FirstOrDefaultAsync(w => w.EmployeeId == id);
        }

        public async Task<Employee> UpdateAsync(Guid id, Employee.Builder employeeBuilder)
        {
            var employeeToUpdate = employeeBuilder.Build();

            if (!Notification.HasNotification())
                employeeToUpdate = await _employeeRepository.UpdateAsync(employeeToUpdate);

            return employeeToUpdate;
        }
    }
}
