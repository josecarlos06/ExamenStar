using ExamenWebStar.Data;
using ExamenWebStar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenWebStar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public EmpleadoController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("GetEmpleado")]
        public async Task<ActionResult> GetEmpleado()
        {
            try
            {
                string query = @"
                    SELECT E.idEmpleado, E.nombre, E.edad, E.correoElectronico, E.idArea ,A.nombre AS area
	                FROM Empleado AS E 
	                LEFT JOIN Area AS A ON E.idArea = A.idArea";

                var listEmpleado = await context.Set<EmpleadoModelDtos>()
                        .FromSqlRaw(query)
                        .AsNoTracking()
                        .ToListAsync();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se obtuvieron de forma correcta los datos",
                    data = listEmpleado
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


        [HttpPost("saveEmpleado")]
        public async Task<IActionResult> SaveEmpleado([FromBody] EmpleadoModel empleado)
        {
            try
            {
                string query = "INSERT INTO Empleado (nombre, edad, correoElectronico, idArea) VALUES ({0}, {1}, {2}, {3})";
                await context.Database.ExecuteSqlRawAsync(query, empleado.Nombre, empleado.Edad, empleado.CorreoElectronico, empleado.IdArea);
                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se guardaron datos correctamente"
                });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 500,
                    statusText = "Error",
                    message = "Ocurrió un error: " + ex.Message
                });
            }
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                string query = "DELETE FROM empleado WHERE idEmpleado = {0}";
                await context.Database.ExecuteSqlRawAsync(query, id);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se elemino de forma correcta"
                });
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    status = 404,
                    message = "Ocurrio un error " + ex.ToString()
                });
            }
        }

        [HttpPut("putEmpleado")]
        public async Task<IActionResult> PutEmpleado([FromBody] EmpleadoModel empleado)
        {

            try
            {
                string query = "update Empleado set nombre = {0} ,edad = {1} , correoElectronico = {2} , idArea = {3} where idEmpleado = {4}";
                await context.Database.ExecuteSqlRawAsync(query, empleado.Nombre, empleado.Edad, empleado.CorreoElectronico, empleado.IdArea, empleado.IdEmpleado);
                return StatusCode(StatusCodes.Status200OK, new
                {
                    status = 200,
                    message = "Se edito de forma correcta"
                });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    status = 404,
                    message = "Ocurrio un error " + ex.ToString()
                });
            }


        }
    }
}
