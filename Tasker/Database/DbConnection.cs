using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tasker.Models;

namespace Tasker.Database
{
    public class DbConnection
    {
        private SQLiteAsyncConnection _connection;
        private static DbConnection _dbConnection;
        private string _dbPath;
        private static readonly object lockObject = new object();
        private static bool _isInitialized = false;

        public Task<List<TaskHeaderModel>> TaskHeaders => _connection.Table<TaskHeaderModel>().ToListAsync();
        public Task<List<TaskLineModel>> TaskLines => _connection.Table<TaskLineModel>().ToListAsync();

        private DbConnection()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database");

            Directory.CreateDirectory(folderPath);

            _dbPath = Path.Combine(folderPath, "TaskerDB.db3");

            _connection = new SQLiteAsyncConnection(_dbPath);
        }

        private async Task Initialize()
        {
            await CreateTable();

            _isInitialized = true;
        }

        public static async Task<DbConnection> GetDbConnection()
        {
            lock (lockObject)
            {
                if (_dbConnection == null)
                {
                    _dbConnection = new DbConnection();
                }
            }

            if (!_isInitialized)
            {
                await _dbConnection.Initialize();
            }

            return _dbConnection;
        }

        private async Task CreateTable()
        {
            await _connection.CreateTableAsync<TaskHeaderModel>();
            await _connection.CreateTableAsync<TaskLineModel>();
        }

        public async Task InsertRecord<E>(List<E> items) where E : BaseModel
        {
            items.ForEach(x => x.CreatedDate = DateTime.Now);
            await _connection.InsertAllAsync(items);
        }

        public async Task InsertRecord<E>(E item) where E : BaseModel
        {
            item.CreatedDate = DateTime.Now;
            await _connection.InsertAsync(item);
        }

        public async Task UpdateRecord<E>(List<E> items) where E : BaseModel
        {
            items.ForEach(x => x.ModifiedDate = DateTime.Now);
            await _connection.UpdateAllAsync(items);
        }

        public async Task UpdateRecord<E>(E item) where E : BaseModel
        {
            item.ModifiedDate = DateTime.Now;
            await _connection.UpdateAsync(item);
        }

        public async Task DeleteRecord<E>(List<E> items) where E : BaseModel
        {
            foreach (E item in items)
            {
                await DeleteRecord<E>(item);
            }
        }

        public async Task DeleteRecord<E>(E item) where E : BaseModel
        {
            await _connection.DeleteAsync(item);
        }

        public string GetDBPath()
        {
            return _dbPath;
        }
    }
}