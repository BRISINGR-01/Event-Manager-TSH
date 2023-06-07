using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Images
{
    public interface IImage: IEntity
    {
        public string ImagePath { get; }
    }
}
