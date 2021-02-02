using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencySolvers
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBrandService>().To<BrandManager>().InSingletonScope();
            Bind<IBrandDal>().To<MemBrandDal>().InSingletonScope();

            Bind<IColorService>().To<ColorManager>().InSingletonScope();
            Bind<IColorDal>().To<MemColorDal>().InSingletonScope();
            
            Bind<ICarService>().To<CarManager>().InSingletonScope();
            Bind<ICarDal>().To<MemCarDal>().InSingletonScope();
        }
    }
}
