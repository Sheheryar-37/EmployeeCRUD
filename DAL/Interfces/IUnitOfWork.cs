using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;

namespace DAL.Interfces
{
    public interface IUnitOfWork :IDisposable
    {

        CRUDEntities Context { get; }

        IGenericRepository <Employee_Detials> Employee_Details { get; }
        IGenericRepository <Address> Address { get; }

        IGenericRepository <Salary> Salary { get; }
        int save();
        //IGenericRepository<Employee_Detials> Employee_Details();
    }
}
