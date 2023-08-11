using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Buisness_Entities.Employee;
//using DAL;

namespace Buisness_Entities.Employee
{
    public class EmpCompDetail
    {
        public int Emp_ID { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Gender { get; set; }
        public string Emp_Phone { get; set; }
        public int Emp_Salary { get; set; }
        public List<string> Addresses { get; set; }


    }
}
