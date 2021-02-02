using Business.Abstract;
using Business.DependencySolvers;
using ConsoleUI.Models;
using Core.Entity;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        private static ICarService _carService;
        private static IBrandService _brandService;
        private static IColorService _colorService;
        static void Main(string[] args)
        {
            _carService = InstanceFactory.GetInstance<ICarService>();
            _colorService = InstanceFactory.GetInstance<IColorService>();
            _brandService = InstanceFactory.GetInstance<IBrandService>();

            WriteTheCars();

            Console.WriteLine("============ Seçilen Araç ================");
            WriteCar(_carService.GetById(1));

            Console.WriteLine("============= Eklenen Araç ================");

            Car addedCar = new Car { BrandId = 1, ColorId = 3, DailyPrice = 2000, ModelYear = 2021, Description = "Çok noktalı klima, Multimedia sistemi" };
            _carService.Add(addedCar);
            WriteCar(addedCar);
            WriteTheCars();
        }
        private static IEnumerable<CarDTO> GetCarDTOsByLinq()
        {
            IEnumerable<CarDTO> resultLinq = from c in _carService.GetAll()
                                             join b in _brandService.GetAll()
                                             on c.BrandId equals b.Id
                                             join cl in _colorService.GetAll()
                                             on c.ColorId equals cl.Id
                                             select new CarDTO
                                             {
                                                 Brand = b.Name,
                                                 Color = cl.Name,
                                                 DailyPrice = c.DailyPrice,
                                                 Description = c.Description,
                                                 ModelYear = c.ModelYear
                                             };
            return resultLinq;
        }
        private static List<CarDTO> GetCarDTOsByLambda()
        {
            var resultLambda = _carService.GetAll()
                .Join(_brandService.GetAll(), c => c.BrandId, b => b.Id, (c, b) => new Car
                {
                    Id = c.Id,
                    Brand = b,
                    BrandId = c.BrandId,
                    ColorId = c.ColorId,
                    DailyPrice = c.DailyPrice,
                    Description = c.Description,
                    ModelYear = c.ModelYear
                }).Join(_colorService.GetAll(), c => c.ColorId, cl => cl.Id, (c, cl) => new CarDTO
                {
                    Brand = c.Brand.Name,
                    Color = cl.Name,
                    DailyPrice = c.DailyPrice,
                    ModelYear = c.ModelYear,
                    Description = c.Description
                }).ToList();
            return resultLambda;
        }
        private static CarDTO GetCarDTO(Car car)
        {
            var resultDto = new CarDTO
            {
                Brand = _brandService.GetById(car.BrandId).ToString(),
                Color = _colorService.GetById(car.ColorId).ToString(),
                ModelYear = car.ModelYear,
                DailyPrice = car.DailyPrice,
                Description = car.Description
            };

            return resultDto;
        }

        private static void WriteTheCars()
        {
            List<CarDTO> cars = _carService.GetAll().Select(x => GetCarDTO(x)).ToList();

            Console.WriteLine("=============== Araç Listesi ================");

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Brand}    \t{car.Color}    \t{car.ModelYear} \t{car.DailyPrice.ToString("#,###.00")}    \t{car.Description}");
            }
        }
        private static void WriteCar(Car car)
        {
            var carDTO = GetCarDTO(car);
            Console.WriteLine($"{carDTO.Brand} marka \n{ carDTO.Color} renkli \n{car.Description} özelliklerine sahip \n{carDTO.ModelYear} model araç günlüğü \n{carDTO.DailyPrice.ToString("#,###.00")} TL");
        }
    }
}
