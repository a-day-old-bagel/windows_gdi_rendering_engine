using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids3D
{
    class Wrapper<T>
    {
        public T Value { get; set; }

        public Wrapper(T value)
        {
            this.Value = value;
        }
    }
}
