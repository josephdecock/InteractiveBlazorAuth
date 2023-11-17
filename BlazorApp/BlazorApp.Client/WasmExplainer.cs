using BlazorApp.Client;

public class WasmExplainer : IRenderModeExplainer
{
    public string WhereAmI() => "Rendered by client";
}
