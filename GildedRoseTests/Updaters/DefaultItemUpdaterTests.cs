using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class DefaultItemUpdaterTests
{
    [Theory]
    [InlineData("Normal Item", 10, 20, 9, 19)]  // Normal item degrades by 1
    [InlineData("Normal Item", 0, 20, -1, 18)]  // Degrades by 2 after sell date
    [InlineData("Normal Item", 0, 1, -1, 0)]    // Quality should not be negative
    public void GIVEN_DefaultItemUpdater_WHEN_ItemUpdated_THEN_ShouldDegradeCorrectly(
            string name,
            int sellIn,
            int quality,
            int expectedSellIn,
            int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var updater = new DefaultItemUpdater();

        // Act
        var updatedItem = updater.UpdateItem(item);

        // Assert
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
