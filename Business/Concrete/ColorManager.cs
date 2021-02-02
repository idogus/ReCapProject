using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
            _colorDal.Add(entity);
        }

        public void Delete(Color entity)
        {
            var color = _colorDal.GetById(entity.Id);
            if (color == null) throw new NullReferenceException("Silinecek renk bulunamadı!");
            _colorDal.Delete(color);
        }

        public List<Color> GetAll(Func<Color, bool> filter = null)
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
