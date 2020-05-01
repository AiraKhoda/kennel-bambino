using kennel_bambino.web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GroupsController:ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SelectListItem>>> GetSubGroups(int id)
        {
            try
            {
                return Ok(await _groupService.GetSubGroupSelectListAsync(id));
            }
            catch 
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "SubGroup not fetch properly");
            }
        }

    }
}
