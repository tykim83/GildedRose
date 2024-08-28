using System;

namespace GildedRose.Models;

public record Item(string Name, int SellIn, int Quality)
{
    public Item DecreaseSellIn() => this with { SellIn = SellIn - 1 };

    public Item AdjustQuality(int qualityChange) =>
        this with { Quality = Math.Clamp(Quality + qualityChange, 0, 50) };

    public bool IsExpired() => SellIn < 0;

    public string Display() => $"{Name}, {SellIn}, {Quality}";
}
