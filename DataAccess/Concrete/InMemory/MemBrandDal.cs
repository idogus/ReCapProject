using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class MemBrandDal : IBrandDal
    {
        private List<Brand> _brands;
        public MemBrandDal()
        {
            _brands = new List<Brand>
            {
                new Brand{Id = 1, Name = "Mercedes"},
                new Brand{Id = 2, Name = "Skoda"},
                new Brand{Id = 3, Name = "Volkswagen"}
            };
        }
        public void Add(Brand entity)
        {
            entity.Id = _brands.Max(x => x.Id) + 1;
            _brands.Add(entity);
        }

        public void Delete(Brand entity)
        {
            _brands.Remove(entity);
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            return _brands.SingleOrDefault(filter.Compile());
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return filter == null ? _brands : _brands.Where(filter.Compile()).ToList();
        }

        public Brand GetById(int id)
        {
            return _brands.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Brand entity)
        {
            var brand = _brands.SingleOrDefault(x => x.Id == entity.Id);
            if (brand == null) throw new NullReferenceException("Güncellenecek marka bulunamadı");
            brand.Name = entity.Name;
        }
    }
}
