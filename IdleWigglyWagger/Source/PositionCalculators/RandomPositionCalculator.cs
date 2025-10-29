
using System;
using System.Drawing;

namespace IdleWigglyWagger.Source.PositionCalculators
{
    public class RandomPositionCalculator( Random random ) : PositionCalculator( "Random" )
    {
        private readonly Random _random = random;


        // Public methods
        ///////////////////////////////

        public override Point GetNextPosition( Point currentPosition, int minMovementLimit, int maxMovementLimit )
        {
            _currentPosition = currentPosition;

            // Randomly choose the size of the next movement
            var xStepSize = _random.Next( minMovementLimit, maxMovementLimit );
            var yStepSize = _random.Next( minMovementLimit, maxMovementLimit );

            // Decide on a 0/1 random whether to move to the positive or negative
            var newX = ( _random.Next( 2 ) != 0 ) ? ( _currentPosition.X + xStepSize ) : ( _currentPosition.X - xStepSize );
            var newY = ( _random.Next( 2 ) != 0 ) ? ( _currentPosition.Y + yStepSize ) : ( _currentPosition.Y - yStepSize );

            return new Point( newX, newY );
        }
    }
}
