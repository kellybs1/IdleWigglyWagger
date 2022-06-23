using System.Threading;
using System.ComponentModel;

namespace IdleWigglyWagger
{
    public class WigglyWaggerApp : IIdleWigglyWagger, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Private members
        ///////////////////////////////

        private const int _MouseMovementUpdateUpdateTickRateInMilliseconds = 50;

        private Timer _mouseMovementUpdateTimer;

        private bool _mouseMovementIsEnabled = false;


        // Constructors
        ///////////////////////////////

        public WigglyWaggerApp()
        {
            _mouseMovementUpdateTimer = new Timer( onMouseMovementTick, null, Timeout.Infinite, Timeout.Infinite );
        }


        // Public Members
        ///////////////////////////////

        public bool MouseMovementIsEnabled
        {
            get => _mouseMovementIsEnabled;
            set
            {
                if ( _mouseMovementIsEnabled != value )
                {
                    updateTimerState( ref _mouseMovementUpdateTimer, value, _MouseMovementUpdateUpdateTickRateInMilliseconds );
                    _mouseMovementIsEnabled = value;
                }

                PropertyHelper.InvokePropertyChanged( this, ref PropertyChanged, nameof( MouseMovementIsEnabled ) );
            }
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
