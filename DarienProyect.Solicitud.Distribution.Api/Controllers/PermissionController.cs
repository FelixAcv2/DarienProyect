using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acv2.SharedKernel.Crosscutting.Exceptions;
using DarienProyect.Solicitud.Aplication.Permissions.Commands;
using DarienProyect.Solicitud.Aplication.Permissions.Querys;
using DarienProyect.Solicitud.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DarienProyect.Solicitud.Distribution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {


        private readonly Mediator _mediator;

        public PermissionController(Mediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api
        [HttpGet("all/{withtypepermission}")]
        [Route("all")]
        public async Task<IActionResult> All([FromQuery] GetAllPermissionsQuery query,bool withtypepermission=false)
        {

            try
            {

                query.WithTypePermission = withtypepermission;

                return Ok(await _mediator.Send(query));

            }
            catch (HttpRequestExceptionEx ex)
            {

                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }


        }

        // GET api/
        [HttpGet("forid/{id}/{withtypePermission}")]
        [Route("forid/{id}")]
        public async Task<IActionResult> ForId([FromQuery] GetPermissionsQuery query,int id,bool withtypePermission=false)
        {
            try
            {
                query.Id = id;
                query.WithTypePermission = withtypePermission;
                return Ok(await _mediator.Send(query));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }
        }

        // POST api/
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] CreatePermissionCommand cmd,PermissionDto permission)
        {

            try
            {

                if (permission == null)
                {

                    ModelState.AddModelError("", "El permiso no puede ser en blanco");
                    return BadRequest(ModelState);
                }

                cmd.Permission = permission;
                return Ok(await _mediator.Send(cmd));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }

        }

        // PUT api/
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] UpdatePermissionCommand cmd, PermissionDto permission)
        {

            try
            {

                if (permission == null)
                {

                    ModelState.AddModelError("", "El permiso no puede ser en blanco");
                    return BadRequest(ModelState);
                }

                cmd.Permission = permission;
                return Ok(await _mediator.Send(cmd));
            }
            catch (HttpRequestExceptionEx ex)
            {
                return new NotFoundObjectResult(string.Format("Status code: {0} Message: {1}", ex.HttpCode, ex.Message));
            }

        }

        // DELETE api/
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeletePermissionCommand cmd, int id)
        {

            try
            {

                if (id <= 0)
                {

                    ModelState.AddModelError("", "El permiso no puede ser en blanco");
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
