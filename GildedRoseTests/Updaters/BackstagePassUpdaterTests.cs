using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class BackstagePassUpdaterTests
{
    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 11, 20, 10, 21)]  // Increases by 1
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 20, 9, 22)]  // Increases by 2
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 20, 4, 23)]  // Increases by 3
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 49, 0, 50)]  // Quality should not go above 50
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 20, -1, 0)]  // Drops to 0 after concert
    public void GIVEN_BackstagePassUpdater_WHEN_ItemUpdated_THEN_ShouldUpdateQualityCorrectly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var updater = new BackstagePassUpdater();

        // Act
        var updatedItem = updater.UpdateItem(item);

        // Assert
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
