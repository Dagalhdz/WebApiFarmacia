using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia.Controllers
{
    [Route("modoAdministracion")]
    [ApiController]
    public class ModoAdmistracionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModoAdmistracionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ModoAdministracion>> NewModoAdministracion(ModoAdministracion modoAdministracion)
        {
            _context.Add(modoAdministracion);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ModoAdministracion>>> GetAll()
        {
            return await _context.ModoAdministracion.Include(x => x.Medicamentos).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModoAdministracion>> GetById(int id)
        {
            var exist = await _context.ModoAdministracion.AnyAsync(x => x.ModoAdministracionId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            return await _context.ModoAdministracion.Include(x => x.Medicamentos).FirstOrDefaultAsync(x => x.ModoAdministracionId == id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ModoAdministracion>> Edit(ModoAdministracion modoAdministracion, int id)
        {
            if (modoAdministracion.ModoAdministracionId != id)
            {
                return NotFound("El id de modo de administracion no coicide con el establecido en la url");
            }
            _context.Update(modoAdministracion);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ModoAdministracion>> Delete(int id)
        {
            var exist = await _context.ModoAdministracion.AnyAsync(x => x.ModoAdministracionId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            _context.Remove(new ModoAdministracion() { ModoAdministracionId = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
