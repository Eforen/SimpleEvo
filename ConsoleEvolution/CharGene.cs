using System;
using System.Collections.Generic;
using System.Text;
using Evolution.DNA;

namespace ConsoleEvolution
{
    class CharGene : Evolution.DNA.Gene
    {
        public CharGene(char value)
        {
            this.value = value;
        }
        public char value;

        public override Gene clone { get { return new CharGene(value); } }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
