using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal brandDal)
        {
            _colorDal = brandDal;
        }

        public void Add(Color entity)
        {
            if (_colorDal.Get(x => x.Name == entity.Name) == null)
            {
                _colorDal.Add(entity);
            }
        }

        public void Delete(Color entity)
        {
            var color = _colorDal.GetById(entity.Id);
            if (color == null) throw new NullReferenceException("Silinecek renk bulunamadı!");
            _colorDal.Delete(color);
        }

        public List<Color> GetAll(Expression<Func<Color, bool>> filter = null)
        {
            return _colorDal.GetAll(filter);
        }

        public Color GetById(int id)
        {
            return _colorDal.GetById(id);
        }

        public void Update(Color entity)
        {
            _colorDal.Update(entity);
        }
    }
}
