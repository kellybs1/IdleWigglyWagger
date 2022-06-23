using System;
using System.ComponentModel;
using System.Diagnostics;

namespace IdleWigglyWagger
{
    public static class PropertyHelper
    {
        public static void InvokePropertyChanged( object invoker, ref PropertyChangedEventHandler propertyChanged, string propertyName )
        {
            try
            {
                propertyChanged?.Invoke( invoker, new PropertyChangedEventArgs( propertyName ) );
            }
            catch ( Exception e )
            {
                Debug.WriteLine( $"Exception caught: {System.Reflection.MethodBase.GetCurrentMethod()} : {e.Message}" );
            }
        }
    }
}
