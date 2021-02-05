using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class MemColorDal : IColorDal
    {
        private List<Color> _colors;
        public MemColorDal()
        {
            _colors = new List<Color>
            {
                new Color{Id = 1, Name="Mavi"},
                new Color{Id = 2, Name="Kırmızı"},
                new Color{Id = 3, Name="Beyaz"}
            };
        }
        public void Add(Color entity)
        {
            entity.Id = _colors.Max(x => x.Id) + 1;
            _colors.Add(entity);
        }

        public void Delete(Color entity)
        {
            _colors.Remove(entity);
        }

        public Color Get(Expression<Func<Color, bool>> filter)
        {
            return _colors.SingleOrDefault(filter.Compile());
        }

        public List<Color> GetAll(Expression<Func<Color, bool>> filter = null)
        {
            return filter == null ? _colors : _colors.Where(filter.Compile()).ToList();
        }

        public Color GetById(int id)
        {
            return _colors.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Color entity)
        {
            var color = _colors.SingleOrDefault(c => c.Id == entity.Id);
            if (color == null) throw new NullReferenceException("Güncellenecek renk bulunamadı");
            color.Name = entity.Name;
        }
    }
}
