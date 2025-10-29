
using IdleWigglyWagger.Source;
using IdleWigglyWagger.Source.PositionCalculators;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

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

        // Three second leeway after stopping mouse movement
        private static int _leewayTimeInMilliseconds = 3000;
        private static bool _leewayIsActive = true;
        private static Timer _mouseMovementLeewayTimer = new Timer( ( object state ) =>
                                                                    {
                                                                        _leewayIsActive = false;
                                                                    },
                                                                    null,
                                                                    Timeout.Infinite,
                                                                    Timeout.Infinite );


        // Public members
        ///////////////////////////////

        public static IPositionCalculator PositionCalculator { get; set; }

        // Public methods
        ///////////////////////////////

        public static void ResetLeeway()
        {
            _leewayIsActive = true;
            _mouseMovementLeewayTimer.Change( _leewayTimeInMilliseconds, Timeout.Infinite );
        }

        public static void UpdateMousePosition()
        {
            GetCursorPos( out _currentPosition );

            if ( _leewayIsActive )
            {
                // do nothing, wait for the timer to expire so mouse will start wiggling then
            }
            else if ( ( _currentPosition == _previousSetPosition ) ||
                      ( _currentPosition == _previousPosition ) )
            {
                _previousSetPosition = PositionCalculator.GetNextPosition( _currentPosition, _MinMouseMovementStepInPixels, _MaxMouseMovementStepInPixels );
                SetCursorPos( _previousSetPosition.X, _previousSetPosition.Y );
            }
            else
            {
                // mouse has been manually moved away from the place where we left it
                // consider this detection of user input - let them use the mouse by not wiggling
                ResetLeeway();

            }

            _previousPosition = _currentPosition;
        }
    }
}
