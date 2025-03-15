using ExamenWebStar.Data;
using ExamenWebStar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet("GetArea")]
        public async Task<ActionResult> GetArea()
        {
            try
            {
                List<AreaModel> listArea = await context.Area.ToListAsync();
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
                await context.Area.Where(e => e.IdArea == id).ExecuteDeleteAsync();
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
    }
}
