using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Viv
{
    public sealed partial class MainWindow : Window
    {
        private readonly AppWindow appWindow;
        public MainWindow()
        {
            InitializeComponent();

            appWindow = GetAppWindowForCurrentWindow();
            appWindow.SetPresenter(AppWindowPresenterKind.Default);
            ExtendsContentIntoTitleBar = false;

        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(windowId);
        }
    }
}
