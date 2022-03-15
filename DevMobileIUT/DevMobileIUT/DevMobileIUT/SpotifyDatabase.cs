using DevMobileIUT.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevMobileIUT
{
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }

    public class SpotifyDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<SpotifyDatabase> Instance = new AsyncLazy<SpotifyDatabase>(async () =>
        {
            var instance = new SpotifyDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Musique>();
            return instance;
        });

        public SpotifyDatabase()
        {
            Database = new SQLiteAsyncConnection(constants.DatabasePath, constants.Flags);
        }

        public Task<List<Musique>> GetMusiquesAsync()
        {
            return Database.Table<Musique>().ToListAsync();
        }

        public Task<Musique> GetMusiqueByIdAsync(int id)
        {
            return Database.Table<Musique>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> AddMusiqueAsync(Musique musique)
        {
            return Database.InsertAsync(musique);
        }

        public Task<int> DeleteMusiqueAsync(Musique musique)
        {
            return Database.DeleteAsync(musique);
        }

        public Task<bool> IsSpotifyDatabaseEmptyAsync()
        {
            return Task.Run(() => 0 == Database.Table<Musique>().ToListAsync().Result.Count);
        }
    }
}
