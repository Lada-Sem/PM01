using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

public class DatabaseHelper
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseHelper(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<TestResult>().Wait(); // Создание таблицы для результатов тестов
    }

    // Получение всех результатов тестов
    public Task<List<TestResult>> GetResultsAsync()
    {
        return _database.Table<TestResult>().ToListAsync();
    }

    // Сохранение результата теста
    public Task<int> SaveResultAsync(TestResult result)
    {
        return result.Id != 0 ? _database.UpdateAsync(result) : _database.InsertAsync(result);
    }

    // Удаление результата теста
    public Task<int> DeleteResultAsync(TestResult result)
    {
        return _database.DeleteAsync(result);
    }
}

public class TestResult
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int TestIndex { get; set; }
    public int CorrectAnswersCount { get; set; }
    public DateTime DateTaken { get; set; }
}
