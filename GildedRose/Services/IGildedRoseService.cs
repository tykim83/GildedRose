using GildedRose.Models;
using System.Collections.Generic;

namespace GildedRose.Services;

public interface IGildedRoseService
{
    ICollection<Item> UpdateQuality(ICollection<Item> items);
}