using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Interfaces;
using Checkpoint01.Classes;

namespace Checkpoint01.Classes
{
    public enum XmasGiftSortType
    {
        Unsorted,
        BySugar,            // сортировка по сахару в наборах
        ByFoodValue,        // по энерг ценности
        ByWeight,           // по весу
        ByFlour,            // по углеводам
        ByExpirationDate    // по сроку годности
    }
    public class XmasGift : ICollection<ISweets> // CandySet
    {
        private ICollection<ISweets> _items = new List<ISweets>();

        private XmasGiftSortType _sortType;

        public int Weigh
        {
            get { return _items.Sum(x => (int)x.Weight); }
        }

        public XmasGiftSortType SortedBy
        {
            get { return _sortType; }
            set
            {
                switch (value)
                {
                    case XmasGiftSortType.Unsorted:
                        _sortType = XmasGiftSortType.Unsorted;
                        break;

                    case XmasGiftSortType.ByExpirationDate:
                        _sortType = XmasGiftSortType.ByExpirationDate;
                        Sort(new CandySetComparerByExpirationDate());
                        break;

                    case XmasGiftSortType.ByFlour:
                        _sortType = XmasGiftSortType.ByFlour;
                        Sort(new CandySetComparerByFlour());
                        break;

                    case XmasGiftSortType.ByFoodValue:
                        _sortType = XmasGiftSortType.ByFoodValue;
                        Sort(new CandySetComparerByFoodValue());
                        break;

                    case XmasGiftSortType.BySugar:
                        _sortType = XmasGiftSortType.BySugar;
                        Sort(new CandySetComparerBySugar());
                        break;

                    case XmasGiftSortType.ByWeight:
                        _sortType = XmasGiftSortType.ByWeight;
                        Sort(new CandySetComparerByWeight());
                        break;
                }
            }
        }


        #region Sort
        public void SortByExpirationDate()
        {
            SortedBy = XmasGiftSortType.ByExpirationDate;
        }
        public void SortByFlour()
        {
            SortedBy = XmasGiftSortType.ByFlour;
        }

        public void SortByFoodValue()
        {
            SortedBy = XmasGiftSortType.ByFoodValue;
        }
        public void SortBySugar()
        {
            SortedBy = XmasGiftSortType.BySugar;
        }
        public void SortByWeight()
        {
            SortedBy = XmasGiftSortType.ByWeight;
        }
        //////////////////////////
        protected void Sort(IComparer<ISweets> comparer)
        {
            var newList = _items.ToList();
            newList.Sort(comparer);
            _items = newList;
        }
        #endregion

        #region ICollection<ICandy>

        public void Add(ISweets item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(ISweets item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(ISweets[] array, int arrayIndex)
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

        public bool Remove(ISweets item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<ISweets> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        public IEnumerable<ISweets> GetXmasGiftForExpirationDate(DateTime startDate, DateTime endDate)
        {
            foreach (var i in _items)
            {
                if ((i as CandySet).ExpirationDate >= startDate && (i as CandySet).ExpirationDate <= endDate)
                {
                    yield return i;
                }
            }
        }
    }
}
