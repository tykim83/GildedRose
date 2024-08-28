using FluentAssertions;
using GildedRose.Factories;
using GildedRose.Models;
using GildedRose.Updaters;
using System;
using Xunit;

namespace GildedRoseTests.Factories;

public class ItemUpdaterFactoryTests
{
    [Theory]
    [InlineData("Aged Brie", typeof(AgedBrieUpdater))]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", typeof(BackstagePassUpdater))]
    [InlineData("Conjured Mana Cake", typeof(ConjuredItemUpdater))]
    [InlineData("Sulfuras, Hand of Ragnaros", typeof(SulfurasUpdater))]
    [InlineData("Normal Item", typeof(DefaultItemUpdater))]
    public void GIVEN_CreateUpdater_WHEN_CalledWithItem_THEN_ShouldReturnCorrectUpdater(string itemName, Type expectedUpdaterType)
    {
        // Arrange
        var factory = new ItemUpdaterFactory();
        var item = new Item(itemName, 0, 0);

        // Act
        var updater = factory.CreateUpdater(item);

        // Assert
        updater.Should().BeOfType(expectedUpdaterType);
    }
}
