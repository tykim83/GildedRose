using GildedRose.Models;
using GildedRose.Updaters;

namespace GildedRose.Factories;

public interface IItemUpdaterFactory
{
    IItemUpdater CreateUpdater(Item item);
}
