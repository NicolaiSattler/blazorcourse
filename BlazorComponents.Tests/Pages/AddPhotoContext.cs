using AutoFixture;
using Moq;
using MyBlazorCourse.Shared.Interface;

namespace BlazorComponents.Tests;

public class AddPhotoContext: TestContextWrapper
{
    protected Mock<IPhotoService> MockedPhotoService { get; private set; }
    protected Fixture Fixture = new();

    [TestInitialize]
    public void Initialize()
    {
	    TestContext = new Bunit.TestContext();
        MockedPhotoService = new();

        Services.AddSingleton<IPhotoService>(MockedPhotoService.Object);
    }

    [TestCleanup]
    public void Cleanup() => TestContext?.Dispose();
}