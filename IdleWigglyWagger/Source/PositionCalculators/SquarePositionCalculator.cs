using System.Drawing;

namespace IdleWigglyWagger.Source.PositionCalculators
{
    public class SquarePositionCalculator() : PositionCalculator( "Square" )
    {
        private enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        Direction _currentDirection = Direction.Up;


        // Public methods
        ///////////////////////////////

        public override Point GetNextPosition( Point currentPosition, int minMovementLimit, int maxMovementLimit )
        {
            _currentPosition = currentPosition;

            int newX = _currentPosition.X;
            int newY = _currentPosition.Y;

            // Lazy doubling it to make the square bigger
            maxMovementLimit = maxMovementLimit * 2;

            // Move the cursor relative to the current direction
            // Set the current direction to the next direction for the next iteration
            switch ( _currentDirection )
            {
                case Direction.Up:
                    newY = _currentPosition.Y -= maxMovementLimit;
                    _currentDirection = Direction.Right;
                    break;

                case Direction.Right:
                    newX = _currentPosition.X + maxMovementLimit;
                    _currentDirection = Direction.Down;
                    break;

                case Direction.Down:
                    newY = _currentPosition.Y + maxMovementLimit;
                    _currentDirection = Direction.Left;
                    break;

                case Direction.Left:
                    newX = _currentPosition.X - maxMovementLimit;
                    _currentDirection = Direction.Up;
                    break;
            }

            return new Point( newX, newY );
        }
    }

}
