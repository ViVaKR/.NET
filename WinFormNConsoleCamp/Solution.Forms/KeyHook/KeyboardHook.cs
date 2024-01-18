using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyHook
{
    public class KeyboardHook : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Window _window;

        private int _currentId;

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private class Window : NativeWindow, IDisposable
        {
            public int Id { get; set; }

            private static readonly int WM_HOTKEY = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    KeyModifiers modifier = (KeyModifiers)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    KeyPressed_Window?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed_Window;

            #region IDisposable Members
            public void Dispose()
            {
                DestroyHandle();
            }
            #endregion
        }

        public KeyboardHook()
        {
            _currentId = 0;
            _window = new Window();
            _window.KeyPressed_Window += (s, e) => KeyPressed?.Invoke(this, e);
        }

        /// <summary>
        /// Registers a hot key in the System.
        /// </summary>
        /// <param name="modifier"></param>
        /// <param name="key"></param>
        public void RegisterHotKey(KeyModifiers modifier, Keys key)
        {
            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
            {
                throw new InvalidOperationException($"( {_currentId} ) Register the hot key Fail.");
            }
            _currentId++;
        }

        public void Unregister()
        {
            for (int i = _currentId; i >= 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }
        }
        public void Dispose()
        {
            Unregister();
            _window.Dispose();
        }
    }
}
