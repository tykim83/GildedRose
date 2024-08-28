using GildedRose.Models;
using System;

namespace GildedRose.Updaters;

public static class ItemUpdaters
{
    // Function for Aged Brie
    public static Func<Item, Item> AgedBrieUpdater = item =>
    {
        // Aged Brie quality increases by 1 or 2 if expired
        var qualityChange = item.IsExpired() ? 2 : 1;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    // Function for Backstage Passes
    public static Func<Item, Item> BackstagePassUpdater = item =>
    {
        // Determine quality change based on the SellIn value
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

    // Function for Conjured Items
    public static Func<Item, Item> ConjuredItemUpdater = item =>
    {
        // Conjured items degrade twice as fast as normal items
        var qualityChange = item.IsExpired() ? -4 : -2;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    // Function for Default Items
    public static Func<Item, Item> DefaultItemUpdater = item =>
    {
        // Normal items degrade by 1 or 2 if expired
        var qualityChange = item.IsExpired() ? -2 : -1;
        return item with { Quality = ClampQuality(item.Quality + qualityChange) };
    };

    // Function for Sulfuras (no quality change)
    public static Func<Item, Item> SulfurasUpdater = item =>
    {
        // Sulfuras does not change its quality or sell-in value
        return item; // No modification
    };

    // Method to clamp the quality within the valid range
    private static int ClampQuality(int quality) => Math.Clamp(quality, 0, 50);
}
