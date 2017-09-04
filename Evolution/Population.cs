using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution
{
    public class Population<T> where T : Evolution.DNA.Gene
    {
        public Evolution.DNA.DNA<T>[] pop;
        public int popSize;
        public DNA.Genome<T> genome;
        public Random rand = new Random();

        public Population(int popSize, DNA.Genome<T> genome)
        {
            this.genome = genome;
            this.popSize = popSize;
            this.pop = new DNA.DNA<T>[popSize];
            for (int i = 0; i < popSize; i++)
                this.pop[i] = new DNA.DNA<T>(genome);
        }
        
        List<DNA.DNA<T>> matingPool = new List<DNA.DNA<T>>();

        /// <summary>
        /// Generate mating pool
        /// </summary>
        public void naturalSelection()
        {
            matingPool.Clear();

            //Get larges fitness value
            float maxFitness = 0;
            for (int i = 0; i < pop.Length; i++)
            {
                if (maxFitness < pop[i].fitness)
                    maxFitness = pop[i].fitness;
            }

            //add each DNA Actor to the mating pool n times based on its fitness
            for (int i = 0; i < pop.Length; i++)
            {
                int n = (int) pop[i].fitness.Remap(0, maxFitness, 0, 100);

                for (int ii = 0; ii < n; ii++)
                {
                    this.matingPool.Add(pop[i]);
                }
            }
        }

        /// <summary>
        /// Create next generation
        /// </summary>
        public void generate()
        {
            //Just overwrite current pop because we have the mating pool
            for (int i = 0; i < pop.Length; i++)
            {
                if (i < genome.clonesToKeep)
                {
                    pop[i] = top[i].clone;
                }
                else
                {
                    DNA.DNA<T> a = matingPool[rand.Next(0, matingPool.Count)];
                    DNA.DNA<T> b = matingPool[rand.Next(0, matingPool.Count)];
                    DNA.DNA<T> child = a.crossover(b);
                    child.mutate(genome.mutationRate);
                    pop[i] = child;
                }
            }
            calcFitness();
        }

        DNA.DNA<T>[] top;

        /// <summary>
        /// Calculate fitness
        /// </summary>
        public void calcFitness()
        {
            //top = new DNA.DNA<T>[genome.slowDownTop == 0 ? 1 : genome.slowDownTop];
            top = new DNA.DNA<T>[genome.slowDownTop > genome.clonesToKeep ? genome.slowDownTop : genome.clonesToKeep];
            for (int i = 0; i < pop.Length; i++)
            {
                pop[i].fitness = pop[i].genome.getFitness(pop[i]);
                if (float.IsInfinity(pop[i].fitness)) pop[i].fitness = 0;
                
                if (top.Length > 0)
                {
                    int pushback = -1;
                    for (int t = 0; t < top.Length; t++)
                    {
                        if (top[t] == null || top[t].fitness < pop[i].fitness)
                        {
                            pushback = t;
                            break;
                        }
                    }
                    if (pushback > -1)
                    {
                        for (int t = top.Length - 2; t >= pushback; t--)
                        {
                            top[t + 1] = top[t];
                        }
                        top[pushback] = pop[i];
                    }
                }
            }
            
            for (int i = 0; i < genome.slowDownTop; i++)
            {
                pop[i].isSlow = true;
            }
        }

        /// <summary>
        /// Check if we are done
        /// </summary>
        public bool evaluate()
        {
            return false;
        }

    }
}
