using BlazorApp.Client;

namespace BlazorApp;

public class ServerExplainer : IRenderModeExplainer
{
    public string WhereAmI() => "This was rendered in the server project.";
}
