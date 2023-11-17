using BlazorApp.Client;

namespace BlazorApp;

public class ServerExplainer : IRenderModeExplainer
{
    public string WhereAmI() => "Rendered by server";
}
