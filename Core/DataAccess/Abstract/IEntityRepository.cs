using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()  // IEntityRepository interface'ine verilecek tip bir class olmalı IEntity'den türemiş olmalı ve Intance'ı alınabilir olmalı.
    {
        List<T> GetAll(Func<T, bool> filter = null);  // zorunlu olmayan bir parametre. 
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
