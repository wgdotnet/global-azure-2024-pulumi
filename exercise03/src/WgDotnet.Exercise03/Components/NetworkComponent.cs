using Pulumi;

namespace WgDotnet.Exercise03.Components;

class NetworkComponent : Pulumi.ComponentResource
{
    // (...) Outputs

    public NetworkComponent(string name, ComponentResourceOptions opts)
        : base("wgdotnet:workshop:ex03:NetworkComponent", name, opts)
    {
        // (...) Add resource-creation logic here

        this.RegisterOutputs();
    }
}