using GildedRose.Models;

namespace GildedRose.Updaters;

public interface IItemUpdater
{
    Item UpdateItem(Item item);
}
