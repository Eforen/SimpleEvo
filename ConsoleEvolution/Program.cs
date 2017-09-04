using System;
using System.Collections.Generic;
using System.Text;
using Evolution;
using Evolution.DNA;

namespace ConsoleEvolution
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make buffer not store history so no clear is needed.
            Console.BufferHeight = Console.WindowHeight;

            bool updated = true;

            setup();
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.Q:
                            running = false;
                            updated = true;
                            break;
                        case ConsoleKey.Insert:
                            if(key.Modifiers == ConsoleModifiers.Shift)
                                pop.genome.mutationRate += 0.001f;
                            else
                                pop.genome.mutationRate += 0.01f;
                            updated = true;
                            break;
                        case ConsoleKey.Delete:
                            if (key.Modifiers == ConsoleModifiers.Shift)
                                pop.genome.mutationRate -= 0.001f;
                            else
                                pop.genome.mutationRate -= 0.01f;
                            updated = true;
                            break;
                        case ConsoleKey.Home:
                            pop.genome.slowDownTop++;
                            pop.calcFitness();
                            updated = true;
                            break;
                        case ConsoleKey.End:
                            pop.genome.slowDownTop--;
                            updated = true;
                            break;
                        case ConsoleKey.PageUp:
                            pop.genome.clonesToKeep++;
                            pop.calcFitness();
                            updated = true;
                            break;
                        case ConsoleKey.PageDown:
                            pop.genome.clonesToKeep--;
                            updated = true;
                            break;
                        default:
                            break;
                    }
                }
                update();
                updateStats();
                if (generation % 25 == 0 || updated)
                {
                    displayInfo();
                    updated = false;
                }
                if (CharGenome.getGenomeValue(best) == target) running = false;
            }
            Console.WriteLine("\nDone... Press any key to exit...");
            Console.ReadKey();
        }

        static string target = "This was a triumph! "+
        "I'm making a note here: "+
        "Huge success! "+
        " "+
        "It's hard to overstate "+
        "my satisfaction. "+
        " "+
        "Aperture Science: "+
        "We do what we must "+
        "because we can "+
        "For the good of all of us. "+
        "Except the ones who are dead. "+
        " "+
        "But there's no sense crying "+
        "over every mistake. "+
        "You just keep on trying "+
        "'til you run out of cake. "+
        "And the science gets done. "+
        "And you make a neat gun "+
        "for the people who are "+
        "still alive. "+
        " "+
        "I'm not even angry... "+
        "I'm being so sincere right now. "+
        "Even though you broke my heart, "+
        "and killed me. "+
        " "+
        "And tore me to pieces. "+
        "And threw every piece into a fire. "+
        "As they burned it hurt because "+
        "I was so happy for you! "+
        " "+
        "Now, these points of data "+
        "make a beautiful line. "+
        "And we're out of beta. "+
        "We're releasing on time! "+
        "So I'm GLaD I got burned! "+
        "Think of all the things we learned! "+
        "for the people who are "+
        "still alive. "+
        " "+
        "Go ahead and leave me... "+
        "I think I'd prefer to stay inside... "+
        "Maybe you'll find someone else "+
        "to help you. "+
        "Maybe Black Mesa? "+
        "That was a joke.Ha Ha. Fat Chance! "+
        " "+
        "Anyway this cake is great! "+
        "It's so delicious and moist! "+
        " "+
        "Look at me: still talking "+
        "when there's science to do! "+
        "When I look out there, "+
        "it makes me glad I'm not you. "+
        " "+
        "I've experiments to run. "+
        "There is research to be done. "+
        "On the people who are "+
        "still alive. "+
        "And believe me I am "+
        "still alive. "+
        "I'm doing science and I'm "+
        "still alive. "+
        "I feel fantastic and I'm "+
        "still alive. "+
        "While you're dying I'll be "+
        "still alive. "+
        "And when you're dead I will be "+
        "still alive "+
        "     "+
        "Still alive. "+
        "     "+
        "Still alive.";
        static int totalPop = 1000;
        static float mutation = 0.01f;

        static Population<CharGene> pop;
        static int generation = 0;
        static bool running = true;

        static DNA<CharGene> best;
        static float avgFit = 0;

        static void setup()
        {
            target = "This was a triumph! " +
        "I'm making a note here: " +
        "Huge success! " +
        " " +
        "It's hard to overstate " +
        "my satisfaction. " +
        " " +
        "Aperture Science: " +
        "We do what we must " +
        "because we can " +
        "For the good of all of us. " +
        "Except the ones who are dead. " +
        " " +
        "But there's no sense crying " +
        "over every mistake. " +
        "You just keep on trying " +
        "'til you run out of cake. " +
        "And the science gets done. " +
        "And you make a neat gun " +
        "for the people who are " +
        "still alive. ";

            // 1: Create a random population of N agents
            pop = new Population<CharGene>(totalPop, new CharGenome(target.Length, mutation, target, 00, 50));
            // 2: Calculate fitness for N agents
            pop.calcFitness();
        }

        static void update()
        {
            generation++;
            // 3: Reproduction / Selection
            pop.naturalSelection();
            // 3.1: Pick 2 parents
            // 3.2: Make a new agent
            // 3.2.1: Crossover
            // 3.2.2: Mutation
            // 3.2.3: Add agent to new pop
            // 4: Replace old pop with new pop
            // 5: Calculate fitness for N agents
            pop.generate();
            // 6: return to 3
        }

        static void updateStats()
        {
            best = pop.pop[0];
            avgFit = 0;

            foreach (DNA<CharGene> agent in pop.pop)
            {
                avgFit += agent.fitness;
                if (best.fitness < agent.fitness) best = agent;
            }
            avgFit /= pop.pop.Length;
        }

        static void displayInfo()
        {
            Console.WriteLine(new String('\n', Console.WindowHeight - 9)+
                "Best Fitness: " + best.fitness + "\n" +
                "Best phrase: " + CharGenome.getGenomeValue(best) + "\n" +
                "Total generations: " + generation + "\n" +
                "Average fitness: " + avgFit + "\n" +
                "total population: " + pop.pop.Length + "\n" +
                "mutation rate: " + pop.pop[0].genome.mutationRate + "\n" +
                "slows: " + pop.genome.slowDownTop + "\n" +
                "clones: " + pop.genome.clonesToKeep + "\n" +
                "Command: ");
        }
    }
}
