using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Types;

namespace Checkpoint01.Interfaces
{
    public interface ICandy : ISugary, IFlour
    {
        CandyTypes CandyType { get; }
        DateTime ExpirationDate { get; }
    }
}
