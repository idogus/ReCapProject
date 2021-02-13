using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null);
        IDataResult<Rental> GetById(int id);
        IResult Add(Rental entity);
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IDataResult<Rental> GetAvailableCar(Rental entity);
    }
}
