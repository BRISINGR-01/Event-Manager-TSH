using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface ILocalRepository
    {
        public void Update(string key, string? value);
        public string? Read(string key);
    }
}
