using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;

namespace Checkpoint01.Classes
{
    [Serializable]
    public class CandySet : ICollection<CandyForSet>, ISerializable // набор конфет
    {
        private readonly ICollection<CandyForSet> _candy = new List<CandyForSet>();
        
        // название набора конфет
        public string CandySetName { get; set; }

        // находим минимальный срок хранения
        public DateTime ExpirationDate 
        {
            get {return _candy.Min(x => x.Candy.ExpirationDate);}
            //set { new InvalidOperationException("Deprecated"); }
        }
        
        // кол-во Сахара в наборе
        public double Sugar {
            get { return _candy.Sum(x => x.Candy.Sugar * x.Amount); }
            //set{ new InvalidOperationException("Deprecated"); }
        }

        // энергетическая ценность набора
        public double FoodValue
        {
            get { return _candy.Sum(x => x.Candy.FoodValue * x.Amount); }
            //set { new InvalidOperationException("Deprecated"); }
        }

        // вес набора
        public double Weight
        {
            get { return _candy.Sum(x => x.Candy.Weight * x.Amount); }
            //set { new InvalidOperationException("Deprecated"); }
        }

        // кол-во углеводов
        public double Flour
        {
            get { return _candy.Sum(x => x.Candy.Flour * x.Amount); }
            //set { new InvalidOperationException("Deprecated"); }
        }

        public bool SaveToFile(string candySetName = "")
        {
            candySetName = (candySetName == "") ? this.CandySetName : candySetName;
            return SaveToFileCandySet(candySetName, this);
        }

        public static bool SaveToFileCandySet(string candySetName, CandySet candySet = null)
        {
            if (candySet == null)
                return false;
            try
            {
                Serializer.SaveToXml(Path.Combine(Program.AppPath, Program.CandySetData[0],Path.ChangeExtension(candySetName, Program.CandySetData[1])), candySet);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e.Message);
            }
            return false;
        }

        public static CandySet LoadFromFile(string candyName)
        {
            string fileName = Path.Combine(Program.AppPath, Program.CandyData[0], Path.ChangeExtension(candyName, Program.CandyData[1]));
            if (!File.Exists(fileName))
            {
                return null;
            }
            return Serializer.LoadFromXml<CandySet>(fileName);
        }

       #region ICollection<ICandy>
        public void Add(CandyForSet item)
        {
            _candy.Add(item);
        }

        public void Clear()
        {
            _candy.Clear();
        }

        public bool Contains(CandyForSet item)
        {
            return _candy.Contains(item);
        }

        public void CopyTo(CandyForSet[] array, int arrayIndex)
        {
            _candy.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _candy.Count; }
        }

        public bool IsReadOnly
        {
            get { return _candy.IsReadOnly; }
        }

        public bool Remove(CandyForSet item)
        {
            return _candy.Remove(item);
        }

        public IEnumerator<CandyForSet> GetEnumerator()
        {
            return _candy.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CandySetName, this._candy);
        }
    }
}
