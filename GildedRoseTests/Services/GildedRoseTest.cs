using FluentAssertions;
using GildedRose.Models;
using GildedRose.Services;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests.Services;

public class GildedRoseTests
{
    [Theory]
    [InlineData("Aged Brie", 10, 40, 9, 41)]
    [InlineData("Aged Brie", 10, 50, 9, 50)] // Quality shouldn't go above 50
    [InlineData("Aged Brie", 0, 10, -1, 12)] // Aged Brie increases twice after sell date
    public void GIVEN_UpdateQuality_WHEN_CalledWithAgedBrie_THEN_ShouldUpdateCorrectly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var agedBrie = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var gildedRoseService = new GildedRoseService();

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { agedBrie });

        // Assert
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", 10, 80, 10, 80)]
    [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)]
    public void GIVEN_UpdateQuality_WHEN_CalledWithSulfuras_THEN_ShouldNotChange(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var sulfuras = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var gildedRoseService = new GildedRoseService();

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { sulfuras });

        // Assert
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 11, 20, 10, 21)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 20, 9, 22)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 20, 4, 23)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 49, 0, 50)] // Quality shouldn't go above 50
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 20, -1, 0)] // Quality drops to 0 after concert
    public void GIVEN_UpdateQuality_WHEN_CalledWithBackstagePasses_THEN_ShouldUpdateCorrectly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var backstagePass = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var gildedRoseService = new GildedRoseService();

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { backstagePass });

        // Assert
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData("Conjured Mana Cake", 10, 20, 9, 18)] // Degrades twice as fast as normal items
    [InlineData("Conjured Mana Cake", 0, 10, -1, 6)]  // Degrades twice as fast even after sell date
    [InlineData("Conjured Mana Cake", 10, 1, 9, 0)]   // Quality should not be negative
    public void GIVEN_UpdateQuality_WHEN_CalledWithConjuredItem_THEN_ShouldDegradeTwiceAsFast(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var conjuredItem = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var gildedRoseService = new GildedRoseService();

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { conjuredItem });

        // Assert
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData("Normal Item", 10, 20, 9, 19)] // Normal item degrades by 1
    [InlineData("Normal Item", 0, 20, -1, 18)] // Degrades by 2 after sell date
    [InlineData("Normal Item", 0, 1, -1, 0)]   // Quality should not be negative
    public void GIVEN_UpdateQuality_WHEN_CalledWithNormalItem_THEN_ShouldDegradeCorrectly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var normalItem = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var gildedRoseService = new GildedRoseService();

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { normalItem });

        // Assert
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
