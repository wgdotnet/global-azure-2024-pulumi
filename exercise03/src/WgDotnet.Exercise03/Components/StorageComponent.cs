using Pulumi;

namespace WgDotnet.Exercise03.Components;

class StorageComponent : Pulumi.ComponentResource
{
    // (...) Outputs

    public StorageComponent(string name, ComponentResourceOptions opts)
        : base("wgdotnet:workshop:ex03:StorageComponent", name, opts)
    {
        // (...) Add resource-creation logic here

        this.RegisterOutputs();
    }
}