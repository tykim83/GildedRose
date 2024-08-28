using GildedRose.Factories;
using GildedRose.Models;
using System;
using System.Collections.Generic;

namespace GildedRose.Services;

public class GildedRoseService : IGildedRoseService
{
    private readonly IItemUpdaterFactory _itemUpdaterFactory;

    public GildedRoseService(IItemUpdaterFactory itemUpdaterFactory)
    {
        ArgumentNullException.ThrowIfNull(itemUpdaterFactory, nameof(itemUpdaterFactory));

        _itemUpdaterFactory = itemUpdaterFactory;
    }

    public ICollection<Item> UpdateQuality(ICollection<Item> items)
    {
        var updatedItems = new List<Item>();

        foreach (var item in items)
        {
            // Get Updater
            var updater = _itemUpdaterFactory.CreateUpdater(item);

            // Update Item
            var updatedItem = updater.UpdateItem(item);
            updatedItems.Add(updatedItem);
        }

        return updatedItems;
    }
}
