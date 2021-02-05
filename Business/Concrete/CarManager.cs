using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConserns.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car entity)
        {
            ValidationTool.FluentValidate(new CarValidator(), entity);
            _carDal.Add(entity);
        }

        public void Delete(Car entity)
        {
            var car = _carDal.GetById(entity.Id);
            if (car == null) throw new NullReferenceException("Silinecek araç bulunamadı!");
            _carDal.Delete(car);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _carDal.GetAll(filter);
        }

        public Car GetById(int id)
        {
            return _carDal.GetById(id);
        }

        public void Update(Car entity)
        {
            _carDal.Update(entity);
        }
    }
}
