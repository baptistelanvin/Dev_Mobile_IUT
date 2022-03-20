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
            CreateTableResult result = await Database.CreateTableAsync<Musique>(); //création de la table pour enregistrer les musiques
            return instance;
        });

        public SpotifyDatabase() //Initialisation de la BD
        {
            Database = new SQLiteAsyncConnection(constants.DatabasePath, constants.Flags);
        }

        public Task<List<Musique>> GetMusiquesAsync() //Méthode qui permet de récupérer les données dans la base de données
        {
            return Database.Table<Musique>().ToListAsync();
        }

        public Task<int> AddMusiqueAsync(Musique musique) //Méthode qui permet d'ajouter un élément dans la base de données
        {
            return Database.InsertAsync(musique);
        }

        public Task<bool> IsSpotifyDatabaseEmptyAsync() //Méthode qui permet de supprimer un élément dans la base de données
        {
            return Task.Run(() => 0 == Database.Table<Musique>().ToListAsync().Result.Count);
        }
    }
}
