using GildedRose.Updaters;

namespace GildedRose.Models;

public record Item(string Name, int SellIn, int Quality)
{
    public Item UpdateQuality()
    {
        var updater = Name switch
        {
            "Aged Brie" => ItemUpdaters.AgedBrieUpdater,
            "Backstage passes to a TAFKAL80ETC concert" => ItemUpdaters.BackstagePassUpdater,
            "Conjured Mana Cake" => ItemUpdaters.ConjuredItemUpdater,
            "Sulfuras, Hand of Ragnaros" => ItemUpdaters.SulfurasUpdater,
            _ => ItemUpdaters.DefaultItemUpdater
        };

        return updater(this);
    }

    public Item DecreaseSellIn() =>
        Name == "Sulfuras, Hand of Ragnaros"
            ? this
            : this with { SellIn = SellIn - 1 };

    public bool IsExpired() => SellIn < 0;

    public string Display() => $"{Name}, {SellIn}, {Quality}";
}
