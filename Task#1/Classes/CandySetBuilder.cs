using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    class CandySetBuilder
    {
        public string CandySetName { get; set; }
        public ICollection<ISweets> CandySet {get; set;}

        private CandySet _candySet = new CandySet();

        public CandySetBuilder()
        {
            CandySet = new List<ISweets>();  
        }

        public CandySet Construct(string fileName = "CandySet.xml")
        {
            ConstructCandySet(fileName);
            return _candySet;
        }

        protected void ConstructCandySet(string fileName = "CandySet.xml")
        {

            _candySet.CandySetName = this.CandySetName;
            if (fileName != "")
            {
                LoadFormXml(fileName);
            }
            else
            {
                FillCandySet();
            }
        }

        private void LoadFormXml(string fileName = "CandySet.xml")
        {
            _candySet = Serializer.LoadFromXml<CandySet>(fileName);
        }

        protected void FillCandySet(string fileName = "CandySet.bin")
        {
            _candySet.Clear();
            foreach (var c in CandySet)
            {
                _candySet.Add((CandyForSet)c);   
            }
        }

    }
}
