using System;
using System.Windows.Forms;

namespace KeyHook
{
    public class KeyPressedEventArgs : EventArgs
    {
        public KeyModifiers Modifier { get; private set; } 

        public Keys Key { get; private set; }

        public KeyPressedEventArgs(KeyModifiers modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }
    }
}
