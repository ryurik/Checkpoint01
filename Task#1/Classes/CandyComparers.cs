using System;
using System.Collections.Generic;
using Checkpoint01.Interfaces;
using Checkpoint01;

namespace Checkpoint01.Classes
{
    #region CandyComparer
    class CandyComparerBySugar : IComparer<ICandy>
    {
        public int Compare(ICandy x, ICandy y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.Sugar - y.Sugar) < Program.Precision) ? 0 : (x.Sugar > y.Sugar) ? 1 : -1;
        }
    }

    class CandyComparerByWeight : IComparer<ICandy>
    {
        public int Compare(ICandy x, ICandy y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.Weight - y.Weight) < Program.Precision) ? 0 : (x.Weight > y.Weight) ? 1 : -1;
        }
    }

    class CandyComparerByFoodValue : IComparer<ICandy>
    {
        public int Compare(ICandy x, ICandy y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.FoodValue - y.FoodValue) < Program.Precision) ? 0 : (x.FoodValue > y.FoodValue) ? 1 : -1;
        }
    }
    #endregion

    #region CandySetComparer
    class CandySetComparerBySugar : IComparer<ISweets>
    {
        public int Compare(ISweets x, ISweets y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs((x as CandySet).Sugar - (y as CandySet).Sugar) < Program.Precision) ? 0 : ((x as CandySet).Sugar > (y as CandySet).Sugar) ? 1 : -1;
        }
    }
    class CandySetComparerByExpirationDate : IComparer<ISweets>
    {
        public int Compare(ISweets x, ISweets y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (DateTime.Compare((x as CandySet).ExpirationDate, (y as CandySet).ExpirationDate));
        }
    }

    class CandySetComparerByWeight : IComparer<ISweets>
    {
        public int Compare(ISweets x, ISweets y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.Weight - y.Weight) < Program.Precision) ? 0 : (x.Weight > y.Weight) ? 1 : -1;
        }
    }
    class CandySetComparerByFlour : IComparer<ISweets>
    {
        public int Compare(ISweets x, ISweets y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs((x as CandySet).Flour - (y as CandySet).Flour) < Program.Precision) ? 0 : ((x as CandySet).Flour > (y as CandySet).Flour) ? 1 : -1;
        }
    }

    class CandySetComparerByFoodValue : IComparer<ISweets>
    {
        public int Compare(ISweets x, ISweets y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.FoodValue - y.FoodValue) < Program.Precision) ? 0 : (x.FoodValue > y.FoodValue) ? 1 : -1;
        }
    }
    #endregion
}
