using GildedRose.Extensions;
using GildedRose.Models;

namespace GildedRose.Updaters;

public class BackstagePassUpdater : IItemUpdater
{
    public Item UpdateItem(Item item)
    {
        item.ReduceSellIn();

        int qualityChange = CalculateBackstagePassQualityChange(item);
        item.AdjustQuality(qualityChange);

        return item;
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
