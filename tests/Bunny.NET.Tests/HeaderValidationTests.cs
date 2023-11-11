using Bunny.NET.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bunny.NET.Tests;

[TestClass]
public class HeaderValidationTests
{
    [TestMethod]
    public void UserAgent_Header_IsValid()
    {
        var uaHeader = Meta.UserAgent;
        Assert.IsNotNull(uaHeader);
    }
}
