using Business.Abstract;
using Business.DependencySolvers;
using Business.ValidationRules.FluentValidation;
using ConsoleUI.Models;
using Core.CrossCuttingConserns.FluentValidation;
using Core.Entity;
using Entities.Concrete;
using FluentValidation;
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
            // Business katmanındaki Ninject dependency solver instance factory metodu
            _carService = InstanceFactory.GetInstance<ICarService>();
            _colorService = InstanceFactory.GetInstance<IColorService>();
            _brandService = InstanceFactory.GetInstance<IBrandService>();

            // 8. Gün ödevi testi
            Car car = new Car { ColorId = 1, Description = "Test", DailyPrice = 0, ModelYear = 2021, Brand = new Brand { Name = "A" } };

            try
            {
                ValidationTool.FluentValidate(new BrandValidator(), car.Brand);
                _brandService.Add(car.Brand);
            }
            catch (ValidationException ex)
            {
                var msg = ex.Message.Split(':')[ex.Message.Split(':').Length - 1];
                Console.WriteLine(msg); ;
            }

            try
            {
                ValidationTool.FluentValidate(new CarValidator(), car);
                _carService.Add(car);
            }
            catch (ValidationException ex)
            {
                var msg = ex.Message.Split(':')[ex.Message.Split(':').Length - 1];
                Console.WriteLine(msg); ;
            }


            WriteTheCars(); // UI metodu biraz clean code

            // ilişkili(bağlı) tabloları ekleyerek birlikte geriye ana tabloyu döndürüen Linq-Lambda Select metodu
            var resultByLinkedTables = _carService.GetAll().Select(x => new Car
            {
                Id = x.Id,
                BrandId = x.BrandId,
                ColorId = x.ColorId,
                Brand = _brandService.GetById(x.BrandId),
                Color = _colorService.GetById(x.ColorId),
                ModelYear = x.ModelYear,
                DailyPrice = x.DailyPrice,
                Description = x.Description
            }).ToList();

            // Bire çok tablolardan veri getiren markaya ait araçları getiren Linq-lambda SelectMany Metodu
            var allMercedes = _brandService.GetAll(b => b.Name.Contains("Mercedes")).SelectMany(b => _carService.GetAll(x => x.BrandId == b.Id)).ToList();

            Console.WriteLine("============ Seçilen Araç ================");
            WriteCar(_carService.GetById(1)); // Id değerine göre tablodan değer getiren sorgu

            Console.WriteLine("============= Eklenen Araç ================");

            // Instance anında nesne yaratma
            Car addedCar = new Car { BrandId = 1, ColorId = 3, DailyPrice = 2000, ModelYear = 2021, Description = "Çok noktalı klima, Multimedia sistemi" };
            _carService.Add(addedCar); // Business katmanında veri ekleme
            WriteCar(addedCar);
            WriteTheCars();
        }

        // Bir DTO listesi döndüren Linq join sorgusu
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

        // Bir DTO nesne listesi döndüren Linq-lambda join sorgusu
        private static List<CarDTO> GetCarDTOsByLambda()
        {
            var resultLambda = _carService.GetAll()
                .Join(_brandService.GetAll(), c => c.BrandId, b => b.Id, (c, b) => new Car // Join metodu parametreleri( bağlanacak tablo, sol tablonun bağlantı parametresi, sağ tablonun parametresi, sağ ve sol tabloyu döndüren lambda fonsiyon)
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
        // Entity parametresiyle geriye tek bir DTO nesnesi döndüren metod
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

        // Araç listesini yazdıran metod
        private static void WriteTheCars()
        {
            List<CarDTO> cars = _carService.GetAll().Select(x => GetCarDTO(x)).ToList();

            Console.WriteLine("=============== Araç Listesi ================");

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Brand}    \t{car.Color}    \t{car.ModelYear} \t{car.DailyPrice.ToString("#,###.00")}    \t{car.Description}");
            }
        }
        // Tek aracı yazdıran metod
        private static void WriteCar(Car car)
        {
            var carDTO = GetCarDTO(car);
            Console.WriteLine($"{carDTO.Brand} marka \n{ carDTO.Color} renkli \n{car.Description} özelliklerine sahip \n{carDTO.ModelYear} model araç günlüğü \n{carDTO.DailyPrice.ToString("#,###.00")} TL");
        }
    }
}
