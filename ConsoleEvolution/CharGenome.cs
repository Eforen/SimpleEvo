using Evolution.DNA;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEvolution
{
    class CharGenome : Genome<CharGene>
    {
        public override int slowDownTop {
            get { return _slow; }
            set { _slow = value; }
        }
        public override int clonesToKeep {
            get { return _clones; }
            set { _clones = value; }
        }

        public string target { get; set; }
        public CharGenome(int length, float mutationRate, string target, int slow=0, int clones = 0)
        {
            this._clones = clones;
            this._len = length;
            this._rate = mutationRate;
            this.target = target;
            this._slow = slow;
        }
        public static readonly string chars = "abcdefghijklmnopqrstuvwxyz.,!? ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private int _len;
        private float _rate;
        private int _slow;
        private int _clones;

        public override int length { get { return _len; } }

        public override float mutationRate { get { return _rate; } set { _rate = value; } }

        public override CharGene getRandomGene()
        {
            return new CharGene(chars[rand.Next(0, chars.Length)]);
        }

        public static string getGenomeValue(DNA<CharGene> dna)
        {
            string r = "";
            foreach (CharGene gene in dna.genes)
            {
                r += gene.value;
            }
            return r;
        }

        public override float getFitness(DNA<CharGene> dna)
        {
            int score = 0;
            for (int i = 0; i < dna.genes.Length; i++)
            {
                if (dna.genes[i].value == target[i])
                    score++;
            }
            return score;
            //return (float)(target.Length) / (float)(score);
        }
    }
}
