using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll(Func<Brand, bool> filter = null);
        Brand GetById(int id);
        void Add(Brand entity);
        void Update(Brand entity);
        void Delete(Brand entity);
    }
}
