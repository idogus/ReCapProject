using Business.Abstract;
using Business.DependencySolvers;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConserns.FluentValidation;
using Core.Entity;
using Entities.Concrete;
using Entities.Dtos;
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
        //private static IColorService _colorService;
        static void Main(string[] args)
        {
            // Business katmanındaki Ninject dependency solver instance factory metodu
            _carService = InstanceFactory.GetInstance<ICarService>();
            //_colorService = InstanceFactory.GetInstance<IColorService>();
            _brandService = InstanceFactory.GetInstance<IBrandService>();

            // 8. Gün ödevi testi
            Car car = new Car { ColorId = 1, Description = "Test", DailyPrice = 0, ModelYear = 2021, Brand = new Brand { Name = "A" } };

            try
            {
                _brandService.Add(car.Brand);
            }
            catch (ValidationException ex)
            {
                var msg = ex.Message.Split(':')[ex.Message.Split(':').Length - 1];
                Console.WriteLine(msg); ;
            }

            try
            {
                _carService.Add(car);
            }
            catch (ValidationException ex)
            {
                var msg = ex.Message.Split(':')[ex.Message.Split(':').Length - 1];
                Console.WriteLine(msg); ;
            }

            WriteTheCars(); // UI metodu biraz clean code

            // Bire çok tablolardan veri getiren markaya ait araçları getiren Linq-lambda SelectMany Metodu
            var allMercedes = _brandService.GetAll(b => b.Name.Contains("Mercedes")).SelectMany(b => _carService.GetAll(x => x.BrandId == b.Id)).ToList();

            Console.WriteLine("============ Seçilen Araç ================");
            WriteCar(); // Id değerine göre tablodan değer getiren sorgu

            WriteTheCars();
        }

        // Araç listesini yazdıran metod
        private static void WriteTheCars()
        {
            List<CarDTO> cars = _carService.GetCarDTOs();

            Console.WriteLine("=============== Araç Listesi ================");

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Brand}    \t{car.Color}    \t{car.ModelYear} \t{car.DailyPrice.ToString("#,###.00")}    \t{car.Description}");
            }
        }
        // Tek aracı yazdıran metod
        private static void WriteCar()
        {
            var carDTO = _carService.GetCarDTO(2);
            Console.WriteLine($"{carDTO.Brand} marka \n{ carDTO.Color} renkli \n{carDTO.Description} özelliklerine sahip \n{carDTO.ModelYear} model araç günlüğü \n{carDTO.DailyPrice.ToString("#,###.00")} TL");
        }
    }
}
