using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDTO> GetCarsDetails()
        {
            using (var context = new ReCapContext())
            {
                var cars = from c in context.Cars
                           join b in context.Brands
                           on c.BrandId equals b.Id
                           join cl in context.Colors
                           on c.ColorId equals cl.Id
                           select new CarDTO
                           {
                               Brand = b.Name,
                               Color = cl.Name,
                               DailyPrice = c.DailyPrice,
                               Description = c.Description,
                               ModelYear = c.ModelYear
                           };
                //var carsDto = context.Cars.Select(x => new CarDTO
                //{
                //    Brand = context.Brands.SingleOrDefault(b => b.Id == x.BrandId).Name,
                //    Color = context.Colors.SingleOrDefault(cl => cl.Id == x.ColorId).Name,
                //    DailyPrice = x.DailyPrice,
                //    Description = x.Description,
                //    ModelYear = x.ModelYear
                //});
                    return cars.ToList();
            }
        }   
        public CarDTO GetCarDetailsById(int id)
        {
            using (var context = new ReCapContext())
            {
                var car = from cr in context.Cars
                          join br in context.Brands
                          on cr.BrandId equals br.Id
                          join cl in context.Colors
                          on cr.ColorId equals cl.Id
                          where cr.Id == id
                          select new CarDTO
                          {
                              Brand = br.Name,
                              Color = cl.Name,
                              DailyPrice = cr.DailyPrice,
                              Description = cr.Description,
                              ModelYear = cr.ModelYear
                          };
                return car.SingleOrDefault();
            }
        }
    }
}
