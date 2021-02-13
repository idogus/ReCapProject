using Business.Abstract;
using Business.DependencySolvers;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        private static ICarService _carService;
        private static IBrandService _brandService;
        private static IColorService _colorService;
        private static IUserService _userService;
        private static ICustomerService _customerService;
        private static IRentalService _rentalService;
        static void Main(string[] args)
        {
            // Business katmanındaki Ninject dependency solver instance factory metodu
            _carService = InstanceFactory.GetInstance<ICarService>();
            _colorService = InstanceFactory.GetInstance<IColorService>();
            _brandService = InstanceFactory.GetInstance<IBrandService>();
            _userService = InstanceFactory.GetInstance<IUserService>();
            _customerService = InstanceFactory.GetInstance<ICustomerService>();
            _rentalService = InstanceFactory.GetInstance<IRentalService>();
            //SekizinciGunTest();

            //WriteTheCars(_carService.GetCarDTOs()); // UI metodu 

            //var allSkodas = _carService.GetCarDTOs().Where(x => x.Brand == "Skoda").ToList();
            //WriteTheCars(allSkodas);

            //WriteCar(_carService.GetCarDTO(6)); // Id değerine göre tablodan değer getiren sorgu

            //WriteTheCars(_carService.GetCarDTOs());

            //Task10_1();

            //CreateUserAndCustomer();

            //RentCar();

            //ReturnCar();
        }

        private static void ReturnCar()
        {
            var carsAtCustomer = _rentalService.GetAll(x => x.CustomerId == 1 && x.ReturnDate == null);
            foreach (var car in carsAtCustomer.Data)
            {
                if (car.CarId == 7)
                {
                    car.ReturnDate = DateTime.Now;
                    _rentalService.Update(car);
                }
            }
        }

        private static void RentCar()
        {
            Console.WriteLine(_rentalService.Add(new Rental { CarId = 7, CustomerId = 1, RentDate = DateTime.Now.Date }).Message);
        }

        private static void CreateUserAndCustomer()
        {
            Console.WriteLine(_userService.Add(new User { FirstName = "İbrahim", LastName = "Doğuş", EMail = "idogus@gmail.com", UserName = "idogus", Password = "1234" }).Message);
            Console.WriteLine(_customerService.Add(new Customer { UserId = 1, CompanyName = "Doğuş Yazılım" }).Message);
        }

        private static void Task10_1()
        {
            Console.WriteLine(_brandService.Add(new Brand { Name = "Vişne Çürüğü" }).Message);
        }

        private static void SekizinciGunTest()
        {
            // 8. Gün ödevi testi
            Brand ford = new Brand { Name = "Ford" };
            Brand a = new Brand { Name = "A" };
            Color balKopugu = new Color { Name = "Bal Köpüğü" };
            Car car = new Car { Description = "Otomatik klima, Otomatik vites", DailyPrice = 1200, ModelYear = 2021, BrandId = 2, ColorId = 2 };

            try
            {
                _colorService.Add(balKopugu);
                _brandService.Add(ford);
                _brandService.Add(a);
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
