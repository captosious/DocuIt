using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DocuItService.Controllers
{
    public class InventoryController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public InventoryController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        //[HttpGet("GetInventory")]
        //public IEnumerable<InventoryReport> GetInventory([FromBody] InventoryReport params)
        //{
        //    //IEnumerable<InventoryReport> 
        //    //return 
        //}
    }
}
