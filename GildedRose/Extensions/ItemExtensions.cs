using GildedRose.Models;
using System;

namespace GildedRose.Extensions;

public static class ItemExtensions
{
    public static void ReduceSellIn(this Item item) => item.SellIn--;

    public static void AdjustQuality(this Item item, int amount) => item.Quality = Math.Clamp(item.Quality + amount, 0, 50);

    public static bool IsExpired(this Item item) => item.SellIn < 0;
}
