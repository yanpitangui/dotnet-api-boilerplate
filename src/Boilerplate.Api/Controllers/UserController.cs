using System;
using System.Threading.Tasks;
using Boilerplate.Application.DTOs.Auth;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// Authenticates the user and returns the token information.
        /// </summary>
        /// <param name="loginInfo">Email and password information</param>
        /// <returns>Token information</returns>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JwtDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginInfo)
        {
            var user = await _userService.Authenticate(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(_authService.GenerateToken(user));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GetUserDto>> CreateAccount(CreateUserDto dto)
        {
            var newAccount = await _userService.CreateUser(dto);
            return CreatedAtAction("GetAccountById", new { id = newAccount.Id }, newAccount);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePassword(Guid id, [FromBody] UpdatePasswordDto dto)
        {
            await _userService.UpdatePassword(id, dto);
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userService.DeleteUser(id);
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}
