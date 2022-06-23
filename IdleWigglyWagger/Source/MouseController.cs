using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IdleWigglyWagger
{
    public static class MouseController
    {
        // Imports for mouse interactivity
        ///////////////////////////////

        [DllImport( "user32.dll", EntryPoint = "SetCursorPos" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        private static extern bool SetCursorPos( int x, int y );

        [DllImport("user32.dll", EntryPoint = "GetCursorPos" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        private static extern bool GetCursorPos( out Point lpPoint );

        // Private members
        ///////////////////////////////


        private const int _MinMouseMovementStepInPixels = 3;
        private const int _MaxMouseMovementStepInPixels = 10;

        private static Point _currentPosition = new Point( 0, 0 );
        private static Point _previousPosition = new Point( 0, 0 );
        private static Point _previousSetPosition = new Point( 0, 0 );
        private static bool _firstLoop = true;


        private static readonly Random _random = new();

        // Public methods
        ///////////////////////////////

        public static void UpdateMousePosition()
        {
            GetCursorPos( out _currentPosition );

            if ( _firstLoop ||
                 ( _currentPosition == _previousSetPosition ) ||
                 ( _currentPosition == _previousPosition )
               )
            {
                _firstLoop = false;
                MoveInRandomDirection();
            }
            else
            {
                // mouse has been manually moved away from the place where we left it
                // consider this detection of user input - let them use the mouse by not wiggling
            }

            _previousPosition = _currentPosition;
        }


        // Private methods
        ///////////////////////////////

        private static void MoveInRandomDirection()
        {
            // Randomly choose the size of the next movement
            var xStepSize = _random.Next( _MinMouseMovementStepInPixels, _MaxMouseMovementStepInPixels );
            var yStepSize = _random.Next( _MinMouseMovementStepInPixels, _MaxMouseMovementStepInPixels );

            // Decide on a 0/1 random whether to move to the positive or negative
            var newX = ( _random.Next( 2 ) != 0 ) ? ( _currentPosition.X + xStepSize ) : ( _currentPosition.X - xStepSize );
            var newY = ( _random.Next( 2 ) != 0 ) ? ( _currentPosition.Y + yStepSize ) : ( _currentPosition.Y - yStepSize );

            _previousSetPosition.X = newX;
            _previousSetPosition.Y = newY;

            SetCursorPos( newX, newY );
        }
    }
}
