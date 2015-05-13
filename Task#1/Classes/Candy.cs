using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;

namespace Checkpoint01.Classes
{
    public class Candy : ICandy
    {
        public double Sugar { get; set; }
        public double FoodValue { get; set; }
        public double Weight { get; set; }
        public double Flour { get; set; }
        public CandyTypes CandyType { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
