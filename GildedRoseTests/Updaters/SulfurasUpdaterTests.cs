using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class SulfurasUpdaterTests
{
    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", 10, 80, 10, 80)]  // Sulfuras should not change
    [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)] // Sulfuras should not change
    public void GIVEN_SulfurasUpdater_WHEN_ItemUpdated_THEN_ShouldNotChange(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var updater = new SulfurasUpdater();

        // Act
        var updatedItem = updater.UpdateItem(item);

        // Assert
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
