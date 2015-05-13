using System;
using System.Collections.Generic;
using Checkpoint01.Interfaces;

namespace Checkpoint01.Classes
{
    class CandyComparerBySugar : IComparer<ICandy>
    {
        public int Compare(ICandy x, ICandy y)
        {
            if (x == null || y == null) return (y == null && x == null) ? 0 : (x != null) ? 1 : -1;
            return (Math.Abs(x.Sugar - y.Sugar) < 0.0001) ? 0 : (x.Sugar > y.Sugar) ? 1 : -1;
        }
    }
}
