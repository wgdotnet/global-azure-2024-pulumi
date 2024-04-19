# Exercise05

Pulumi can be used to enforce compliance & standarization. This can be achieved using Pulumi Policies. It is possible to create 2 types of policies: Validation Policies & Remediation Policies, which will mutate the resources being created.

See: [Get started with Pulumi policy as code](https://www.pulumi.com/docs/using-pulumi/crossguard/get-started/)

## Goal

The goal of this exercise is to familiarize you with Pulumi Crossguard, and have you write simple policies in typescript.

## Example Policies

### Useful imports

The following imports are useful when working with Policies:

```typescript
import * as azure from "@pulumi/azure-native";
import {
  PolicyPack,
  ReportViolation,
  ResourceValidationArgs,
  ResourceValidationPolicy,
  StackValidationArgs,
  StackValidationPolicy,
  validateResourceOfType,
  validateStackResourcesOfType,
} from "@pulumi/policy";
```

### Example Resource Validation Policy

`ResourceValidationPolicy` will validate resources of a given type, without any awareness of other resources in the same stack.

```typescript
const policy = {
  name: "example-policy",
  description: "TBA",
  enforcementLevel: "mandatory", // mandatory | advisory | disabled
  validateResource: validateResourceOfType(
    azure.operationalinsights.Workspace,
    (
      workspace,
      args: ResourceValidationArgs,
      reportViolation: ReportViolation
    ) => {
      if (predicate === false) {
        reportViolation(
          "A condition has failed and the policy needs to report a violation",
          args.urn
        );
      }
    }
  ),
} as ResourceValidationPolicy;
```

### Example StackValidation Policy

`StackValidationPolicy` can be used to validate all resources in the stack.

```typescript
const policy = {
  name: "example-stack-validation-policy",
  description: "TBA",
  enforcementLevel: "mandatory", // mandatory | advisory | disabled
  validateStack: (
    args: StackValidationArgs,
    reportViolation: ReportViolation
  ) => {
    if (!condition) {
      reportViolation(`${resource.urn} validation has failed`);
    }
  },
} as StackValidationPolicy;
```

### Hints

#### resource.type

Each Pulumi resource has a resource type identifier, which can be found on it's documentation page.

#### Filtering resources by type

```typescript
args.resources
  .filter(
    (resource) => resource.type === "azure-native:documentdb:DatabaseAccount"
  )
  .forEach((resource) => {
    /* policy logic */
  });
```

#### Debugging typescript policies

Not sure what properties does an object have?\
You can always `console.log(JSON.stringify(obj))`

## Start exercise05

- navigate to `/exercise05/src`

- Foreach of the subexercises below:
    - Create a separate subfolder for the policypack
    - Execute `pulumi policy new azure-typescript`
    - Edit `index.ts` to complete this exercise
    - You will be validating Stacks from `exercise02` against policies writting in this exercise.
        - `pulumi preview --policy-pack <path-to-policy-pack>,<path-to-policy-pack>`

### Simple resource-group policies

- Create a policypack that will validate all `azure-native:resources:ResourceGroup`
    - Ensure all resource group names end with `"rgp"`
    - Ensure all resource groups have a tag `exercise = 05`

### Simple storage-account policies

- Create a policypack that will validate all `azure-native:storage:StorageAccount`
    - Ensure all storage accounts have `allowBlobPublicAccess` set to `false`
    - Ensure all storage accounts have `publicNetworkAccess` set to `Disabled`

### Stack validation policy

- Create a policypack that will validate if each `azure-native:storage:StorageAccount` in the stack has a `azure-native:network:PrivateEndpoint` defined