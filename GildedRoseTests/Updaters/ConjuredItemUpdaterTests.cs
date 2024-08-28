using FluentAssertions;
using GildedRose.Models;
using GildedRose.Updaters;
using Xunit;

namespace GildedRoseTests.Updaters;

public class ConjuredItemUpdaterTests
{
    [Theory]
    [InlineData("Conjured Mana Cake", 10, 20, 9, 18)]  // Degrades by 2
    [InlineData("Conjured Mana Cake", 0, 20, -1, 16)]  // Degrades by 4 after sell date
    [InlineData("Conjured Mana Cake", 10, 1, 9, 0)]   // Quality should not be negative
    public void GIVEN_ConjuredItemUpdater_WHEN_ItemUpdated_THEN_ShouldDegradeTwiceAsFast(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var updater = new ConjuredItemUpdater();

        // Act
        var updatedItem = updater.UpdateItem(item);

        // Assert
        updatedItem.SellIn.Should().Be(expectedSellIn);
        updatedItem.Quality.Should().Be(expectedQuality);
    }
}
