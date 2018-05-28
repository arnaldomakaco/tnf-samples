using Indexing.Application.Services.Interfaces;
using Indexing.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Indexing;

namespace Indexing.Web.Controllers
{
    [Route(WebConstants.EmployeeRouteName)]
    public class EmployeeController : TnfController
    {
        private readonly IEmployeeAppService _appService;
        private readonly IIndexingProvider _indexingProvider;

        public EmployeeController(IEmployeeAppService appService, IIndexingProvider indexingProvider)
        {
            _appService = appService;
            _indexingProvider = indexingProvider;
        }

        /// <summary>
        /// Search employee by age
        /// </summary>
        [HttpGet("/age")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> SearchForAge([FromQuery]int age)
        {
            var response = await _indexingProvider.SearchAsync<Employee>(q =>
                q.Query(i =>
                    i.Match(age.ToString(), f => f.Age, true)
                )
            );

            return CreateResponseOnGetAll(response);
        }

        /// <summary>
        /// Search employee by age
        /// </summary>
        [HttpGet("/term")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> SearchForTerm([FromQuery]string term)
        {
            var response = await _indexingProvider.SearchAsync<Employee>(term);

            return CreateResponseOnGetAll(response);
        }

        /// <summary>
        /// Get employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee requested</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _appService.GetAsync(id);

            return CreateResponseOnGet(response);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid employee");

            var builder = Employee.Create(Notification, employee);

            employee = await _appService.CreateAsync(builder);

            return CreateResponseOnPost(employee);
        }

        /// <summary>
        /// Update a employee
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid employee");

            var builder = Employee.Create(Notification, employee);

            employee = await _appService.UpdateAsync(id, builder);

            return CreateResponseOnPut(employee);
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Customer id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
