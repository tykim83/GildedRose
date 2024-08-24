using GildedRose.Extensions;
using GildedRose.Models;
using System.Collections.Generic;

namespace GildedRose.Services;

public class GildedRoseService
{
    private readonly ICollection<Item> _items;

    public GildedRoseService(ICollection<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (Item item in _items)
        {
            // Skip when Item is Sulfuras, Hand of Ragnaros
            if (item.Name == "Sulfuras, Hand of Ragnaros")
                continue;

            // Reduce SellIn
            item.ReduceSellIn();

            // Calculate the change in Quality
            int qualityChange = item.Name switch
            {
                "Aged Brie" => item.IsExpired() ? 2 : 1,
                "Backstage passes to a TAFKAL80ETC concert" => item.IsExpired() ? -item.Quality : (item.SellIn < 5 ? 3 : item.SellIn < 10 ? 2 : 1),
                "Conjured Mana Cake" => item.IsExpired() ? -4 : -2,
                _ => item.IsExpired() ? -2 : -1
            };

            // Apply the calculated Quality change
            item.AdjustQuality(qualityChange);
        }
    }
}
