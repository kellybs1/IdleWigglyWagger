
using System;
using System.Drawing;


namespace IdleWigglyWagger.Source
{
    public abstract class PositionCalculator( string displayName ) : IPositionCalculator
    {
        private string _displayName = displayName;

        protected Point _currentPosition;

        public string DisplayName => _displayName;

        // Public methods
        ///////////////////////////////

        public virtual Point GetNextPosition( Point currentPosition, int minMovementLimit, int maxMovementLimit )
        {
            throw new NotImplementedException();
        }
    }
}
