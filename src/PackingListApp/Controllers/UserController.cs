using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackingListApp.Interfaces;
using PackingListApp.Models;
using Microsoft.AspNet.OData.Query;
using PackingList.Core.Queries;

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

        //GET: api/User
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<UserModel> options)
        {
            var list = _userService.GetAll();
            return Ok(new QueryResult<UserModel>(list, list.Count));
        }

        ////GET: api/User/5
        ////[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userService.Get(id));
        }

        //POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] NewUserModel value)
        {
            var id = _userService.Add(value);
            return Ok(new CommandHandledResult(true, id.ToString(), id.ToString(), id.ToString()));

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel item)
        {
            _userService.Put(id, item);
            return Ok(new CommandHandledResult(true, id.ToString(), id.ToString(), id.ToString()));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _userService.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            _userService.Delete(item);
            return Ok(new CommandHandledResult(true, id.ToString()));
                       
        }

    }
}
