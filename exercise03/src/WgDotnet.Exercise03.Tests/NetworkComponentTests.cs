using Pulumi;
using Pulumi.Testing;

namespace WgDotnet.Exercise03.Tests;

public class NetworkComponentTests
{
    [Fact]
    public async void Example_Test()
    {
        // Arrange
        var mocks = new Mocks();

        // Act
        var resources = await Deployment.TestAsync(
            testMocks: mocks,
            testOptions: new TestOptions() { IsPreview = false },
            createResources: () => {
            // todo: add relevant resources here
        });

        // Assert
        // var resource = resources.OfType<TResource>().SingleOrDefault();
        // resource.Should().NotBeNull();
        // resource.Name.Should().Be("value");
    }
}