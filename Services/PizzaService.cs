using Pizzaria.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Pizzaria.Service;

public class PizzaService
{
    private readonly IDistributedCache _cache;
    private static List<Pizza> Pizzas { get; }
    private static int nextId = 3;
    private const string CacheKey = "Pizzas";

    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza {Id = 1, Name = "Italiana", ConGluten = false},
            new Pizza {Id = 2, Name = "Calabresa", ConGluten = true}
        };
    }

    public PizzaService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<List<Pizza>> GetAllAsync()
    {
        var cachedPizzas = await _cache.GetStringAsync(CacheKey);
        if (cachedPizzas != null)
        {
            return JsonSerializer.Deserialize<List<Pizza>>(cachedPizzas);
        }

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(Pizzas), options);
        return Pizzas;
    }

    public async Task<Pizza?> GetAsync(int id)
    {
        var pizzas = await GetAllAsync();
        return pizzas.FirstOrDefault(p => p.Id == id);
    }

    public async Task AddAsync(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
        await RefreshCache();
    }

    public async Task DeleteAsync(int id)
    {
        var pizza = Pizzas.FirstOrDefault(p => p.Id == id);
        if (pizza != null)
        {
            Pizzas.Remove(pizza);
            await RefreshCache();
        }
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index != -1)
        {
            Pizzas[index] = pizza;
            await RefreshCache();
        }
    }

    private async Task RefreshCache()
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(Pizzas), options);
    }
}