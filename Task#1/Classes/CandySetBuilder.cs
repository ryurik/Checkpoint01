using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    class CandySetBuilder
    {
        public string CandySetName { get; set; }
        public ICollection<ISweets> CandySet {get; set;}

        private CandySet candySet = new CandySet();

        public CandySet Construct()
        {
            ConstructCandySet();
            return candySet;
        }

        protected void ConstructCandySet()
        {
            candySet.CandySetName = this.CandySetName;
        }

        protected void FillCandySet()
        {
            candySet.Clear();
            foreach (CandyForSet c in CandySet)
            {
                candySet.Add(c);   
            }
        }

    }
}
