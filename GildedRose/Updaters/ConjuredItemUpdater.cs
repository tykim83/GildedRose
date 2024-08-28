using GildedRose.Extensions;
using GildedRose.Models;

namespace GildedRose.Updaters;

public class ConjuredItemUpdater : IItemUpdater
{
    public Item UpdateItem(Item item)
    {
        item.ReduceSellIn();

        int qualityChange = item.IsExpired() ? -4 : -2;
        item.AdjustQuality(qualityChange);

        return item;
    }
}
