using FluentAssertions;
using GildedRose.Models;
using GildedRose.Services;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests.Services;

public class GildedRoseTests
{
    [Fact]
    public void GIVEN_Items_WHEN_UpdateQuality_THEN_ShouldUpdateAllItemsCorrectly()
    {
        // Arrange
        var items = new List<Item>
            {
                new Item("Aged Brie", 10, 40),
                new Item("Backstage passes to a TAFKAL80ETC concert", 5, 20),
                new Item("Conjured Mana Cake", 3, 6),
                new Item("Sulfuras, Hand of Ragnaros", 0, 80),
                new Item("Normal Item", 0, 10)
            };

        var expectedItems = new List<Item>
            {
                new Item("Aged Brie", 9, 41), // Aged Brie: SellIn - 1, Quality + 1
                new Item("Backstage passes to a TAFKAL80ETC concert", 4, 23), // Backstage Passes: SellIn - 1, Quality + 3
                new Item("Conjured Mana Cake", 2, 4), // Conjured Item: SellIn - 1, Quality - 2
                new Item("Sulfuras, Hand of Ragnaros", 0, 80), // Sulfuras: No change
                new Item("Normal Item", -1, 8) // Default Item: SellIn - 1, Quality - 2
            };

        var service = new GildedRoseService();

        // Act
        var updatedItems = service.UpdateQuality(items);

        // Assert
        updatedItems.Should().BeEquivalentTo(expectedItems);
    }
}
