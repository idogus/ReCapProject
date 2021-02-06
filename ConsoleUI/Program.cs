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
using System.Diagnostics;
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
            Brand brand = new Brand { Name = "Mercedes" };
            Color color = new Color { Name = "Beyaz" };
            Car car = new Car {Description = "Otomatik klima, Otomatik vites", DailyPrice = 0, ModelYear = 2021, Brand = new Brand { Name = "S" }, ColorId = 1  };

            try
            {
                _colorService.Add(color);
                _brandService.Add(brand);
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

            var cars = _carService.GetAll();

            WriteTheCars(_carService.GetCarDTOs()); // UI metodu 

            var allSkodas = _carService.GetCarDTOs().Where(x => x.Brand == "Skoda").ToList();
            WriteTheCars(allSkodas);

            WriteCar(_carService.GetCarDTO(4)); // Id değerine göre tablodan değer getiren sorgu

            WriteTheCars(_carService.GetCarDTOs());
        }

        // Araç listesini yazdıran metod
        private static void WriteTheCars(IList<CarDTO> cars)
        {
            Console.WriteLine("=============== Araç Listesi ================");

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Brand}    \t{car.Color}    \t{car.ModelYear} \t{car.DailyPrice.ToString("#,###.00")}    \t{car.Description}");
            }
        }
        // Tek aracı yazdıran metod
        private static void WriteCar(CarDTO car)
        {
            if (car != null)
            {
                Console.WriteLine("============ Seçilen Araç ================");
                Console.WriteLine($"{car.Brand} marka \n{ car.Color} renkli \n{car.Description} özelliklerine sahip \n{car.ModelYear} model araç günlüğü \n{car.DailyPrice.ToString("#,###.00")} TL");
            }
        }
    }
}
