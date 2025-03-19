using ExamenWebStar.Data;
using ExamenWebStar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenWebStar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AreaController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpPost("AddArea")]
        public async Task<ActionResult> AddArea([FromBody] AreaModel area)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                context.Add(area);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Area guardada correctamente",
                });

            }
            catch (Exception ex) {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrió un error: " + ex.Message
                });
            } 
        }

        [HttpGet("GetAreaSearch")]
        public async Task<IActionResult> GetAreaSearch()
        {
            try
            {
                var listArea = await context.Area
                                .Select(e => new { e.IdArea, e.Nombre })
                                .ToListAsync();
                return Ok(new
                {
                    status = 200,
                    message = "Se obtuvieron de forma correcta los datos",
                    data = listArea
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrió un error: " + ex.Message
                });
            }

        }


        [HttpGet("GetArea")]
        public async Task<ActionResult<List<AreaEmpleadoDto>>> GetArea()
        {
            try
            {
                string query = @"
                    SELECT
                        Area.idArea,
                        Area.nombre,
                        Area.descripcion,
                        Area.activo,
                        Area.Alta, 
                        COUNT(Empleado.idEmpleado) AS CantidadEmpleados
                    FROM Area
                    LEFT JOIN Empleado ON Empleado.idArea = Area.idArea
                    GROUP BY Area.idArea, Area.nombre, Area.activo, Area.descripcion, Area.Alta";

                List<AreaEmpleadoDto> listArea = await context.Set<AreaEmpleadoDto>()
                        .FromSqlRaw(query)
                        .AsNoTracking()
                        .ToListAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Se obtuvieron de forma correcta los datos",
                    data = listArea
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrió un error: " + ex.Message
                });
            }
        }


        [HttpDelete("DeleteArea/{id}")]
        public async Task<ActionResult> GetArea(int id)
        {
            try
            {
                var validaExistencias = await context.Empleado.Where(a => a.IdArea == id).ToListAsync();

                if(validaExistencias.Count >= 1) 
                {
                    return Ok(new
                    {
                        status = 404,
                        message = "El registro actual no se puedo eliminar, Existen empleados Relacionados",
                    });
                }

                await context.Area.Where( a => a.IdArea == id).ExecuteDeleteAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Se eliminino de forma correcta",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrió un error: " + ex.Message
                });
            }
        }

        [HttpPut("UpdateArea")]
        public async Task<IActionResult> UpdateArea([FromBody] AreaModel area)
        {
            try
            {
                if(area.IdArea == 0)
                {
                    return Ok(new
                    {
                        status = 200,
                        message = "Objeto no cuenta con Id"
                    });
                };

                context.Update(area);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Área modificada correctamente."
                });

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrió un error: " + ex.Message
                });
            }
        }
    }
}
