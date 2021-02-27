using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll(Expression<Func<CarImage, bool>> filter = null);
        IDataResult<CarImage> GetById(int id);
        IResult Add(CarImage entity);
        IResult Update(CarImage entity);
        IResult Delete(CarImage entity);
        IDataResult<List<CarImage>> GetImagesByCarId(int carId);
    }
}
