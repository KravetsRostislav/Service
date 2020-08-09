using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services;
using Shop.API.Extensions;
using Shop.API.Resources;

namespace Shop.API.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ResponseData> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resource = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            var result = new ResponseData
            {
                Data = resource,
                Message = "",
                Success = true
            };
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var result = await userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var result = await userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }
    }
}