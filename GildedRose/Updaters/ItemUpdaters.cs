using GildedRose.Models;
using System;

namespace GildedRose.Updaters;

public static class ItemUpdaters
{
    public static Func<Item, Item> AgedBrieUpdater = item =>
    {
        var qualityChange = item.IsExpired() ? 2 : 1;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    public static Func<Item, Item> BackstagePassUpdater = item =>
    {
        var qualityChange = item.IsExpired() ? -item.Quality :
            item.SellIn switch
            {
                < 0 => -item.Quality,
                < 5 => 3,
                < 10 => 2,
                _ => 1
            };
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    public static Func<Item, Item> ConjuredItemUpdater = item =>
    {
        var qualityChange = item.IsExpired() ? -4 : -2;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    public static Func<Item, Item> DefaultItemUpdater = item =>
    {
        var qualityChange = item.IsExpired() ? -2 : -1;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    public static Func<Item, Item> SulfurasUpdater = item =>
    {
        return item;
    };

    private static int ClampQuality(int quality) => Math.Clamp(quality, 0, 50);
}
