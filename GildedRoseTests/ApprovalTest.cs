using GildedRoseKata;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace GildedRoseTests;

public class ApprovalTest
{
    [Fact]
    public Task ThirtyDays()
    {
        // Arrange
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader("a\n"));

        // Act
        Program.Main(["30"]);

        // Assert
        var output = fakeOutput.ToString();
        return Verifier.Verify(output);
    }
}
