using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.DNA
{
    public class DNA<T> where T : Gene
    {
        public Genome<T> genome;
        public T[] genes;
        public float fitness = 0;
        public bool isSlow;
        public DNA(Genome<T> genome)
        {
            this.genome = genome;
            genes = new T[genome.length];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = genome.getRandomGene();
            }
        }
        public DNA(DNA<T> dna)
        {
            this.genome = dna.genome;
            genes = new T[genome.length];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = (T)dna.genes[i].clone;
            }
        }

        public DNA<T> clone { get { return new DNA<T>(this); } }

        public DNA<T> crossover(DNA<T> b)
        {
            DNA<T> gene = new DNA<T>(genome);

            // Pick a split point
            /* we must wait at least 1 gene before the split
             * so that we never have a situation where there is 
             * no split in the genes and we are just returning one or the other in full
             * for this reason we also require that the random split is 1 less then the end of the gene pool so that we will atleast have a split of 1 gene from each no mater what.)*/
            int flip = genome.rand.Next(1, genes.Length - 1);
            bool fromSelf = true;
            for (int i = 0; i < genes.Length; i++)
            {
                if(flip == 0 && isSlow == false && b.isSlow == false) 
                {
                    flip = genome.rand.Next(0, genes.Length - 1);
                    fromSelf = !fromSelf;
                }

                gene.genes[i] = fromSelf ? genes[i] : b.genes[i];

                flip--;
            }

            return gene;
        }

        public void mutate(float mutationRate = -1)
        {
            int corrector = 50;
            if (mutationRate == -1) mutationRate = genome.mutationRate;

            int floatResolution = 100000;
            float chanceRoll = 0;
            for (int i = 0; i < genes.Length; i++)
            {
                chanceRoll = genome.rand.Next(0, floatResolution) / (float) floatResolution;
                if (chanceRoll < mutationRate * corrector / genes.Length)
                    genes[i] = genome.getRandomGene();
            }
        }
    }
}
