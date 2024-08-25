using GildedRose.Models;
using GildedRose.Shared;
using System;

namespace GildedRose.Extensions;

public static class ItemExtensions
{
    public static void ReduceSellIn(this Item item) => item.SellIn--;

    public static void AdjustQuality(this Item item, int amount) => item.Quality = Math.Clamp(item.Quality + amount, Constants.MinQuality, Constants.MaxQuality);

    public static bool IsExpired(this Item item) => item.SellIn < 0;
}
