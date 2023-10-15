﻿using System.Linq.Expressions;
using SQLite;
using Tasker.Models;
using Tasker.Utils;

namespace Tasker.Database
{
	public class DbConnection
	{
		public async Task Create<T>(T model)
		{
			await Init();
			await connection.InsertAsync(model);
		}

		public async Task<T> Get<T>(int id) where T : class, new()
		{
			await Init();
			return await connection.GetAsync<T>(id); ;
		}

		public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> func) where T : class, new()
		{
			await Init();
			return await connection.Table<T>().Where(func).ToListAsync();
		}

		public async Task Update<T>(T model)
		{
			await Init();
			await connection.UpdateAsync(model);
		}

		public async Task Delete<T>(int id)
		{
			await Init();
			await connection.DeleteAsync<T>(id);
		}


		async Task Init()
		{
			if (connection is not null)
			{
				return;
			}

			connection = new SQLiteAsyncConnection(DbFullPath(), DbFlags);
			await connection.CreateTablesAsync(CreateFlags.None, typeof(TaskHeaderModel));
		}

		string DbFullPath()
		{
			var dbFolderPath = Path.Combine(GlobalConstants.RootFolder, DbSubFolder);
			if (!Directory.Exists(dbFolderPath))
			{
				Directory.CreateDirectory(dbFolderPath);
			}

			return Path.Combine(dbFolderPath, DbFileName);
		}

		SQLiteAsyncConnection connection;
		const string DbFileName = "TaskerSqLite.db3";
		const string DbSubFolder = "Database";
		const SQLiteOpenFlags DbFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;
	}
}