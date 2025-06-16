using Microsoft.AspNetCore.Components;

namespace VivFluentApp.Components.Pages;
public partial class Chat : ComponentBase
{
    private string title = "Chat";
    protected override void OnAfterRender(bool firstRender)
    {
        title = "Chat";
        base.OnAfterRender(firstRender);
    }
}
