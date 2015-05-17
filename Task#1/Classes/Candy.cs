using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;
using Checkpoint01;


namespace Checkpoint01.Classes
{
    // Конфета
    [Serializable]
    public class Candy : ICandy
    {
        public string CandyName { get; set; }
        public double Sugar { get; set; }       
        public double FoodValue { get; set; }
        public double Weight { get; set; }
        public double Flour { get; set; }
        public CandyTypes CandyType { get; set; }
        public DateTime ExpirationDate { get; set; }

        public static void SaveToFile(string candyName)
        {
            Random r = new Random();
            Candy candy = new Candy()
            {
                CandyName = candyName,
                //Amount = r.Next(1,5),
                CandyType = (CandyTypes)r.Next(0, 5), // CandyTypes.SingleLayer,
                ExpirationDate = new DateTime(r.Next(2015, 2020), r.Next(1, 12), 1),
                Flour = r.Next(-10, 2) > 0 ? r.NextDouble() * 100 : 0,
                FoodValue = r.NextDouble() * 1000,
                Sugar = r.NextDouble() * 100,
                Weight = r.Next(5, 20),
            };
            Serializer.SaveToXml(Path.Combine(Program.AppPath, Program.CandyData[0], Path.ChangeExtension(candyName, Program.CandyData[1])), candy);
        
        }
        public static Candy LoadFromFile(string candyName)
        {
            string fileName = Path.Combine(Program.AppPath, Program.CandyData[0], Path.ChangeExtension(candyName, Program.CandyData[1]));
            if (!File.Exists(fileName))
            {
                return null;
            }
            return Serializer.LoadFromXml<Candy>(fileName);
        }
    }

    // Конфета для набора.
    [Serializable]
    public class CandyForSet
    {
        public Candy Candy { get; set; }
        public int Amount { get; set; } // Вводим кол-во конфет
    }
}
