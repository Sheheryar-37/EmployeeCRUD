using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Buisness_Entities.Employee;
using DAL.Interfaces;
using DAL.Interfces;
using DAL.Implementation;

namespace Employee_Services.EmployeeManipulate
{
    public interface IEmployeeService :IDisposable
    {
        Task<Employee_Detials> GetByIdAsync(int Employee_id);
        Task<EmpCompDetail> GetEmployeealldetails(int Employee_id);
        Task<int> DeleteEmployee(int Employee_id);
        
        Task<List<EmpCompDetail>> GetAllEmployees();

        Task<List<EmpCompDetail>> AddEmployee(EmpCompDetail emp);
        Task<List<EmpCompDetail>> UpdateEmployee(EmpCompDetail emp);




    }

}
