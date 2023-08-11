using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Buisness_Entities.Employee;
using DAL.Implementation;
using DAL.Interfces;
using System.Transactions;

namespace Employee_Services.EmployeeManipulate
{
    public class EmployeeService : IEmployeeService
    {
        private bool disposed = false;
        private readonly IUnitOfWork unitOfWork;
        public EmployeeService()
        {
            unitOfWork = new UnitOfWork();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public Task<Employee_Detials> GetByIdAsync(int Employee_id)
        {
            return unitOfWork.Employee_Details.Get(Employee_id);

            //throw new NotImplementedException();
        }
        public async Task<EmpCompDetail> GetEmployeealldetails(int Employee_id)
        {
            var result = await unitOfWork.Employee_Details.GetEagerByChoice(x => x.Emp_ID == Employee_id, new List<string> { "Salary", "Addresses" });

            EmpCompDetail emp = new EmpCompDetail();
            if (result != null)
            {

                emp.Emp_ID = result.Emp_ID;
                emp.Emp_Name = result.Emp_Name;
                emp.Emp_Gender = result.Emp_Gender;
                emp.Emp_Phone = result.Emp_Phone;
                emp.Emp_Salary = (int)result.Salary.Salary_Amount;

                if (result.Addresses.Count > 0)
                {
                    List<string> addList = new List<string>();
                    for (int i = 0; i < result.Addresses.Count; i++)
                    {
                        addList.Add(result.Addresses.ElementAt(i).Address1);
                    }
                    emp.Addresses = addList;
                }
            }




            return emp;
            //emplist.Add(emp);




            //throw new NotImplementedException();
        }

        public async Task<int> DeleteEmployee(int Employee_ID)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    unitOfWork.Salary.Delete(Employee_ID);
                    unitOfWork.Employee_Details.Delete(Employee_ID);
                    var Addresses = await unitOfWork.Address.GetAll(x => x.Emp_ID == Employee_ID);
                    unitOfWork.Address.DeleteRange(Addresses);
                    var result = unitOfWork.save();
                    scope.Complete();
                    return result;

                    //throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmpCompDetail>> GetAllEmployees()
        {

            List<EmpCompDetail> emplist = new List<EmpCompDetail>();
            var result = await unitOfWork.Employee_Details.GetAllEmployees(new List<string> { "Salary", "Addresses" });


            if (result.Count > 0)
            {
                foreach (var datarow in result)
                {
                    EmpCompDetail emp = new EmpCompDetail();
                    emp.Emp_ID = datarow.Emp_ID;
                    emp.Emp_Name = datarow.Emp_Name;
                    emp.Emp_Gender = datarow.Emp_Gender;
                    emp.Emp_Phone = datarow.Emp_Phone;
                    emp.Emp_Salary = (int)datarow.Salary.Salary_Amount;
                    emplist.Add(emp);
                    if (datarow.Addresses.Count > 0)
                    {
                        List<string> addList = new List<string>();
                        for (int i = 0; i < datarow.Addresses.Count; i++)
                        {
                            addList.Add(datarow.Addresses.ElementAt(i).Address1);
                        }
                        emp.Addresses = addList;
                    }

                }

            }
            return emplist;
        }

        public async Task<List<EmpCompDetail>> AddEmployee(EmpCompDetail emp)
        {
            try
            {
                List<EmpCompDetail> newemp = new List<EmpCompDetail>();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        Employee_Detials empnew = new Employee_Detials
                        {
                            Emp_Gender = emp.Emp_Gender,
                            Emp_Name = emp.Emp_Name,
                            Emp_Phone = emp.Emp_Phone
                        };

                        unitOfWork.Employee_Details.Add(empnew);
                        unitOfWork.save(); // Save changes to generate Emp_ID

                        // Use the generated Emp_ID to insert the salary record
                        Salary empsal = new Salary
                        {
                            Emp_ID = empnew.Emp_ID, // Use the generated Emp_ID
                            Salary_Amount = emp.Emp_Salary
                        };

                        unitOfWork.Salary.Add(empsal);
                        unitOfWork.save();
                        //Employee_Detials empnew = new Employee_Detials();
                        ////empnew.Emp_ID = emp.Emp_ID;
                        //empnew.Emp_Gender = emp.Emp_Gender;
                        //empnew.Emp_Name = emp.Emp_Name;
                        //empnew.Emp_Phone = emp.Emp_Phone;
                        //unitOfWork.Employee_Details.Add(empnew);

                        //Salary empsal = new Salary();
                        //empsal.Emp_ID = emp.Emp_ID;
                        //empsal.Salary_Amount = emp.Emp_Salary;
                        //unitOfWork.Salary.Add(empsal);

                        if (emp.Addresses != null)
                        {


                            if (emp.Addresses.Count > 0)
                            {
                                for (int i = 0; i < emp.Addresses.Count; i++)
                                {
                                    Address empadd = new Address();
                                    empadd.Emp_ID = empnew.Emp_ID;
                                    empadd.Address1 = emp.Addresses[i].ToString();
                                    unitOfWork.Address.Add(empadd);
                                }

                            }
                        }

                        newemp = await GetAllEmployees();
                        //if (result.Count > 0)
                        //{
                        //    foreach (var datarow in result)
                        //    {
                        //        EmpCompDetail employee = new EmpCompDetail();
                        //        employee.Emp_ID = datarow.Emp_ID;
                        //        employee.Emp_Name = datarow.Emp_Name;
                        //        employee.Emp_Gender = datarow.Emp_Gender;
                        //        employee.Emp_Phone = datarow.Emp_Phone;
                        //        newemp.Add(employee);

                        //    }

                        //}
                        unitOfWork.save();
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }


                }
                return newemp;
            }
            catch (Exception ex)
            {

                throw;
            }

