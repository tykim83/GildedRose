using GildedRose.Extensions;
using GildedRose.Models;
using System.Collections.Generic;

namespace GildedRose.Services;

public class GildedRoseService : IGildedRoseService
{
    public ICollection<Item> UpdateQuality(ICollection<Item> items)
    {
        var updatedItems = new List<Item>();

        foreach (Item item in items)
        {
            // Skip when Item is Sulfuras, Hand of Ragnaros
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                updatedItems.Add(item);
                continue;
            }

            // Reduce SellIn
            item.ReduceSellIn();

            // Calculate the change in Quality
            int qualityChange = item.Name switch
            {
                "Aged Brie" => item.IsExpired() ? 2 : 1,
                "Backstage passes to a TAFKAL80ETC concert" => CalculateBackstagePassQualityChange(item),
                "Conjured Mana Cake" => item.IsExpired() ? -4 : -2,
                _ => item.IsExpired() ? -2 : -1
            };

            // Apply the calculated Quality change
            item.AdjustQuality(qualityChange);

            // Add the updated item to the new list
            updatedItems.Add(item);
        }

        return updatedItems;
    }

    private static int CalculateBackstagePassQualityChange(Item item)
    {
        if (item.IsExpired())
            return -item.Quality;
        if (item.SellIn < 5)
            return 3;
        if (item.SellIn < 10)
            return 2;
        return 1;
    }
}
