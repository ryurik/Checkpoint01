using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    public class CandySet :ICollection<Candy> // набор конфет
    {
        private readonly ICollection<Candy> _candy = new List<Candy>();

        public string CandySetName { get; set; }
        public DateTime ExpirationDate // находим минимальный срок хранения
        {
            get {return _candy.Min( new Func<Candy, DateTime>());} 
            set { ; }
        }
        public int Quntity { get; set; }


        #region ICollection<ICandy>
        public void Add(Candy item)
        {
            _candy.Add(item);
        }

        public void Clear()
        {
            _candy.Clear();
        }

        public bool Contains(Candy item)
        {
            return Contains(item);
        }

        public void CopyTo(Candy[] array, int arrayIndex)
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

        public bool Remove(Candy item)
        {
            return _candy.Remove(item);
        }

        public IEnumerator<Candy> GetEnumerator()
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
