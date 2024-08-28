using GildedRose.Models;
using GildedRose.Shared;
using GildedRose.Updaters;

namespace GildedRose.Factories;

public class ItemUpdaterFactory : IItemUpdaterFactory
{
    public IItemUpdater CreateUpdater(Item item)
    {
        return item.Name switch
        {
            Constants.AgedBrie => new AgedBrieUpdater(),
            Constants.BackstagePass => new BackstagePassUpdater(),
            Constants.ConjuredManaCake => new ConjuredItemUpdater(),
            Constants.Sulfuras => new SulfurasUpdater(),
            _ => new DefaultItemUpdater()
        };
    }
}
