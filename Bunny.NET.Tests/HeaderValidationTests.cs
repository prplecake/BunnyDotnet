using Bunny.NET.Constants;

namespace Bunny.NET.Tests;

public class HeaderValidationTests
{
    [Fact]
    public void UserAgent_Header_IsValid()
    {
        var uaHeader = Meta.UserAgent;
        Assert.NotNull(uaHeader);
    }
}