            //Address empadd = new Address();
            //empadd.Emp_ID = emp.Emp_ID;
            //empadd.Address1 = emp.Addresses[0].ToString();





            //throw new NotImplementedException();
        }

        public async Task<List<EmpCompDetail>> UpdateEmployee(EmpCompDetail emp)
        {
            List<EmpCompDetail> UPdatedEmployees = new List<EmpCompDetail>();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Employee_Detials empnew = new Employee_Detials
                    {
                        Emp_ID = emp.Emp_ID,
                        Emp_Gender = emp.Emp_Gender,
                        Emp_Name = emp.Emp_Name,
                        Emp_Phone = emp.Emp_Phone
                    };
                    unitOfWork.Employee_Details.Update(empnew);
                    //unitOfWork.Employee_Details.Add(empnew);
                    /*unitOfWork.save();*/ // Save changes to generate Emp_ID

                    // Use the generated Emp_ID to insert the salary record
                    Salary empsal = new Salary
                    {
                        Emp_ID = empnew.Emp_ID, // Use the generated Emp_ID
                        Salary_Amount = emp.Emp_Salary
                    };
                    unitOfWork.Salary.Update(empsal);
                    //unitOfWork.Salary.Add(empsal);
                    unitOfWork.save();

                    var Addresses = await unitOfWork.Address.GetAll(x => x.Emp_ID == emp.Emp_ID);
                    if (Addresses.Count > 0 && emp.Addresses != null)
                    {
                        int count = (Addresses.Count < emp.Addresses.Count) ? Addresses.Count : emp.Addresses.Count;
                        int i;
                        for (i = 0; i < count; i++)
                        {
                            Address address = new Address();
                            address.Adderess_ID = Addresses[i].Adderess_ID;
                            address.Emp_ID = emp.Emp_ID;
                            address.Address1 = emp.Addresses.ElementAt(i);
                            unitOfWork.Address.Update(address, Addresses[i]);
                        }
                        if (Addresses.Count < emp.Addresses.Count)
                        {
                            for (int j = i; j < emp.Addresses.Count; j++)
                            {
                                Address empadd = new Address();
                                empadd.Emp_ID = emp.Emp_ID;
                                empadd.Address1 = emp.Addresses[j].ToString();
                                unitOfWork.Address.Add(empadd);
                            }
                        }
                        else if (Addresses.Count > emp.Addresses.Count)
                        {
                            List<Address> addlist = new List<Address>();
                            for (int j = i; j < Addresses.Count; j++)
                            {
                                addlist.Add(Addresses[j]);
                                //unitOfWork.Address.Delete(Addresses[i]);
                            }
                            unitOfWork.Address.DeleteRange(addlist);
                        }
                    }
                    else
                    {
                        if (emp.Addresses != null)
                        {
                            if (emp.Addresses.Count > 0)
                            {
                                for (int i = 0; i < emp.Addresses.Count; i++)
                                {
                                    Address empadd = new Address();
                                    empadd.Emp_ID = emp.Emp_ID;
                                    empadd.Address1 = emp.Addresses[i].ToString();
                                    unitOfWork.Address.Add(empadd);
                                }
                            }

                        }
                        else if (Addresses.Count > 0)
                        {
                            List<Address> addlist = new List<Address>();
                            for (int j = 0; j < Addresses.Count; j++)
                            {
                                addlist.Add(Addresses[j]);
                                //unitOfWork.Address.Delete(Addresses[i]);
                            }
                            unitOfWork.Address.DeleteRange(addlist);
                        }
                    }
                    UPdatedEmployees = await GetAllEmployees();
                    unitOfWork.save();
                    scope.Complete();
                    return UPdatedEmployees;
                }
                catch (Exception ex)
                {

                    throw;
                }

                //public Task<List<Employee_Detials>> UpdateEmployees()
                //{
                //    return unitOfWork.Employee_Details.Update();

                //    //throw new NotImplementedException();
                //}
            }
        }
    }
}
