using GildedRose.Models;
using GildedRose.Updaters;

namespace GildedRose.Factories;

public class ItemUpdaterFactory : IItemUpdaterFactory
{
    public IItemUpdater CreateUpdater(Item item)
    {
        return new DefaultItemUpdater();
    }
}
