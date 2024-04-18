using Pulumi;

namespace WgDotnet.Exercise02.Stack01;

public class StorageStack : Stack01
{
    public StorageStack()
    {
        /*
            todo:
            - create a storage account
            - export the PrimaryAccessKey of the StorageAccount using Output<T> and ensure it's secret
            - export the SecondaryAccessKey of the StorageAccount using Output<T> and ensure it's secret

            see:
            - https://www.pulumi.com/registry/packages/azure-native/api-docs/storage/storageaccount/
        */
    }
}