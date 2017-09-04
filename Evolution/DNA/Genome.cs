using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.DNA
{
    public abstract class Genome<T> where T : Gene
    {
        public Random rand = new Random();
        public abstract float mutationRate { get; set; }
        public abstract int length { get; }
        public abstract T getRandomGene();
        public abstract float getFitness(DNA<T> dna);
        /// <summary>
        /// Indicates to slow down the top n fitness results.
        /// </summary>
        public abstract int slowDownTop { get; set; }

        /// <summary>
        /// How many of the top performers should be cloned directly into the new generations.
        /// </summary>
        public abstract int clonesToKeep { get; set; }
    }
}
