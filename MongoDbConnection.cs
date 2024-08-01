using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public class MongoDBConnection
{
    private readonly IMongoDatabase _database;

    public MongoDBConnection(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public async Task InsertRecordAsync<T>(string collectionName, T record)
    {
        var collection = _database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(record);
    }

    public async Task<List<T>> LoadRecordsAsync<T>(string collectionName)
    {
        var collection = _database.GetCollection<T>(collectionName);
        return await collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> LoadRecordByIdAsync<T>(string collectionName, Guid id)
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpsertRecordAsync<T>(string collectionName, Guid id, T record)
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        var options = new ReplaceOptions { IsUpsert = true };
        await collection.ReplaceOneAsync(filter, record, options);
    }

    public async Task DeleteRecordAsync<T>(string collectionName, Guid id)
    {
        var collection = _database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        await collection.DeleteOneAsync(filter);
    }
}