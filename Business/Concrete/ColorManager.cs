using Business.Abstract;
using Core.Utilities.Results;
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

        public IResult Add(Color entity)
        {
            if (_colorDal.Get(x => x.Name == entity.Name) == null)
            {
                _colorDal.Add(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(Color entity)
        {
            var color = _colorDal.GetById(entity.Id);
            if (color == null) return new ErrorResult();
            _colorDal.Delete(color);
            return new SuccessResult();
        }

        public IDataResult<List<Color>> GetAll(Expression<Func<Color, bool>> filter = null)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(filter));
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.GetById(id));
        }

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult();
        }
    }
}
