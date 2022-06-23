using System.ComponentModel;


namespace IdleWigglyWagger
{
    public interface IIdleWigglyWagger
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool MouseMovementIsEnabled { get; set; }
    }
}
