using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll(Expression<Func<Car, bool>> filter = null);
        Car GetById(int id);
        void Add(Car entity);
        void Update(Car entity);
        void Delete(Car entity);
    }
}
