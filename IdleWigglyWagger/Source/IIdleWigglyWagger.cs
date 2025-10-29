using IdleWigglyWagger.Source;
using System.Collections.Generic;
using System.ComponentModel;


namespace IdleWigglyWagger
{
    public interface IIdleWigglyWagger
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool MouseMovementIsEnabled { get; set; }

        public List<IPositionCalculator> PositionCalculators { get; }

        public IPositionCalculator SelectedPositionCalculator { get; set; }
    }
}
