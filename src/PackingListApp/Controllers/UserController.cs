using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackingList.Core.Queries;
using PackingListApp.DTO;
using PackingListApp.Interfaces;
using PackingListApp.Models;

namespace PackingListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<UserModel> options)
        {
            var list = _userService.GetAllUsers();
            return Ok(new QueryResult<UserModel>(list, list.Count));
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.GetUser(id));
        }


        // DELETE: api/User
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] NewUserModel value)
        {
            var id = _userService.AddUser(value);
            return Ok(new CommandHandledResult(true, id.ToString(), id.ToString(), id.ToString()));

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel item)
        {
            _userService.PutUser(id, item);
            return Ok(new CommandHandledResult(true, id.ToString(), id.ToString(), id.ToString()));
        }
    }
}
