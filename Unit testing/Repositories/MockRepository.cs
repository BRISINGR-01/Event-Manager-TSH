using Logic;
using Logic.Interfaces;
using Logic.Interfaces.Repositories;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_testing.Repositories
{
    public class MockRepository<T>: IRepository<T> where T : IEntity
    {
        public Guid BranchId { get => Guid.Empty; }
        protected readonly List<T> _data;
        public T Random { get => _data.Count > 0 ? _data[new Random().Next(0, _data.Count)] : throw new Exception("There is no data to pick a random element from.");  }
        public T First { get => _data.Count > 0 ? _data[0] : throw new Exception("There is no data to pick an element from.");  }
        public MockRepository()
        {
            _data = new();
        }
        protected void AddData(T entity)
        {
            _data.Add(entity);
        }
        public List<T> GetAll(int? offsetIndex = null)
        {
            return _data;
        }
        public bool Create(T entity)
        {
            _data.Add(entity);
            return true;
        }
        public bool Update(T Entity)
        {
            _data[_data.FindIndex(entity => entity.Id == Entity.Id)] = Entity;
            return true;
        }
        public bool Delete(Guid id)
        {
            _data.RemoveAt(_data.FindIndex(entity => entity.Id == id));
            return true;
        }
        public T? FindSingleBy(Guid id)
        {
            return _data.Find(entity => entity.Id == id);
        }
    }
}
