# Exercise04

## Goal

Due to Pulumi's imposed limitation, of deploying exactly one stack per Pulumi program, deploying a more complex system requires some more effort.

The goal of this is to create a simple orchestration of deployed Stacks using [NUKE](https://nuke.build/).

See:
- [When Pulumi met Nuke: a .NET love story](https://www.techwatching.dev/posts/when-pulumi-met-nuke)
- [Pulumi Automation API](https://www.pulumi.com/docs/using-pulumi/automation-api/)
- [Pulumi Automation API blog posts](https://www.pulumi.com/blog/tag/automation-api/)
- [PulumiFn](https://www.pulumi.com/docs/reference/pkg/dotnet/Pulumi.Automation/Pulumi.Automation.PulumiFn.html)

## NUKE Build

A typical NUKE build may look similar to the one presented below. Targets are defined in code and can be invoked using `dotnet nuke --target <TargetName> --<Parameter> Value`

```csharp
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Solution]
    readonly Solution Solution;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
          DotNetRestore(_ => _.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
          DotNetBuild(_ => _
            .SetProjectFile(Solution)
            .SetConfiguration(Configuration)
            .EnableNoRestore());
        });
}
```

## NUKE Support for Pulumi

NUKE contains extensive support for multiple 3rd party tools, including Pulumi. The Pulumi integration is available in the `Nuke.Common.Tools.Pulumi` namespace.

> :warning:
>
> You will need to define an environment variable containing the Pulumi passprase and pass it to NUKE, otherwise the pulumi CLI will prompt for a password and hang.
> ```csharp
> const string PulumiPassphraseEnvVar = "PULUMI_CONFIG_PASSPHRASE";
> ```

```csharp
PulumiUp(_ => _
    .SetYes(true)   // equivalent to --yes
    .SetCwd(Solution.Directory / stack)
    .SetProcessEnvironmentVariable(PulumiPassphraseEnvVar, string.Empty)
);
```

```csharp
PulumiDestroy(_ => _
    .SetYes(true)   // equivalent to --yes
    .SetCwd(Solution.Directory / stack)
    .SetProcessEnvironmentVariable(PulumiPassphraseEnvVar, string.Empty)
);
```

## Start exercise03

For the purpose of this exercise, we are going to re-use code from [exercise02](../exercise02/readme.md)

### Initialize NUKE
- navigate back to `/exercise02/src`
- Follow the [documentation](https://nuke.build/docs/introduction/) in order to install NUKE and generate a default build for the solution

### Pulumi UP

- Implement NUKE targets that will execute `pulumi up` on exercise02 stacks in order:
    - `WgDotnet.Exercise02.Stack01`
    - `WgDotnet.Exercise02.Stack02`

### Pulumi Destroy

- Implement NUKE targets that will execute `pulumi destroy` on exercise02 stacks in reverse order to the one in which they were created