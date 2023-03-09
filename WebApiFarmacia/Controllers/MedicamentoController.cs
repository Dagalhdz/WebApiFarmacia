using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia.Controllers
{
    [ApiController]
    [Route("medicamento")]
    public class MedicamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Medicamento>> NewMedicamento(Medicamento medicamento)
        {
            var existeLaboratorio = await _context.Laboratorio.AnyAsync(x => x.LaboratorioId == medicamento.LaboratorioId);
            var existeModoAdministracion = await _context.ModoAdministracion.AnyAsync(x => x.ModoAdministracionId == medicamento.ModoAdministracionId);

            if (!existeLaboratorio)
            {
                return NotFound("No existe el id del laboratorio");
            }

            if (!existeModoAdministracion)
            {
                return NotFound("No existe el id del modo de admisitracion");
            }

            _context.Add(medicamento);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Medicamento>>> GetAll()
        {
            return await _context.Medicamento.Include(x => x.Ventas).Include(x => x.Laboratorio).Include(x => x.ModoAdministracion).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Medicamento>> GetById(int id)
        {
            var exist = await _context.Medicamento.AnyAsync(x => x.MedicamentoId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            return await _context.Medicamento.Include(x => x.Ventas).Include(x => x.Laboratorio).Include(x => x.ModoAdministracion).FirstOrDefaultAsync(x => x.MedicamentoId == id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Medicamento>> Edit(Medicamento medicamento, int id)
        {
            var existeMedicamento = await _context.Medicamento.AnyAsync(x => x.MedicamentoId == medicamento.MedicamentoId);
            var existeLaboratorio = await _context.Laboratorio.AnyAsync(x => x.LaboratorioId == medicamento.LaboratorioId);
            var existeModoAdministracion = await _context.ModoAdministracion.AnyAsync(x => x.ModoAdministracionId == medicamento.ModoAdministracionId);

            if (!existeMedicamento)
            {
                return NotFound("No existe el medicamento");
            }

            if (!existeLaboratorio)
            {
                return NotFound("No existe el id del laboratorio");
            }

            if (!existeModoAdministracion)
            {
                return NotFound("No existe el id del modo de admisitracion");
            }

            if (medicamento.MedicamentoId != id)
            {
                return NotFound("La id del medicamento no coincide con la id del url");
            }
            _context.Update(medicamento);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Medicamento>> Delete(int id)
        {
            var exist = await _context.Medicamento.AnyAsync(x => x.MedicamentoId == id);

            if (!exist)
            {
                return NotFound("No se encontro el registro en la base de datos");
            }

            _context.Remove(new Medicamento() { MedicamentoId = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
