using Checkpoint01.Types;

namespace Checkpoint01.Interfaces
{
    public interface IFruitAndBerries : ISugary
    {
        FruitAndBerriesTypes FruitAndBerriesType { get; }
    }
}
