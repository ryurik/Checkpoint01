using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    [Serializable]
    public class CandySet : ICollection<CandyForSet> // набор конфет
    {
        private readonly ICollection<CandyForSet> _candy = new List<CandyForSet>();
        
        // название набора конфет
        public string CandySetName { get; set; }

        // находим минимальный срок хранения
        public DateTime ExpirationDate 
        {
            get {return _candy.Min(x => x.ExpirationDate);}
            set { new InvalidOperationException("Deprecated"); }
        }

        // кол-во наборов в подарке?
        public int Quntity { get; set; } 

        // кол-во Сахара в наборе
        public double Sugar {
            get{return _candy.Sum(x => x.Sugar * x.Amount);}
            set{ new InvalidOperationException("Deprecated"); }
        }

        // энергетическая ценность набора
        public double FoodValue
        {
            get { return _candy.Sum(x => x.FoodValue * x.Amount); }
            set { new InvalidOperationException("Deprecated"); }
        }

        // вес набора
        public double Weight
        {
            get { return _candy.Sum(x => x.Weight * x.Amount); }
            set { new InvalidOperationException("Deprecated"); }
        }

        // кол-во углеводов
        public double Flour
        {
            get { return _candy.Sum(x => x.Flour * x.Amount); }
            set { new InvalidOperationException("Deprecated"); }
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
    }
}
