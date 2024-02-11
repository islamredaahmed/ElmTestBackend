using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Migrations;
using Migrations.Data;
using Migrations.Models;
using Newtonsoft.Json;
using System.Data;

namespace ElmTest.Controllers
{
    public static class SeedExtension
    {
        public static async Task Seed(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<string>>();
            var context = scope.ServiceProvider.GetRequiredService<ElmTestDbContext>();


            await BookDataCreator.CreateAsync(context);
        }
    }


    public class BookDataCreator
    {
        static Random _rand = new Random((int)DateTime.Now.Ticks);

        static string GenerateEnglishName(int minSize, int maxSize)
        {
            int size = GetRandomInt(minSize, maxSize) - 1;
            string result = ((char)GetRandomInt('A', 'Z')).ToString();
            for (int i = 0; i < size; i++)
            {
                result += ((char)GetRandomInt('a', 'z')).ToString();
            }
            return result;
        }

        static string GenerateEnglishFullName(int minWordCount, int maxWordCount, int nameMinSize, int nameMaxSize)
        {
            int size = GetRandomInt(minWordCount, maxWordCount);
            string result = "";
            for (int i = 0; i < size; i++)
            {
                result += GenerateEnglishName(nameMinSize, nameMaxSize) + " ";
            }
            return result.TrimEnd();
        }

        public static async Task CreateAsync(ElmTestDbContext context)
        {
            if (!context.Books.Any())
            {
                var data = GetData();
                await context.Books.AddRangeAsync(data);
                await context.SaveChangesAsync();
            }
        }

        static int GetRandomInt(int minInclusive, int maxInclusive)
        {
            return _rand.Next(minInclusive, maxInclusive + 1);
        }
        private static List<Book> GetData()
        {
            var someImages = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                someImages.Add(ImageGenerator.GenerateRandomBase64Image(80 + GetRandomInt(0, 30), 150 + GetRandomInt(0, 40)));
            }

            List<Book> books = new List<Book>();
            for (int i = 0; i < 100000; i++)
            {
                var json = JsonConvert.SerializeObject(new
                {
                    BookTitle = GenerateEnglishFullName(1, 4, 2, 12),
                    BookDescription = GenerateEnglishFullName(2, 20, 2, 12),
                    Author = GenerateEnglishFullName(2, 3, 3, 12),
                    PublishDate = DateTime.UtcNow.AddHours(GetRandomInt(-100000, -1)),
                    CoverBase64 = someImages[i % 10]
                });
                Book book = new Book();
                book.BookInfo = json;
                book.LastModified = DateTime.UtcNow.AddHours(GetRandomInt(-100000, -1));
                books.Add(book);
            }
            return books;
        }
    }
}
