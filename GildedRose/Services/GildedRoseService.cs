using GildedRose.Models;
using System.Collections.Generic;

namespace GildedRose.Services;

public class GildedRoseService : IGildedRoseService
{
    public ICollection<Item> UpdateQuality(ICollection<Item> items)
    {
        var updatedItems = new List<Item>();

        foreach (var item in items)
        {
            var updatedItem = item.DecreaseSellIn().UpdateQuality();
            updatedItems.Add(updatedItem);
        }

        return updatedItems;
    }
}
