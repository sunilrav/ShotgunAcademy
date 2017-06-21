using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace ShotgunAcademy.Controllers
{
    public class SgaExtController : Controller
    {
        [Route("rumblelist")]
        public IActionResult RumbleList()
        {
            SgaExtContext context = HttpContext.RequestServices.GetService(typeof(SgaExtContext)) as SgaExtContext;

            return View(context.GetAllRumblePlayers());
        }
    }
}
