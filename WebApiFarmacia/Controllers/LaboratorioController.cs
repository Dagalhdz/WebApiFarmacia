using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia.Controllers
{
    [Route("laboratorio")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LaboratorioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Laboratorio>> NewLaboratorio(Laboratorio laboratorio)
        {
            _context.Add(laboratorio);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Laboratorio>>> GetAll()
        {
            return await _context.Laboratorio.Include(x => x.Medicamentos).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Laboratorio>> GetById(int id)
        {
            var exist = await _context.Laboratorio.AnyAsync(x => x.LaboratorioId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            return await _context.Laboratorio.Include(x => x.Medicamentos).FirstOrDefaultAsync(x => x.LaboratorioId == id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Laboratorio>> Edit(Laboratorio laboratorio, int id)
        {
            var existeLaboratorio = await _context.Laboratorio.AnyAsync(x => x.LaboratorioId == laboratorio.LaboratorioId);

            if (!existeLaboratorio)
            {
                return NotFound("No existe el pasajero que quieres editar");
            }

            if (laboratorio.LaboratorioId != id)
            {
                return NotFound("El id del Laboratorio no coincide con el id de la url");
            } 
            _context.Update(laboratorio);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Laboratorio>> Delete(int id)
        {
            var exist = await _context.Laboratorio.AnyAsync(x => x.LaboratorioId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            _context.Remove(new Laboratorio() { LaboratorioId = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
