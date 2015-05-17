using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Classes;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;

namespace Checkpoint01
{
    

    class Program
    {
        // Точность вычислений при сравнении
        public const double Precision = 0.0001;

        // рассматриваем Новогодний подарок как совокупность наборов конфет CandySet. Т.е. даже если 1 конфета - то мы ее помещаем в набор с кол-ом 1.
        static void Main(string[] args)
        {
            CandySet[] CandySet;

            CandySet = FillCandySet();

            


            XmasGift XmasGift = new XmasGift();
            //XmasGift.Add(new CandySet());
        }

        static public CandySet[] FillCandySet()
        {
            CandySet[] resuSet = new CandySet[10];
            resuSet[0].Add(new CandyForSet()
                            {
                                Amount = 1, 
                                CandyType = CandyTypes.SingleLayer,
                                ExpirationDate = new DateTime(2015, 06, 01),
                                Flour = 0,
                                FoodValue = 100,
                                Sugar = 10,
                                Weight = 10
                            });
            return resuSet;
        }
    }
}
