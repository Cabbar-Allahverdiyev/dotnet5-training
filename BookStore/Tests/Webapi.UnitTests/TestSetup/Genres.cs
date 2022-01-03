using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(new Genre[]{
                new Genre(){Id=1,Name = "Personal Growth"},
                new Genre(){Id=2,Name = "Science Fiction"},
                new Genre(){Id=3,Name = "Romance"}
            });
        }
    }
}