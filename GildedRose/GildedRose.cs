using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly ICollection<Item> _items;

    public GildedRose(ICollection<Item> items)
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
                    if (item.Quality < 50) item.Quality++;
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    continue;
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.Quality < 50) item.Quality++;
                    if (item.SellIn < 11 && item.Quality < 50) item.Quality++;
                    if (item.SellIn < 6 && item.Quality < 50) item.Quality++;
                    break;
                case "Conjured Mana Cake":
                    if (item.Quality > 0) item.Quality--;
                    if (item.Quality > 0) item.Quality--;
                    break;
                default:
                    if (item.Quality > 0) item.Quality--;
                    break;
            }

            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn--;
            }

            switch (item.Name)
            {
                case "Aged Brie":
                    if (item.SellIn < 0 && item.Quality < 50) item.Quality++;
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    continue;
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.SellIn < 0) item.Quality -= item.Quality;
                    break;
                case "Conjured Mana Cake":
                    if (item.SellIn < 0 && item.Quality > 0) item.Quality--;
                    if (item.SellIn < 0 && item.Quality > 0) item.Quality--;
                    break;
                default:
                    if (item.SellIn < 0 && item.Quality > 0) item.Quality--;
                    break;
            }
        }
    }
}
