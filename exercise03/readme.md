# Exercise03

## Goal

The goal of this exercise is to create a component resource which will aggregate the creation of multiple sub-resources.
In addition, you will learn about working with UnitTests in Pulumi.

## Component Resources

*A [component resource](https://www.pulumi.com/docs/concepts/resources/components/) is a logical grouping of resources. Components usually instantiate a set of related resources in their constructor, aggregate them as children, and create a larger, useful abstraction that encapsulates their implementation details.*

```csharp
class MyComponent : Pulumi.ComponentResource
{
    // (...) Outputs

    public MyComponent(string name, ComponentResourceOptions opts)
        : base("wgdotnet:workshop:MyComponent", name, opts)
    {
        // (...) Add resource-creation logic here

        this.RegisterOutputs();
    }
}
```

**Usage**

```csharp
class MyStack : Pulumi.Stack
{
    public MyStack()
    {
        var options = new ComponentResourceOptions()
        {
            Protect = false;
        };

        var component = new MyComponent("myComponent", options);
    }
}
```

## UnitTests

*Pulumi programs are authored in a general-purpose language like TypeScript, Python, Go, C# or Java. The full power of each language is available, including access to tools and libraries for that runtime, including testing frameworks.

When running an update, your Pulumi program talks to the Pulumi CLI to orchestrate the deployment. The idea of unit tests is to cut this communication channel and replace the engine with mocks. The mocks respond to the commands from within the same OS process and return dummy data for each call that your Pulumi program makes.

Because mocks don’t execute any real work, unit tests run very fast. Also, they can be made deterministic because tests don’t depend on the behavior of any external system.*

See: [Unit testing Pulumi](https://www.pulumi.com/docs/using-pulumi/testing/unit/)

## Start exercise03

### Virtual Networks

- navigate to `/exercise03/src/WgDotnet.Exercise03` folder
- Create a component that will provision:
    - [VirtualNetwork](https://www.pulumi.com/registry/packages/azure-native/api-docs/network/virtualnetwork/)
    - [Subnets](https://www.pulumi.com/registry/packages/azure-native/api-docs/network/subnet/) (note: it is possible to specify subnets directly on the `VirtualNetworkArgs`)
- The component needs to abstract away the resource naming from the client
    - The virtual networks can be named using the following pattern: `wgdotnet-ex03-network-{region}-vnet`
- The component needs to be able to provision the networks in multiple regions
- The component's resouces need to be placed within a single resource group: `wgdotnet-ex03-network-rgp`
- :warning: ensure the virtual network resources are placed in correct locations

```
wgdotnet-ex03-network-polandcentral-vnet:
10.0.1.0/24
subnet-1: 10.0.1.0/26               // 62 hosts
subnet-2: 10.0.1.64/26              // 62 hosts
subnet-3: 10.0.1.128/26             // 62 hosts
private-endpoints: 10.0.1.192/26    // 62 hosts
```

```
wgdotnet-ex03-network-swedencentral-vnet:
10.0.2.0/24
subnet-1: 10.0.2.0/26               // 62 hosts
subnet-2: 10.0.2.64/26              // 62 hosts
subnet-3: 10.0.2.128/26             // 62 hosts
private-endpoints: 10.0.2.192/26    // 62 hosts
```

#### UnitTests

- Ensure that:
    - Exactly 2 virtual networks are is created
    - VirtualNetworks' names are as you expect
    - Expected amount of Subnets is created
    - Subnets names match

### Storage Account

- Create a component that will:
    - Create a [StorageAccount](https://www.pulumi.com/registry/packages/azure-native/api-docs/databoxedge/storageaccount/) in `polandcentral`
    - Create a private-endpoint tunneling to the `StorageAccount` in `polandcentral`, placed in `wgdotnet-ex03-network-polandcentral-vnet/private-endpoints`
    - Create a private-endpoint tunneling to the `StorageAccount` in `swedencentral`, placed in `wgdotnet-ex03-network-swedencentral-vnet/private-endpoints`
    - Ensure relevant Outputs are provided by the Network stack in order to provision the [PrivateEndpoints](https://www.pulumi.com/registry/packages/azure-native/api-docs/storage/privateendpointconnection/)


#### UnitTests

- Ensure that:
    - Exactly 1 storage account is created
    - Storage account's name is as you expect
    - Exactly 2 private endpoints are created
    - Both private endpoints have different location(s)