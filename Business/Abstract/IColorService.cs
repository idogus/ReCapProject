using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        List<Color> GetAll(Expression<Func<Color, bool>> filter = null);
        Color GetById(int id);
        void Add(Color entity);
        void Update(Color entity);
        void Delete(Color entity);
    }
}
