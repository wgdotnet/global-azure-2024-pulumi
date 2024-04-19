using System.Collections.Immutable;
using Pulumi.Testing;

public class Mocks : IMocks
{
    public Task<(string? id, object state)> NewResourceAsync(MockResourceArgs args)
    {
        var outputs = ImmutableDictionary<string, object>.Empty
            .Concat(args.Inputs)
            .ToImmutableDictionary();

        // todo: add relevant output overrides here
        // outputs = outputs.Add("key", "value");

        return Task.FromResult<(string?, object)>((args.Name, outputs));
    }

    public Task<object> CallAsync(MockCallArgs args)
    {
        return Task.FromResult((object)args.Args);
    }
}