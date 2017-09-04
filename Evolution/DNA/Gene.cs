using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.DNA
{
    public abstract class Gene
    {
        public abstract Gene clone { get; }
    }
}
