using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

//Registrar eventos en el loger
    private readonly ILogger<ProductosController> _logger;

    private readonly DataContext _context;

    public ProductosController(ILogger<ProductosController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetProductos")]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        //ToListAsyc no es lo mismo que liq es un metodo de entityFramework
        return await _context.Productos.ToListAsync();
    }

    //Traer un solo dato dependiendo de el id
    [HttpGet("{id}", Name = "GetProducto")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if(producto == null)
        {
            return NotFound();
        }
        //Retornamos el prodtco existente
        return producto;
    }

    //metodo post 
    [HttpPost]
    public async Task<ActionResult<Producto>> Post(Producto producto)
    {
        //agregar a la tabla
        _context.Productos.Add(producto);
         System.Console.Write("aca");
        //salvamos cambios
        await _context.SaveChangesAsync();

        //resultado de accion devuleve una respuesta Http con codigo 201 que es created
        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        //return new CreatedAtRouteResult("GetProducto", new {id = producto.Id} , producto);
    }

    //put (actualizar)

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id,Producto producto)
    {
        if(id != producto.Id)
        {
            //BadRequest Status 400
            return BadRequest();
        }

        //Agregar producto y decir que esta modificando
        _context.Entry(producto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        //Devolvemos estado 200 que es ok
        return Ok();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult<Producto>> Delete(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if(producto == null)
        {
            return NotFound();
        }

        //Eliminar el producto
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return producto;
    }

    }
}