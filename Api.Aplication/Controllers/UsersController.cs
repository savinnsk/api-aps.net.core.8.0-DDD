
using System.Net;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Aplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController  : ControllerBase
    {
            private readonly IUserService _userService;


            public  UsersController(IUserService userService){
                _userService = userService;
            }

         
           [HttpGet]
           [Authorize("Bearer")]
           public async Task<IActionResult> GetAll(Guid id){

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            try {

                var result = await this._userService.GetAll();

                return Ok(result);

            }catch(ArgumentException ex){
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

       }
           [HttpGet]
           [Route("{id}", Name = "GetWithId")]
           [Authorize("Bearer")]
           public async Task<IActionResult> Get(Guid id){

            if (!ModelState.IsValid){ 
                return BadRequest(ModelState); 
            }
            
            try {

                var result = await this._userService.GetOne(id);

                return Ok(result);

            }catch(ArgumentException ex){
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

       }

           [HttpPost]
           [AllowAnonymous]
           public async Task<IActionResult> Create([FromBody]UserDto user){

            if (!ModelState.IsValid){ 
                return BadRequest(ModelState); 
            }
            
            try {

                var result = await this._userService.Create(user);

                return Ok(result);

             

            }catch(ArgumentException ex){
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

       }


           [HttpPut]
           [Authorize("Bearer")]
           public async Task<IActionResult> Update([FromBody]UserDto user){

            if (!ModelState.IsValid){ 
                return BadRequest(ModelState); 
            }
            
            try {

                var result = await this._userService.Update(user);

                if (result != null){
                    return Ok(result);
                }else{
                    return BadRequest();
                }

             

            }catch(ArgumentException ex){
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

       }
           [Authorize("Bearer")]
           [HttpDelete ("{id}")]
           public async Task<IActionResult> Delete(Guid id){

            if (!ModelState.IsValid){ 
                return BadRequest(ModelState); 
            }
            
            try {

                var result = await this._userService.Delete(id);

                if (result != null){
                    return Ok(result);
                }else{
                    return BadRequest();
                }

             

            }catch(ArgumentException ex){
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

    }
  }
}