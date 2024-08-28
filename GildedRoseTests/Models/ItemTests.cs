using FluentAssertions;
using GildedRose.Models;
using Xunit;

namespace GildedRoseTests.Models
{
    public class ItemTests
    {
        [Theory]
        [InlineData("Aged Brie", 10, 40, 41)] // Aged Brie, quality increases by 1
        [InlineData("Aged Brie", -1, 40, 42)]  // Aged Brie, quality increases by 2 when expired
        [InlineData("Aged Brie", 10, 50, 50)] // Aged Brie, quality should not exceed 50
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 11, 20, 21)] // Backstage Passes, quality increases by 1
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 9, 20, 22)] // Backstage Passes, quality increases by 2
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 4, 20, 23)]  // Backstage Passes, quality increases by 3
        [InlineData("Backstage passes to a TAFKAL80ETC concert", -1, 20, 0)]   // Backstage Passes, quality drops to 0 after concert
        [InlineData("Conjured Mana Cake", 10, 20, 18)]  // Conjured Item, quality decreases by 2
        [InlineData("Conjured Mana Cake", -1, 20, 16)]   // Conjured Item, quality decreases by 4 when expired
        [InlineData("Normal Item", 10, 20, 19)]        // Default Item, quality decreases by 1
        [InlineData("Normal Item", -1, 20, 18)]         // Default Item, quality decreases by 2 when expired
        [InlineData("Sulfuras, Hand of Ragnaros", 10, 80, 80)] // Sulfuras, no change in quality or sellIn
        public void GIVEN_Item_WHEN_UpdateQuality_THEN_ShouldUpdateQualityCorrectly(string name, int sellIn, int quality, int expectedQuality)
        {
            // Arrange
            var item = new Item(name, sellIn, quality);

            // Act
            var updatedItem = item.UpdateQuality();

            // Assert
            updatedItem.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData("Aged Brie", 10, 9)] // Normal item, sellIn decreases by 1
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 4)] // Normal item, sellIn decreases by 1
        [InlineData("Conjured Mana Cake", 3, 2)] // Normal item, sellIn decreases by 1
        [InlineData("Sulfuras, Hand of Ragnaros", 10, 10)] // Sulfuras, sellIn does not change
        public void GIVEN_Item_WHEN_DecreaseSellIn_THEN_ShouldDecreaseSellInCorrectly(string name, int sellIn, int expectedSellIn)
        {
            // Arrange
            var item = new Item(name, sellIn, 0);

            // Act
            var updatedItem = item.DecreaseSellIn();

            // Assert
            updatedItem.SellIn.Should().Be(expectedSellIn);
        }

        [Theory]
        [InlineData("Aged Brie", 10, 40, "Aged Brie, 10, 40")]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 20, "Backstage passes to a TAFKAL80ETC concert, 5, 20")]
        [InlineData("Conjured Mana Cake", 0, 6, "Conjured Mana Cake, 0, 6")]
        [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, "Sulfuras, Hand of Ragnaros, -1, 80")]
        public void GIVEN_Item_WHEN_Display_THEN_ShouldReturnCorrectDisplayString(string name, int sellIn, int quality, string expectedDisplay)
        {
            // Arrange
            var item = new Item(name, sellIn, quality);

            // Act
            var display = item.Display();

            // Assert
            display.Should().Be(expectedDisplay);
        }
    }
}
