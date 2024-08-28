using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class ItemUpdatersTests
{
    [Theory]
    [InlineData(10, 40, 41)] // Aged Brie, quality increases by 1
    [InlineData(-1, 40, 42)] // Aged Brie, quality increases by 2 when expired (sellIn < 0)
    [InlineData(10, 50, 50)] // Aged Brie, quality should not exceed 50
    public void GIVEN_AgedBrieUpdater_WHEN_ItemUpdated_THEN_ShouldUpdateQualityCorrectly(
        int sellIn,
        int quality,
        int expectedQuality)
    {
        // Arrange
        var item = new Item("Aged Brie", sellIn, quality);

        // Act
        var updatedItem = ItemUpdaters.AgedBrieUpdater(item);

        // Assert
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData(11, 20, 21)] // Backstage Passes, quality increases by 1
    [InlineData(9, 20, 22)] // Backstage Passes, quality increases by 2
    [InlineData(4, 20, 23)]  // Backstage Passes, quality increases by 3
    [InlineData(1, 49, 50)]  // Backstage Passes, quality should not exceed 50
    [InlineData(-1, 20, 0)]  // Backstage Passes, quality drops to 0 after concert (sellIn < 0)
    public void GIVEN_BackstagePassUpdater_WHEN_ItemUpdated_THEN_ShouldUpdateQualityCorrectly(
        int sellIn,
        int quality,
        int expectedQuality)
    {
        // Arrange
        var item = new Item("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

        // Act
        var updatedItem = ItemUpdaters.BackstagePassUpdater(item);

        // Assert
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData(10, 20, 18)] // Conjured Item, quality decreases by 2
    [InlineData(-1, 20, 16)] // Conjured Item, quality decreases by 4 when expired (sellIn < 0)
    [InlineData(10, 1, 0)]   // Conjured Item, quality should not be negative
    public void GIVEN_ConjuredItemUpdater_WHEN_ItemUpdated_THEN_ShouldUpdateQualityCorrectly(
        int sellIn,
        int quality,
        int expectedQuality)
    {
        // Arrange
        var item = new Item("Conjured Mana Cake", sellIn, quality);

        // Act
        var updatedItem = ItemUpdaters.ConjuredItemUpdater(item);

        // Assert
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData(10, 20, 19)] // Default Item, quality decreases by 1
    [InlineData(-1, 20, 18)] // Default Item, quality decreases by 2 when expired (sellIn < 0)
    [InlineData(10, 1, 0)]   // Default Item, quality should not be negative
    public void GIVEN_DefaultItemUpdater_WHEN_ItemUpdated_THEN_ShouldUpdateQualityCorrectly(
        int sellIn,
        int quality,
        int expectedQuality)
    {
        // Arrange
        var item = new Item("Normal Item", sellIn, quality);

        // Act
        var updatedItem = ItemUpdaters.DefaultItemUpdater(item);

        // Assert
        updatedItem.Quality.Should().Be(expectedQuality);
    }

    [Theory]
    [InlineData(10, 80, 80)] // Sulfuras, no change in quality
    [InlineData(-1, 80, 80)] // Sulfuras, no change in quality
    public void GIVEN_SulfurasUpdater_WHEN_ItemUpdated_THEN_ShouldNotChange(
        int sellIn,
        int quality,
        int expectedQuality)
    {
        // Arrange
        var item = new Item("Sulfuras, Hand of Ragnaros", sellIn, quality);

        // Act
        var updatedItem = ItemUpdaters.SulfurasUpdater(item);

        // Assert
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
