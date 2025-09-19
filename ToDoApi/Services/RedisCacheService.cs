using System.Text.Json;
using StackExchange.Redis;

namespace ToDoApi.Services;

public class RedisCacheService
{
    private readonly IDatabase _db;
    public RedisCacheService(IConnectionMultiplexer redis) => _db = redis.GetDatabase();
 
    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _db.StringGetAsync(key);
        return data.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(data!);
    }
 
    public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, ttl);
    }
 
    public async Task RemoveAsync(string key) => await _db.KeyDeleteAsync(key);
}