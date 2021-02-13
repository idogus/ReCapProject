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
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(User entity)
        {
            var userToDelete = _userDal.GetById(entity.Id);
            if (userToDelete == null) return new ErrorResult();
            _userDal.Delete(userToDelete);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(filter));
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.GetById(id));
        }

        public IResult Update(User entity)
        {
            var userToUpdate = _userDal.GetById(entity.Id);
            if (userToUpdate == null) return new ErrorResult();
            _userDal.Update(userToUpdate);
            return new SuccessResult();
        }
    }
}
