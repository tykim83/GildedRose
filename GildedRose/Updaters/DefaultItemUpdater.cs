using GildedRose.Extensions;
using GildedRose.Models;

namespace GildedRose.Updaters;

public class DefaultItemUpdater : IItemUpdater
{
    public Item UpdateItem(Item item)
    {
        item.ReduceSellIn();

        int qualityChange = item.IsExpired() ? -2 : -1;
        item.AdjustQuality(qualityChange);

        return item;
    }
}
