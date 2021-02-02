using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        List<Color> GetAll(Func<Color, bool> filter = null);
        Color GetById(int id);
        void Add(Color entity);
        void Update(Color entity);
        void Delete(Color entity);
    }
}
