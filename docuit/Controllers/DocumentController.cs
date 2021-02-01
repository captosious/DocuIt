using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DocuItService.Controllers
{
    public class DocumentController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public DocumentController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        
    }
}
