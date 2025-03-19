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
        public async Task<IActionResult> GetEmpleado()
        {
            try
            {
                string query = @"
                    SELECT E.idEmpleado, E.nombre, E.edad, E.correoElectronico, E.idArea ,A.nombre AS area, E.Alta
	                FROM Empleado AS E 
	                LEFT JOIN Area AS A ON E.idArea = A.idArea";

                var listEmpleado = await context.Set<EmpleadoModelDtos>()
                        .FromSqlRaw(query)
                        .AsNoTracking()
                        .ToListAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Empleados obtenidos correctamente",
                    data = listEmpleado
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrio un error" + ex.Message,
                });
            }
        }


        [HttpPost("saveEmpleado")]
        public async Task<IActionResult> SaveEmpleado([FromBody] EmpleadoModel empleado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        status = 400,
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                string query = "INSERT INTO Empleado (nombre, edad, correoElectronico,idArea, alta ) VALUES ({0}, {1}, {2}, {3},{4})";
                await context.Database.ExecuteSqlRawAsync(query, empleado.Nombre, empleado.Edad, empleado.CorreoElectronico, empleado.IdArea, empleado.Alta);
                return Ok(new
                {
                    status = 200,
                    message = "El empleado se guardo de forma correcta"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
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

                return Ok(new
                {
                    status = 200,
                    message = "El empleado se elimino de forma correcta"
                });
            }catch(Exception ex)
            {
                return BadRequest( new
                {
                    status = 400,
                    message = "Ocurrio un error " + ex.Message
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
                return Ok(new
                {
                    status = 200,
                    message = "Se edito de forma correcta"
                });
            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Ocurrio un error " + ex.ToString()
                });
            }
        }



        [HttpGet("EmpleadosPage")]
        public async Task<IActionResult> GetEmpleados([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
              
                int offset = (pageNumber - 1) * pageSize;

                string query = @"
                    SELECT E.idEmpleado, E.nombre, E.edad, E.correoElectronico, E.idArea ,A.nombre AS area, E.alta
                    FROM Empleado AS E
                    LEFT JOIN Area AS A ON E.idArea = A.idArea
                    ORDER BY E.idEmpleado
                    OFFSET {0} ROWS
                    FETCH NEXT {1} ROWS ONLY;";

                var empleados = await context.Set<EmpleadoModelDtos>().FromSqlRaw(query, offset, pageSize).ToListAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Empleados obtenidos correctamente",
                    data = empleados
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
