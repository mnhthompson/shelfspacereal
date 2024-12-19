using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfSpace.Data;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ShelfSpaceContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ShelfSpaceContext>>()))
        {
            // Check if database is already seeded
            if (context.User.Any() || context.Media.Any())
            {
                return; // DB already seeded
            }

            // Create MediaItems
            var gatsby = new MediaItem { SpecialId = "1", Title = "The Great Gatsby", Genre = "Classic", Year = 1925, Type = MediaType.Book };
            var inception = new MediaItem { SpecialId = "2", Title = "Inception", Genre = "Science Fiction", Year = 2010, Type = MediaType.Movie };
            var darkSide = new MediaItem { SpecialId = "3", Title = "Dark Side of the Moon", Genre = "Rock", Year = 1973, Type = MediaType.CD };
            var breakingBad = new MediaItem { SpecialId = "4", Title = "Breaking Bad", Genre = "Drama", Year = 2008, Type = MediaType.TVShow };

            context.Media.AddRange(gatsby, inception, darkSide, breakingBad);

            // Create Users and assign MediaShelves
            var alice = new User
            {
                Id = "1",
                Name = "Alice Johnson",
                Email = "alice@example.com",
                Phone = "123-456-7890",
                MediaShelf = new List<MediaItem> { gatsby, breakingBad } // Assign to Alice's shelf
            };

            var bob = new User
            {
                Id = "2",
                Name = "Bob Smith",
                Email = "bob@example.com",
                Phone = "987-654-3210",
                MediaShelf = new List<MediaItem> { inception, darkSide } // Assign to Bob's shelf
            };

            context.User.AddRange(alice, bob);

            // Save Changes
            context.SaveChanges();
        }
    }
}