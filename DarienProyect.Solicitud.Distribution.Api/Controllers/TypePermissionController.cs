using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acv2.SharedKernel.Crosscutting.Exceptions;
using DarienProyect.Solicitud.Aplication.TypePermissions.Commands;
using DarienProyect.Solicitud.Aplication.TypePermissions.Querys;
using DarienProyect.Solicitud.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DarienProyect.Solicitud.Distribution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePermissionController : ControllerBase
    {
        private readonly Mediator _mediator;

        public TypePermissionController(Mediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("all")]
        //[Route("all")]
        public async Task<IActionResult> All([FromQuery] GetAllTypePermissionsQuery query)
        {

            try
            {

               // query.WithPermission = withpermission;

                return Ok(await _mediator.Send(query));

            }
            catch (HttpRequestExceptionEx ex)
            {

                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }


        }


        // GET api/
        [HttpGet("forid/{id}")]
        public async Task<IActionResult> ForId([FromQuery] GetTypePermissionsQuery query, int id)
        {
            try
            {
                query.Id = id;
                return Ok(await _mediator.Send(query));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }
        }

        // POST api/
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] CreateTypePermissionCommand cmd, TypePermissionDto typepermission)
        {

            try
            {

                if (typepermission == null)
                {

                    ModelState.AddModelError("", "El permiso no puede ser en blanco");
                    return BadRequest(ModelState);
                }

                cmd.TypePermission = typepermission;
                return Ok(await _mediator.Send(cmd));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }

        }

        // PUT api/
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] UpdateTypePermissionCommand cmd, TypePermissionDto typepermission)
        {

            try
            {

                if (typepermission == null)
                {

                    ModelState.AddModelError("", "El tipo de permiso no puede ser en blanco");
                    return BadRequest(ModelState);
                }

                cmd.TypePermission = typepermission;
                return Ok(await _mediator.Send(cmd));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }

        }

        // DELETE api/
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteTypePermissionCommand cmd, int id)
        {

            try
            {

                if (id <= 0)
                {

                    ModelState.AddModelError("", "El tipo de permiso no puede ser en blanco");
                    return BadRequest(ModelState);
                }

                cmd.Id = id;
                return Ok(await _mediator.Send(cmd));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }

        }


    }
}
