using Pizzaria.Models;
using Pizzaria.Service;
using Microsoft.AspNetCore.Mvc;

namespace Pizzaria.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll() =>
        await _pizzaService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.GetAsync(id);
        if (pizza == null)
            return NotFound();

        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        await _pizzaService.AddAsync(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = await _pizzaService.GetAsync(id);
        if (existingPizza == null)
            return NotFound();

        await _pizzaService.UpdateAsync(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pizza = await _pizzaService.GetAsync(id);
        if (pizza == null)
            return NotFound();

        await _pizzaService.DeleteAsync(id);
        return NoContent();
    }
}