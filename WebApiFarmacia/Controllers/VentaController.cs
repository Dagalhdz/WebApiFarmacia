using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia.Controllers
{
    [Route("venta")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> NewVenta(Venta venta)
        {
            var existeTrabajador = await _context.Trabajador.AnyAsync(x => x.TrabajadorId == venta.TrabajadorId);
            var existeMedicamento = await _context.Medicamento.AnyAsync(x => x.MedicamentoId == venta.MedicamentoId);

            if (!existeTrabajador)
            {
                return NotFound("No existe el trabajador");
            }

            if (!existeMedicamento)
            {
                return NotFound("No existe el medicamento");
            }

            _context.Add(venta);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Venta>>> GetAll()
        {
            return await _context.Venta.Include(x => x.Trabajador).Include(x => x.Medicamento).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Venta>> GetById(int id)
        {
            var exist = await _context.Venta.AnyAsync(x => x.VentaId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            return await _context.Venta.Include(x => x.Trabajador).Include(x => x.Medicamento).FirstOrDefaultAsync(x => x.VentaId == id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Venta>> Edit(Venta venta, int id)
        {
            var existeVenta = await _context.Venta.AnyAsync(x => x.VentaId == venta.VentaId);
            var existeTrabajador = await _context.Trabajador.AnyAsync(x => x.TrabajadorId == venta.TrabajadorId);
            var existeMedicamento = await _context.Medicamento.AnyAsync(x => x.MedicamentoId == venta.MedicamentoId);

            if (!existeVenta)
            {
                return NotFound("No existe la id que intentas editar de venta");
            }

            if (!existeTrabajador)
            {
                return NotFound("No existe el trabajador");
            }

            if (!existeMedicamento)
            {
                return NotFound("No existe el medicamento");
            }

            if (venta.VentaId != id)
            {
                return NotFound("No se encontro el resgistro en la base de datos");
            }

            _context.Update(venta);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Venta>> Delete(int id)
        {
            var exist = await _context.Venta.AnyAsync(x => x.VentaId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            _context.Remove(new Venta() { VentaId = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
