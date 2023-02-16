using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using myshelter_api.Data;
using myshelter_api.Models;
using myshelter_api.Models.dto;
using System.Reflection.Metadata.Ecma335;

namespace myshelter_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class enterpriseController : ControllerBase
    {
        private readonly ILogger<enterpriseController> _logger;

        public enterpriseController(ILogger<enterpriseController> logger)
        {
            _logger = logger;   
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<enterprisedto>> GetEnterprises()
        {
            _logger.LogInformation("Obtener los enterprises");
            return Ok(enterprisestore.enterpriseList);
        }

        [HttpGet("id:int", Name = "getenterprise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<enterprisedto> GetEnterpriseId(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al intentar traer enterprise id" + id);
                return BadRequest();
            }
            var enterprise = enterprisestore.enterpriseList.FirstOrDefault(v => v.Id == id);
            if (enterprise == null)
            {
                return NotFound();
            }
            return Ok(enterprise);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<enterprisedto> AddEnterprise([FromBody] enterprisedto EnterpriseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(EnterpriseDTO);
            }
            if (enterprisestore.enterpriseList.FirstOrDefault(v => v.Name.ToLower() == EnterpriseDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La empresa con ese nombre ya existe!");
                return BadRequest(ModelState);

            }
            if (EnterpriseDTO == null)
            {
                return BadRequest(EnterpriseDTO);
            }
            if (EnterpriseDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            EnterpriseDTO.Id = enterprisestore.enterpriseList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            enterprisestore.enterpriseList.Add(EnterpriseDTO);

            //return Ok(EnterpriseDTO);
            return CreatedAtRoute("getenterprise", new { id = EnterpriseDTO.Id }, EnterpriseDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelEnterprise(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var enterprise = enterprisestore.enterpriseList.FirstOrDefault(v => v.Id == id);
            if (enterprise == null)
            {
                return NotFound();
            }
            enterprisestore.enterpriseList.Remove(enterprise);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UptEnterprise(int id, [FromBody] enterprisedto enterpriseDTO)
        {
            if (enterpriseDTO==null || id != enterpriseDTO.Id)
            {
                return BadRequest();
            }
            var enterprise = enterprisestore.enterpriseList.FirstOrDefault(v => v.Id == id);
            if (enterprise == null)
            {
                return NotFound();
            }
            enterprise.Name = enterpriseDTO.Name;
            enterprise.ZipCode = enterpriseDTO.ZipCode;
            enterprise.Phone = enterpriseDTO.Phone;

            return NoContent();
        }

        /* update partial */
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UptPartialEnterprise(int id, JsonPatchDocument<enterprisedto> patchDTO)
        {
            if (patchDTO == null || id != patchDTO.Id)
            {
                return BadRequest();
            }
            var enterprise = enterprisestore.enterpriseList.FirstOrDefault(v => v.Id == id);
            if (enterprise == null)
            {
                return NotFound();
            }

            patchDTO.ApplyTo(enterprise, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
