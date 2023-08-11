using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Employee_Services.EmployeeManipulate;
using DAL;
using Buisness_Entities.Employee;
using System.Net;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController :ApiController
    {
        //public readonly IEmployeeService employeeService = new EmployeeService;
        public readonly IEmployeeService employeeService = new EmployeeService();
        public EmployeeController()
        {
            //employeeService = _employeeService;
        }

        //[HttpGet]
        //[Route("GetEmp/{id}")]
        //public async Task<IHttpActionResult> GetEmpByID(string id)
        //{
            
        //var result = await employeeService.GetByIdAsync(Convert.ToInt32(id));

        //    return Ok(result);
        //}

        [HttpGet]
        [Route("GetEmpDetailed/{id}")]
        public async Task<IHttpActionResult> GetEmpDetailed(string id)
        {
            var result = await employeeService.GetEmployeealldetails(Convert.ToInt32(id));

            return Ok(result);
        }
        [HttpDelete]
        [Route("EmpDelete/{employee_id}")]
        public async Task<IHttpActionResult> EmployeeDelete(int employee_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(employee_id.ToString());
            }
            else
            {
                var result = await employeeService.DeleteEmployee(employee_id);

                if (result == -1)
                    return Ok(result);
                else
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("NewEmployee")]
        public async Task<IHttpActionResult> AddEmployee(EmpCompDetail emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(emp.ToString());
            }
            else
            {
                var result = await employeeService.AddEmployee(emp);
                return Ok(result);
            }
            

        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IHttpActionResult> GetAllEmployees()  
        {
            var result = await employeeService.GetAllEmployees();
            //result.ToList();
            return Ok(result);

        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IHttpActionResult> UpdateEmployee(EmpCompDetail emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await employeeService.UpdateEmployee(emp);
                return Ok(result);
            }


        }
    }
}