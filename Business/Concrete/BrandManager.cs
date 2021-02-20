using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand entity)
        {
            //ValidationTool.Validate(new BrandValidator(), entity);
            if (_brandDal.Get(x => x.Name == entity.Name) == null)
            {
                _brandDal.Add(entity);
                return new SuccessResult(Messages.BrandAdded);
            }
            return new ErrorResult(Messages.BrandExists);
        }

        public IResult Delete(Brand entity)
        {
            var brand = _brandDal.GetById(entity.Id);
            if (brand == null) return new ErrorResult();
            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public IDataResult<List<Brand>> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(filter));
        }

        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.GetById(id));
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult();
        }
    }
}
