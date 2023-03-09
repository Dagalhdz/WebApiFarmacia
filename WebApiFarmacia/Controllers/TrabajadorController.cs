using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia.Controllers
{
    [ApiController]
    [Route("trabajador")]
    public class TrabajadorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrabajadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Trabajador>> NewTrabajador(Trabajador trabajador)
        {
            _context.Add(trabajador);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Trabajador>>> GetAll()
        {
            return await _context.Trabajador.Include(x => x.Ventas).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Trabajador>> GetById(int id)
        {
            var exist = await _context.Trabajador.AnyAsync(x => x.TrabajadorId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            return await _context.Trabajador.Include(x => x.Ventas).FirstOrDefaultAsync(x => x.TrabajadorId == id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Trabajador>> Edit(Trabajador trabajador, int id)
        {
            var existeTrabajador = await _context.Trabajador.AnyAsync(x => x.TrabajadorId == trabajador.TrabajadorId);

            if (!existeTrabajador)
            {
                return NotFound("No existe el pasajero que quieres editar");
            }

            if (trabajador.TrabajadorId != id)
            {
                return NotFound("El id del Trabajador no coincide con el id de la url");
            }
            _context.Update(trabajador);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Trabajador>> Delete(int id)
        {
            var exist = await _context.Trabajador.AnyAsync(x => x.TrabajadorId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            _context.Remove(new Trabajador() { TrabajadorId = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
