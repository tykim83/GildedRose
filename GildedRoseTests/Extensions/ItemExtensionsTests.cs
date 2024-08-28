using FluentAssertions;
using GildedRose.Extensions;
using GildedRose.Models;
using Xunit;

namespace GildedRoseKata.Tests;

public class ItemExtensionsTests
{
    [Theory]
    [InlineData(10, 9)]
    [InlineData(1, 0)]
    [InlineData(0, -1)]
    public void GIVEN_ReduceSellIn_WHEN_Called_THEN_ShouldDecreaseSellInByOne(int initialSellIn, int expectedSellIn)
    {
        // Arrange
        var item = new Item { Name = "Test Item", SellIn = initialSellIn, Quality = 10 };

        // Act
        item.ReduceSellIn();

        // Assert
        item.SellIn.Should().Be(expectedSellIn);
    }

    [Theory]
    [InlineData(10, 1, 11)]
    [InlineData(10, -1, 9)]
    [InlineData(0, -1, 0)]
    [InlineData(49, 2, 50)]
    [InlineData(50, 1, 50)]
    [InlineData(1, -2, 0)]
    public void GIVEN_AdjustQuality_WHEN_Called_THEN_ShouldAdjustQualityWithinRange(int initialQuality, int adjustment, int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = "Test Item", SellIn = 5, Quality = initialQuality };

        // Act
        item.AdjustQuality(adjustment);

        // Assert
        item.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    public void GIVEN_IsExpired_WHEN_Called_THEN_ShouldReturnCorrectBoolean(int sellIn, bool isExpired)
    {
        // Arrange
        var item = new Item { Name = "Test Item", SellIn = sellIn, Quality = 10 };

        // Act
        var result = item.IsExpired();

        // Assert
        result.Should().Be(isExpired);
    }

    [Theory]
    [InlineData("Test Item", 10, 20, "Test Item, 10, 20")]
    [InlineData("Aged Brie", 0, 30, "Aged Brie, 0, 30")]
    [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, "Sulfuras, Hand of Ragnaros, -1, 80")]
    public void GIVEN_Display_WHEN_Called_THEN_ShouldReturnFormattedString(string name, int sellIn, int quality, string expectedOutput)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };

        // Act
        var result = item.Display();

        // Assert
        result.Should().Be(expectedOutput);
    }
}
