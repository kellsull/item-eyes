using ItemEyes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemEyes.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ItemContext context)
        {
            context.Database.EnsureCreated();

            if (context.Items.Any())
            {
                return;
            }

            var locations = new Location[]
            {
                new Location { Name="Fictitious Warehouse", Address="555 Fictitious Rd., Random, TX", Contact="John Bard (555)-555-4221" },
                new Location { Name="Shop", Address="525 Imagination Rd., Random, TX", Contact="Main Office (555)-555-1243" }
            };
            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();

            var items = new Item[]
            {
                new Item { ProductId=1, Name="#12 Brown Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E1", LocationId=1 },
                new Item { ProductId=2, Name="#12 Purple Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E2", LocationId=1 },
                new Item { ProductId=3, Name="#12 Yellow Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E3", LocationId=1 },
                new Item { ProductId=4, Name="#12 Grey Solid CU THHN - 500 ft.", OrderCount=200, Quantity=200, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E4", LocationId=1 },
                new Item { ProductId=5, Name="#12 Red Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E5", LocationId=1 },
                new Item { ProductId=6, Name="#12 Green Solid CU THHN - 500 ft.", OrderCount=400, Quantity=400, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E6", LocationId=1 },
                new Item { ProductId=7, Name="#12 Black Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50,  ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E7", LocationId=1 },
                new Item { ProductId=8, Name="#12 Blue Solid CU THHN - 500 ft.", OrderCount=50, Quantity=50,  ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E8", LocationId=1 },
                new Item { ProductId=9, Name="#12 White Solid CU THHN - 500 ft.", OrderCount=200, Quantity=200, ReceivedOn=DateTime.Parse("03-15-2021"), StorageZone="E9", LocationId=1 },
                new Item { ProductId=10, Name="1/4 in.-32 x 1 inch Machine Screws", OrderCount=1000, Quantity=1000, ReceivedOn=DateTime.Parse("02-26-2021"), StorageZone="A25", LocationId=1 },
                new Item { ProductId=11, Name="4 in. Square Electrical Boxes ", OrderCount=200, Quantity=150, ReceivedOn=DateTime.Parse("02-26-2021"), StorageZone="A14", LocationId=1 },
                new Item { ProductId=12, Name="3/4 in. EMT Set-Screw Couplings", OrderCount=1000, Quantity=700, ReceivedOn=DateTime.Parse("08-25-2020"), StorageZone="B15", LocationId=1 },
                new Item { ProductId=13, Name="1 in. EMT Set-Screw Couplings", OrderCount=1000, Quantity=850, ReceivedOn=DateTime.Parse("08-25-2020"), StorageZone="B16", LocationId=1 },
                new Item { ProductId=14, Name="1 in. x 10 ft. EMT", OrderCount=2500, Quantity=2000, ReceivedOn=DateTime.Parse("08-25-2020"), StorageZone="C-Rack", LocationId=1 },
                new Item { ProductId=15, Name="3/4 in. x 10 ft. EMT", OrderCount=2500, Quantity=1000, ReceivedOn=DateTime.Parse("08-25-2020"), StorageZone="C-Rack", LocationId=1 },
                new Item { ProductId=1, Name="#12 Brown Solid CU THHN - 500 ft.", OrderCount=50, Quantity=20,  ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E1", LocationId=1 },
                new Item { ProductId=2, Name="#12 Purple Solid CU THHN - 500 ft.", OrderCount=50, Quantity=20,  ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E2", LocationId=1 },
                new Item { ProductId=3, Name="#12 Yellow Solid CU THHN - 500 ft.", OrderCount=50, Quantity=20, ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E3", LocationId=1 },
                new Item { ProductId=4, Name="#12 Grey Solid CU THHN - 500 ft.", OrderCount=200, Quantity=100, ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E4", LocationId=1 },
                new Item { ProductId=5, Name="#12 Red Solid CU THHN - 500 ft.", OrderCount=50, Quantity=10, ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E5", LocationId=1 },
                new Item { ProductId=6, Name="#12 Green Solid CU THHN - 500 ft.", OrderCount=400, Quantity=150, ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E6", LocationId=1 },
                new Item { ProductId=7, Name="#12 Black Solid CU THHN - 500 ft.", OrderCount=50, Quantity=5,  ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E7", LocationId=1 },
                new Item { ProductId=8, Name="#12 Blue Solid CU THHN - 500 ft.", OrderCount=50, Quantity=10,  ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E8", LocationId=1 },
                new Item { ProductId=9, Name="#12 White Solid CU THHN - 500 ft.", OrderCount=200, Quantity=75, ReceivedOn=DateTime.Parse("06-30-2020"), StorageZone="E9", LocationId=1 },
                new Item { ProductId=16, Name="8 ft. Fiberglass Ladder - 300lbs LC", OrderCount=25, Quantity=10, ReceivedOn=DateTime.Parse("05-30-2020"), StorageZone="", LocationId=2 },
                new Item { ProductId=17, Name="Milwaukee M18 Hammer Drill/Drill", OrderCount=25, Quantity=5, ReceivedOn=DateTime.Parse("05-30-2020"), StorageZone="", LocationId=2 },
                new Item { ProductId=18, Name="Milwaukee 1000V Non-Contact Voltage Detector", OrderCount=25, Quantity=10, ReceivedOn=DateTime.Parse("05-30-2020"), StorageZone="", LocationId=2 },

            };
            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();
        }
    }
}
