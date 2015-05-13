using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    public class XmasGift : ICollection<CandySet>
    {
        private readonly ICollection<CandySet> _items = new List<CandySet>();

        #region ICollection<ICandy>

        public void Add(CandySet item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(CandySet item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(CandySet[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return _items.IsReadOnly; }
        }

        public bool Remove(CandySet item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<CandySet> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion


    }
}
