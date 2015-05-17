using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;

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

        public void SaveToFile(string fileName = "")
        {
            fileName = (fileName == "") ? "CandyClass.bin": fileName;
            var fs = new FileStream(fileName, FileMode.Create);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }
            catch (Exception e)
            {
                Console.WriteLine("*******Error while try to save candy object*******");
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        public static void Load(Candy candy, string fileName = "")
        {
            fileName = (fileName == "") ? "CandyClass.bin" : fileName;
            FileStream fs = new FileStream(fileName, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            candy = (Candy)formatter.Deserialize(fs);

            fs.Close();
        }
    }

    // Конфета для набора.
    [Serializable]
    public class CandyForSet : Candy
    {
        public int Amount { get; set; } // Вводим кол-во конфет
    }
}
