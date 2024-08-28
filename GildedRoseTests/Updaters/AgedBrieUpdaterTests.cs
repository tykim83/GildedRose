using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class AgedBrieUpdaterTests
{
    [Theory]
    [InlineData("Aged Brie", 10, 20, 9, 21)]  // Aged Brie increases by 1
    [InlineData("Aged Brie", 0, 20, -1, 22)]  // Increases by 2 after sell date
    [InlineData("Aged Brie", 10, 50, 9, 50)]  // Quality should not go above 50
    public void GIVEN_AgedBrieUpdater_WHEN_ItemUpdated_THEN_ShouldIncreaseQualityCorrectly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var updater = new AgedBrieUpdater();

        // Act
        var updatedItem = updater.UpdateItem(item);

        // Assert
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
