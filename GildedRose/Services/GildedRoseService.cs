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
            switch (item.Name)
            {
                case "Aged Brie":
                    item.AdjustQuality(1);
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    continue;
                case "Backstage passes to a TAFKAL80ETC concert":
                    item.AdjustQuality(1);
                    if (item.SellIn < 11) item.AdjustQuality(1);
                    if (item.SellIn < 6) item.AdjustQuality(1);
                    break;
                case "Conjured Mana Cake":
                    item.AdjustQuality(-2);
                    break;
                default:
                    item.AdjustQuality(-1);
                    break;
            }

            if (item.Name != "Sulfuras, Hand of Ragnaros")
                item.ReduceSellIn();

            switch (item.Name)
            {
                case "Aged Brie":
                    if (item.IsExpired()) item.AdjustQuality(1);
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    continue;
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.IsExpired()) item.AdjustQuality(-item.Quality);
                    break;
                case "Conjured Mana Cake":
                    if (item.IsExpired()) item.AdjustQuality(-2);
                    break;
                default:
                    if (item.IsExpired()) item.AdjustQuality(-1);
                    break;
            }
        }
    }
}
