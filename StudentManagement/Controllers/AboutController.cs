using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [Route("[controller]")]
    public class AboutController
    {
        [Route("")]
        [Route("test")]
        [Route("test/Me")]
        public string Me()
        {
            return "Me";
        }
        [Route("[action]")]
        public string Company()
        {
            return "HuaWei";
        }
    }
}
