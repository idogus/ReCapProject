using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll(Expression<Func<Car, bool>> filter = null);
        List<Car> GetByBrandId(int brandId);
        List<Car> GetByColorId(int colorId);
        List<CarDTO> GetCarDTOs();
        CarDTO GetCarDTO(int id);
        Car GetById(int id);
        void Add(Car entity);
        void Update(Car entity);
        void Delete(Car entity);
    }
}
