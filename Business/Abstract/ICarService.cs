using Core.Utilities.Results;
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
        IDataResult<List<Car>> GetAll(Expression<Func<Car, bool>> filter = null);
        IDataResult<List<Car>> GetByBrandId(int brandId);
        IDataResult<List<Car>> GetByColorId(int colorId);
        IDataResult<List<CarDTO>> GetCarDTOs();
        IDataResult<CarDTO> GetCarDTO(int id);
        IDataResult<Car> GetById(int id);
        IResult Add(Car entity);
        IResult Update(Car entity);
        IResult Delete(Car entity);
    }
}
