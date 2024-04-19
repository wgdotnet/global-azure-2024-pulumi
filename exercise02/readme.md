# Exercise02

## Goal

The goal of this exercise is to start getting some hands-on experience with Pulumi, using the `azure-native` provider. While doing so, you will deploy 2 stacks, and exchange information between them using a `StackReference`

## Resource names

:warning: Whenever a Pulumi resource is created, a `name` parameter is passed as the 1st argument. This parameter sets the resource's identifier, and is used to generate the resources' target name with a random suffix.

In order to avoid random resource names, pay attention to `Args` arguments passed to Pulumi resources, as they typically contain properties that set the resources' name directly.

**Example property names:**
- `ResourceName`
- `RuleName`
etc.

## StackReferences & Outputs

In order to consume outputs of a stack by another stack, a [StackReference](https://www.pulumi.com/docs/reference/pkg/dotnet/Pulumi/Pulumi.StackReference.html) can be used.

Outputs can be retrieved using `GetOutput` which will return na nullable result if an output has not been set. Alternatively `RequireOutput` can be used in order to get a non-nullable output value.

```csharp
var stackRef = new Pulumi.StackReference(stackId);
string? value1 = stackRef.GetOutput("url");
string value2 = stackRef.RequireOutput("url");
```

### Strongly-typed StackReferences

Using C# it is completely possible, to have StronglyTyped stack references:

```csharp
public class StackReference<T> : StackReference where T : Stack
{
  public static StackReference<T> Create(string? stackName = null) {

    return new StackReference<T>(typeof(T).Name, new StackReferenceArgs
    {
      Name = $"organization/{typeof(T).Assembly.GetName().Name}/{stackName}"
    });
  }

  public StackReference(string name, StackReferenceArgs args, CustomResourceOptions? options = null)
    : base(name, args, options)
  {
  }

  public Output<G> RequireOutput<G>(Expression<Func<T, Output<G>>> action) =>
    RequireOutput(((MemberExpression)action.Body).Member.Name).Apply(_ => (G)_);
}
```

**Example usage:**

```csharp
var kv = StackReference<KeyVaultStack>.Create(Pulumi.Deployment.Instance.StackName);

var vaultRef = (
      ResourceGroup: kv.RequireOutput(x => x.ResourceGroupName),
      Resource: kv.RequireOutput(x => x.Name)
      );
```

## Start exercise02

- navigate to `/exercise02/src` folder and examine both projects which are a part of the solution.
- follow the comments in order to implement the solution

