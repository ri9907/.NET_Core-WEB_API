using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using Entities;
using AutoMapper;
using DTOs;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServieces _userServieces;
        private IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserServieces userServieces, IMapper mapper , ILogger<UsersController> logger )
        {
            _logger = logger;
            _userServieces = userServieces;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task <ActionResult<User>> GetById(int id)
        {
            return await _userServieces.GetById(id);

        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto userDto)
        {
            if(_userServieces.checkPassword(userDto.Password) >= 2)
            {
                User user = _mapper.Map<UserRegisterDto, User>((userDto));
                return Ok(await _userServieces.Register(user));
            }
           return BadRequest();         
        }
      
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLoginDto userLogin)
        {
            User user = _mapper.Map<UserLoginDto, User>((userLogin));
            User user1 =await _userServieces.Login(user);
            if (user1 != null)
            {
                _logger.LogInformation($"Login atempted with username, {userLogin.Email} &{userLogin.Password}");
                return Ok(user1);
            }
            return Forbid();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UserRegisterDto newUser)
        {
            User user = _mapper.Map<UserRegisterDto, User>((newUser));
            User user1 =await _userServieces.UpdateUser(id, user);
            if (user1 != null)
            {
                _logger.LogInformation($"Login attempted with user name, {newUser.Email} and password {newUser.Password}");
                return Ok(user1);
            }
            return BadRequest();
            
        }


        [HttpPost]
        [Route("checkPassword")]
        public ActionResult<int> checkPassword([FromBody] string password)
        {
                return Ok(_userServieces.checkPassword(password));
            
        }
        
    }
}
