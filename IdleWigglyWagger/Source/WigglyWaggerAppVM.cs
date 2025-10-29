
using IdleWigglyWagger.Source;

using System.Collections.Generic;
using System.ComponentModel;

namespace IdleWigglyWagger
{
    public class WigglyWaggerAppVM : INotifyPropertyChanged
    {
        // Events
        ///////////////////////////////

        public event PropertyChangedEventHandler PropertyChanged;


        // Private members
        ///////////////////////////////

        private IIdleWigglyWagger _appInterface;


        // Constructors
        ///////////////////////////////

        public WigglyWaggerAppVM( IIdleWigglyWagger app )
        {
            _appInterface = app;
            _appInterface.PropertyChanged += onAppPropertyChanged;
        }


        // Public Members
        ///////////////////////////////

        public bool MouseMovementIsEnabled
        {
            get => _appInterface.MouseMovementIsEnabled;
            set => _appInterface.MouseMovementIsEnabled = value;
        }


        // Only used for display purposes - not settable
        public string MouseMovementIsEnabledLabel => this.MouseMovementIsEnabled ? "Click to Disable Wiggling" : "Click to Enable Wiggling";

        public List<IPositionCalculator> PositionCalculators => _appInterface.PositionCalculators;

        public IPositionCalculator SelectedPositionCalculator
        {
            get => _appInterface.SelectedPositionCalculator;
            set => _appInterface.SelectedPositionCalculator = value;
        }


        // Public Methods
        ///////////////////////////////



        // Private Methods
        ///////////////////////////////



        // Event Handlers
        ///////////////////////////////
        private void onAppPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            switch ( e.PropertyName )
            {
                case nameof( IIdleWigglyWagger.MouseMovementIsEnabled ):
                    // The app's MouseMovement Property has changed - signal ViewModel change to update the view
                    PropertyHelper.InvokePropertyChanged( this, ref PropertyChanged, nameof( MouseMovementIsEnabled ) );
                    PropertyHelper.InvokePropertyChanged( this, ref PropertyChanged, nameof( MouseMovementIsEnabledLabel ) );
                    break;

                default:
                    // Do nada
                    break;
            }
        }
    }
}
