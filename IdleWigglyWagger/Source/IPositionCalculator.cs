
using System.Drawing;


namespace IdleWigglyWagger.Source
{
    public interface IPositionCalculator
    {
        public string DisplayName { get; }

        public Point GetNextPosition( Point currentPosition, int minMovementLimit, int maxMovementLimit );
    }
}
