using Pulumi;

namespace WgDotnet.Exercise02.Stack01;

public class KeyVaultStack : Stack01
{
    public KeyVaultStack()
    {
        /*
            todo:
            - create a keyvault
            - create a stackreference of `WgDotnet.Exercise02.Stack01.StorageStack` to consume it's outputs
            - add the primary & secondary keys to keyvault as secrets

            see:
            - https://www.pulumi.com/registry/packages/azure-native/api-docs/keyvault/vault/
            - https://www.pulumi.com/registry/packages/azure-native/api-docs/keyvault/secret/
        */
    }
}