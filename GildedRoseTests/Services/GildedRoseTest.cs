using FluentAssertions;
using GildedRose.Factories;
using GildedRose.Models;
using GildedRose.Services;
using GildedRose.Updaters;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests.Services;

public class GildedRoseTests
{
    [Theory]
    [InlineData("Aged Brie")]
    [InlineData("Backstage passes to a TAFKAL80ETC concert")]
    [InlineData("Conjured Mana Cake")]
    [InlineData("Normal Item")]
    [InlineData("Sulfuras, Hand of Ragnaros")]
    public void GIVEN_UpdateQuality_WHEN_CalledWithVariousItems_THEN_ShouldCallAppropriateUpdater(string itemName)
    {
        // Arrange
        var item = new Item(itemName, 10, 20);

        var mockFactory = new Mock<IItemUpdaterFactory>();
        var mockUpdater = new Mock<IItemUpdater>();
        mockFactory.Setup(f => f.CreateUpdater(It.IsAny<Item>())).Returns(mockUpdater.Object);
        mockUpdater.Setup(c => c.UpdateItem(item)).Returns(item);

        var gildedRoseService = new GildedRoseService(mockFactory.Object);

        // Act
        var updatedItems = gildedRoseService.UpdateQuality(new List<Item> { item });

        // Assert
        mockFactory.Verify(f => f.CreateUpdater(It.Is<Item>(i => i.Name == itemName)), Times.Once);
        mockUpdater.Verify(u => u.UpdateItem(It.Is<Item>(i => i.Name == itemName)), Times.Once);
        var updatedItem = updatedItems.Should().ContainSingle().Subject;
        updatedItem.Should().Be(item);
    }
}
