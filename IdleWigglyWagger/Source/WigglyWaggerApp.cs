using IdleWigglyWagger.Source;
using IdleWigglyWagger.Source.PositionCalculators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace IdleWigglyWagger
{
    public class WigglyWaggerApp : IIdleWigglyWagger, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Private members
        ///////////////////////////////

        private readonly Random _random = new();

        private const int _MouseMovementUpdateUpdateTickRateInMilliseconds = 100;

        private Timer _mouseMovementUpdateTimer;

        private bool _mouseMovementIsEnabled = false;

        private List<IPositionCalculator> _positionCalculators;

        // Constructors
        ///////////////////////////////

        public WigglyWaggerApp()
        {
            _mouseMovementUpdateTimer = new Timer( onMouseMovementTick, null, Timeout.Infinite, Timeout.Infinite );

            _positionCalculators =
            [
                new RandomPositionCalculator( _random ),
                new SquarePositionCalculator(),
            ];

            this.SelectedPositionCalculator = _positionCalculators.First();
        }

        // Public Members
        ///////////////////////////////

        public List<IPositionCalculator> PositionCalculators => _positionCalculators;

        public bool MouseMovementIsEnabled
        {
            get => _mouseMovementIsEnabled;
            set
            {
                if ( _mouseMovementIsEnabled != value )
                {
                    if ( value )
                    {
                        // Changed from not active to active
                        // Ensure the leeway works immediately
                        MouseController.ResetLeeway();
                    }

                    updateTimerState( ref _mouseMovementUpdateTimer, value, _MouseMovementUpdateUpdateTickRateInMilliseconds );
                    _mouseMovementIsEnabled = value;
                }

                PropertyHelper.InvokePropertyChanged( this, ref PropertyChanged, nameof( MouseMovementIsEnabled ) );
            }
        }

        public IPositionCalculator SelectedPositionCalculator
        {
            get => MouseController.PositionCalculator;
            set => MouseController.PositionCalculator = value;
        }

        // Private Methods
        ///////////////////////////////

        private void updateTimerState( ref Timer timer, bool enableTimer, int tickRate )
        {
            // Enable the timer if required and set the tick rate
            if ( enableTimer )
            {
                timer.Change( 0, tickRate );
            }
            else
            {
                // Infinite time means never ticking
                timer.Change( 0, Timeout.Infinite );
            }
        }

        // Event Handlers
        ///////////////////////////////

        private void onMouseMovementTick( object state )
        {
            MouseController.UpdateMousePosition();
        }

    }
}
