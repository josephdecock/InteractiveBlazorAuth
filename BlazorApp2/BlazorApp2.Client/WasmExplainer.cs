using BlazorApp2.Client;

public class WasmExplainer : IRenderModeExplainer
{
    public string WhereAmI() => "This was rendered in the client project.";
}
