using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental entity)
        {
            var carAvaliableForHire = _rentalDal.Get(x => x.CarId == entity.CarId && x.ReturnDate != null);
            if (carAvaliableForHire == null) return new ErrorResult(Messages.NoCarAvailable);
            _rentalDal.Add(entity);
            return new SuccessResult(Messages.CarRented);
        }

        public IResult Delete(Rental entity)
        {
            var rentalToDelete = _rentalDal.GetById(entity.Id);
            if (rentalToDelete == null) return new ErrorResult();
            _rentalDal.Delete(rentalToDelete);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(filter));
        }

        public IDataResult<Rental> GetAvailableCar(Rental entity)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.CarId == entity.CarId && x.ReturnDate != null));
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetById(id));
        }

        public IResult Update(Rental entity)
        {
            var rentalToUpdate = _rentalDal.GetById(entity.Id);
            if (rentalToUpdate == null) return new ErrorResult(Messages.ErrorRentUpdate);
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.SuccessRentUpdate);
        }
    }
}
