// Controllers/MunkalapokController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizvezetek.API.Converters;
using Vizvezetek.API.DTOs;
using Vizvezetek.API.Models;

namespace Vizvezetek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MunkalapokController : ControllerBase
    {
        private readonly vizvezetekContext _context;

        public MunkalapokController(vizvezetekContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Összes munkalap lekérdezése
        /// </summary>
        /// <returns>Munkalapok listája</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> GetMunkalapok()
        {
            try
            {
                var munkalapok = await _context.munkalap
                    .Include(m => m.hely)
                    .Include(m => m.szerelo)
                    .Select(m => new MunkalapDto
                    {
                        id = m.id,
                        beadas_datum = DateConverter.DateOnlyToDateTime(m.beadas_datum),
                        javitas_datum = DateConverter.DateOnlyToDateTime(m.javitas_datum),
                        helyszin = $"{m.hely.telepules}, {m.hely.utca}",
                        szerelo = m.szerelo.nev,
                        munkaora = m.munkaora,
                        anyagar = m.anyagar
                    })
                    .ToListAsync();

                return munkalapok;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a munkalapok lekérdezése során: {ex.Message}");
            }
        }

        /// <summary>
        /// Munkalap lekérdezése azonosító alapján
        /// </summary>
        /// <param name="id">Munkalap azonosító</param>
        /// <returns>Munkalap részletei</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MunkalapDto>> GetMunkalap(int id)
        {
            try
            {
                var munkalap = await _context.munkalap
                    .Include(m => m.hely)
                    .Include(m => m.szerelo)
                    .Where(m => m.id == id)
                    .Select(m => new MunkalapDto
                    {
                        id = m.id,
                        beadas_datum = DateConverter.DateOnlyToDateTime(m.beadas_datum),
                        javitas_datum = DateConverter.DateOnlyToDateTime(m.javitas_datum),
                        helyszin = $"{m.hely.telepules}, {m.hely.utca}",
                        szerelo = m.szerelo.nev,
                        munkaora = m.munkaora,
                        anyagar = m.anyagar
                    })
                    .FirstOrDefaultAsync();

                if (munkalap == null)
                {
                    return NotFound($"A(z) {id} azonosítójú munkalap nem található.");
                }

                return munkalap;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a munkalap lekérdezése során: {ex.Message}");
            }
        }

        /// <summary>
        /// Munkalapok keresése helyszín és szerelő azonosító alapján
        /// </summary>
        /// <param name="kereses">Keresési feltételek</param>
        /// <returns>A feltételeknek megfelelő munkalapok listája</returns>
        [HttpPost("Kereses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> KeresMunkalapok([FromBody] MunkalapKeresesDto kereses)
        {
            try
            {
                if (kereses == null)
                {
                    return BadRequest("A keresési feltételek hiányoznak.");
                }

                var munkalapok = await _context.munkalap
                    .Include(m => m.hely)
                    .Include(m => m.szerelo)
                    .Where(m => m.hely_id == kereses.helyszin_id && m.szerelo_id == kereses.szerelo_id)
                    .Select(m => new MunkalapDto
                    {
                        id = m.id,
                        beadas_datum = DateConverter.DateOnlyToDateTime(m.beadas_datum),
                        javitas_datum = DateConverter.DateOnlyToDateTime(m.javitas_datum),
                        helyszin = $"{m.hely.telepules}, {m.hely.utca}",
                        szerelo = m.szerelo.nev,
                        munkaora = m.munkaora,
                        anyagar = m.anyagar
                    })
                    .ToListAsync();

                return munkalapok;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a munkalapok keresése során: {ex.Message}");
            }
        }

        /// <summary>
        /// Munkalapok lekérdezése év alapján
        /// </summary>
        /// <param name="ev">Év</param>
        /// <returns>Az adott évben lezárt munkalapok listája</returns>
        [HttpGet("ev/{ev}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MunkalapDto>>> GetMunkalapokByEv(int ev)
        {
            try
            {
                var munkalapok = await _context.munkalap
                    .Include(m => m.hely)
                    .Include(m => m.szerelo)
                    .Where(m => m.javitas_datum.Year == ev)
                    .Select(m => new MunkalapDto
                    {
                        id = m.id,
                        beadas_datum = DateConverter.DateOnlyToDateTime(m.beadas_datum),
                        javitas_datum = DateConverter.DateOnlyToDateTime(m.javitas_datum),
                        helyszin = $"{m.hely.telepules}, {m.hely.utca}",
                        szerelo = m.szerelo.nev,
                        munkaora = m.munkaora,
                        anyagar = m.anyagar
                    })
                    .ToListAsync();

                return munkalapok;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a munkalapok lekérdezése során: {ex.Message}");
            }
        }
    }
}