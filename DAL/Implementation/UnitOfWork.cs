using DAL.Interfaces;
using DAL.Interfces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IGenericRepository<Employee_Detials> Employee_Detials;
        private IGenericRepository<Address> Addresss;
        private IGenericRepository<Salary> Salaryy;

        private CRUDEntities _context = null;
        private ObjectContext objectContext = null;
        private bool disposed;

        public UnitOfWork()
        {
            _context = new CRUDEntities();
            objectContext = ((IObjectContextAdapter)_context).ObjectContext;
        }
        public CRUDEntities Context
        {
            get { return _context; }
        }

        //public IGenericRepository<Employee_Detials> Employee_Details()
        //{
        //    throw new NotImplementedException();
        //}


        IGenericRepository<Address> IUnitOfWork.Address
        {
            get
            {
                if (this.Addresss == null)
                    this.Addresss = new GenericRepository<Address>(_context);

                return Addresss;
            }
        }

        IGenericRepository<Salary> IUnitOfWork.Salary
        {
            get
            {
                if (this.Salaryy == null)
                    this.Salaryy = new GenericRepository<Salary>(_context);

                return Salaryy;
            }
        }

        IGenericRepository<Employee_Detials> IUnitOfWork.Employee_Details
        {
            get
            {
                if (this.Employee_Detials == null)
                    this.Employee_Detials = new GenericRepository<Employee_Detials>(_context);

                return Employee_Detials;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
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

        public int save()
        {
            int returnVal = -1;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var value = ex.Entries.Single();
                value.OriginalValues.SetValues(value.GetDatabaseValues());
                returnVal = _context.SaveChanges();

            }

            return returnVal;
        }
    }
}
