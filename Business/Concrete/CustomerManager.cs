using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer entity)
        {
            _customerDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Customer entity)
        {
            var customerToDelete = _customerDal.GetById(entity.Id);
            if (customerToDelete == null) return new ErrorResult();
            _customerDal.Delete(customerToDelete);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(filter));
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.GetById(id));
        }

        public IResult Update(Customer entity)
        {
            var customerToUpdate = _customerDal.GetById(entity.Id);
            if (customerToUpdate == null) return new ErrorResult();
            _customerDal.Update(customerToUpdate);
            return new SuccessResult();
        }
    }
}
