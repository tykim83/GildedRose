using GildedRose.Factories;
using GildedRose.Models;
using GildedRose.Services;
using GildedRose.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class Program
{
    public static void Main(string[] args)
    {
        // Set up dependency injection
        var serviceProvider = new ServiceCollection()
            .AddTransient<IGildedRoseService, GildedRoseService>()
            .AddTransient<IItemUpdaterFactory, ItemUpdaterFactory>()
            .BuildServiceProvider();

        // Get days from arguments or default to 30
        int days = args.Length > 0 && int.TryParse(args[0], out var result) ? result : Constants.DefaultDays;

        // Get GildedRoseService instance
        var gildedRoseService = serviceProvider.GetService<IGildedRoseService>();
        ICollection<Item> items = InitializeItems();

        // Run the day simulation
        SimulateDays(gildedRoseService, items, days);
    }

    private static ICollection<Item> InitializeItems()
    {
        return new List<Item>
    {
        new Item(Constants.DexterityVest, 10, 20),
        new Item(Constants.AgedBrie, 2, 0),
        new Item(Constants.ElixirOfTheMongoose, 5, 7),
        new Item(Constants.Sulfuras, 0, 80),
        new Item(Constants.Sulfuras, -1, 80),
        new Item(Constants.BackstagePass, 15, 20),
        new Item(Constants.BackstagePass, 10, 49),
        new Item(Constants.BackstagePass, 5, 49),
        new Item(Constants.ConjuredManaCake, 3, 6)
    };
    }

    private static void SimulateDays(IGildedRoseService gildedRoseService, ICollection<Item> items, int days)
    {
        Console.WriteLine("OMGHAI!");

        for (var i = 0; i <= days; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");

            foreach (var item in items)
                Console.WriteLine(item.Display());

            Console.WriteLine("");

            items = gildedRoseService.UpdateQuality(items);
        }
    }
}

