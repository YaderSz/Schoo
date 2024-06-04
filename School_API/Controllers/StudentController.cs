using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_API.Data;
using School_API.Dto;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        public StudentController(SchoolContext context, ILogger<StudentController> logger, IMapper mapper) 
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents() 
        {
            try
            {
                _logger.LogInformation("Obteniendo los estudiantes");
                var students = await _context.Students.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al obtener a los estudiantes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor al obtener los estudiantes");
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetStudent(int id) 
        {
        if(id<=0)
            { _logger.LogError($"Id del estudiante no valido: {id}");
                return BadRequest("ID del estudiante no valido");
            }
            try
            {
                _logger.LogInformation($"Obteniendo estudiante con ID : {id}");
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    _logger.LogWarning($"No se encontro ningun estudiante con ID :{id}");
                    return NotFound("Estudiante no Encontrado");
                }
                return Ok(_mapper.Map<StudentDto>(student));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener al estudiante con ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor al obtener el estudiante");

            }
        }


    }
}
