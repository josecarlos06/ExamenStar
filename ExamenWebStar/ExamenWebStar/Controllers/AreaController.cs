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
                context.Add(area);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Area guardada correctamente",
                });

            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    status = 404,
                    message = "Ocurrio un error en el proceso",
                });
            } 
        }

        [HttpGet("GetAreaSearch")]
        public async Task<IActionResult> GetAreaSearch()
        {
            var listArea = await context.Area
                .Select(e => new { e.IdArea, e.Nombre })
                .ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new
            {
                status = 200,
                message = "Se obtuvieron de forma correcta los datos",
                data = listArea
            });
        }


        [HttpGet("GetArea")]
        public async Task<ActionResult> GetArea()
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

                var listArea = await context.Set<AreaEmpleadoDto>()
                        .FromSqlRaw(query)
                        .AsNoTracking()
                        .ToListAsync();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se obtuvieron de forma correcta los datos",
                    data = listArea
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    status = 404,
                    message = "Ocurrio un error en el proceso",
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
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        status = 404,
                        message = "La area actual no se puedo eliminar, Existen empleados Relacionados",
                    });
                }

                await context.Area.Where( a => a.IdArea == id).ExecuteDeleteAsync();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se elemino de forma exitosa",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    status = 404,
                    message = "Ocurrio un error en el proceso",
                });
            }
        }

        [HttpPut("UpdateArea")]
        public async Task<ActionResult> UpdateArea([FromBody] AreaModel area)
        {
            try
            {
                if(area.IdArea == 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        status = 200,
                        message = "Objeto no contiene id",
                    });
                };

                context.Update(area);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Area modificada correctamente",
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    status = 404,
                    message = "Ocurrio un error en el proceso",
                });
            }
        }
    }
}
