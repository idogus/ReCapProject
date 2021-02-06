﻿using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConserns.FluentValidation;
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

        public void Add(Brand entity)
        {
            ValidationTool.FluentValidate(new BrandValidator(), entity);
            if (_brandDal.Get(x => x.Name == entity.Name) == null)
            {
                _brandDal.Add(entity);
            }
        }

        public void Delete(Brand entity)
        {
            var brand = _brandDal.GetById(entity.Id);
            if (brand == null) throw new NullReferenceException("Silinecek marka bulunamadı!");
            _brandDal.Delete(brand);
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return _brandDal.GetAll(filter);
        }

        public Brand GetById(int id)
        {
            return _brandDal.GetById(id);
        }

        public void Update(Brand entity)
        {
            _brandDal.Update(entity);
        }
    }
}
