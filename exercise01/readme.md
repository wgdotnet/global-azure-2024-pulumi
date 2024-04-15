# Exercise01

## Local Environment Setup

```

```

## The state backend

Given the fact, that Pulumi is a multi-cloud IaC tool, similarily to Terraform, it needs to rely on persisting state, in order to keep track of resources that have been deployed.

Pulumi supports multiple state backends:

| Type          | Login command                                  |
| ------------- | ---------------------------------------------- |
| Local         | `pulumi login --local`                         |
| File          | `pulumi login file://~`                        |
| Azure Storage | `pulumi login azblob://my-pulumi-state-bucket` |
| AWS S3        | `pulumi login s3://my-pulumi-state-bucket`     |
| GCP GCS       | `pulumi login gs://my-pulumi-state-bucket`     |
| Pulumi Cloud  | `pulumi login`                                 |

See: [pulumi login](https://www.pulumi.com/docs/cli/commands/pulumi_login/#pulumi-login)

## Local Environment Variables

In order to avoid repeating prompts for pulumi password, you can set the following environment variable:

```bash
export PULUMI_CONFIG_PASSPHRASE=""
```

## Deploying your first state with Pulumi

Ensure you are logged in to Azure

- `az login`
- `az account set --subscription <name or id>`

Ensure you are logged in to Pulumi

- `pulumi login --local`

Star exercise01

- `cd /exercise01/src/WgDotnet.Exercise01`
- `pulumi preview --stack dev`

```terminal
pulumi preview --stack dev
Previewing update (dev):
     Type                 Name                     Plan
 +   pulumi:pulumi:Stack  WgDotnet.Exercise01-dev  create

Outputs:
    outputKey: "outputValue"

Resources:
    + 1 to create
```

- `pulumi up --stack dev`

```terminal
pulumi up --stack dev
Previewing update (dev):
     Type                 Name                     Plan
 +   pulumi:pulumi:Stack  WgDotnet.Exercise01-dev  create

Outputs:
    outputKey: "outputValue"

Resources:
    + 1 to create

info: There are no resources in your stack (other than the stack resource).

Do you want to perform this update? yes
Updating (dev):
     Type                 Name                     Status
 +   pulumi:pulumi:Stack  WgDotnet.Exercise01-dev  created (0.03s)

Outputs:
    outputKey: "outputValue"

Resources:
    + 1 created

Duration: 1s
```

## Verifying the outcome

- Ensure you're still in `/exercise01/src/WgDotnet.Exercise01`
- `pulumi stack ls --all`

```terminal
pulumi stack ls --all
NAME                                                 LAST UPDATE     RESOURCE COUNT
dev*                                                 34 seconds ago  1
```

- `cd ../../../` (to repository root)
- `pulumi stack ls --all`

> **note**
>
> When listing all stacks, outside of the project folder, full stack names will be returned

```terminal
pulumi stack ls --all
NAME                                                 LAST UPDATE    RESOURCE COUNT
organization/WgDotnet.Exercise01/dev                 2 minutes ago  1
```

## The Pulumi Stack

**Every Pulumi program is deployed to a stack. A stack is an isolated, independently configurable instance of a Pulumi program. Stacks are commonly used to denote different phases of development (such as development , staging , and production ) or feature branches (such as feature-x-dev )**

- See: [Stacks](https://www.pulumi.com/docs/concepts/stack/)
- See: [Understanding Stacks](https://www.pulumi.com/learn/building-with-pulumi/understanding-stacks/)

> :warning: The stack name is specified in one of the following formats:
>
> - `stackName`: Identifies the stack stackName in the current user account or default organization, and the project specified by the nearest Pulumi.yaml project file.
> - `orgName/stackName`: Identifies the stack stackName in the organization orgName, and the project specified by the nearest Pulumi.yaml project file.
> - `orgName/projectName/stackName`: Identifies the stack stackName in the organization orgName and the project projectName. projectName must match the project specified by the nearest Pulumi.yaml project file.

### The organization name

|           | Outside of Pulumi Cloud | Inside Pulumi Cloud |
| --------- | ----------------------- | ------------------- |
| `orgName` | `"organization"`        | configurable         |

### The project name

The `projectName` can be found in `Pulumi.yaml` under the `name` key:

```yaml
name: WgDotnet.Exercise01
runtime: dotnet
description: A minimal C# Pulumi program
config:
  pulumi:tags:
    value:
      pulumi:template: csharp
```
