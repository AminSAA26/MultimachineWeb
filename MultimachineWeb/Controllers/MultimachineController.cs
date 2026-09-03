using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultimachineWeb.Classes;
using System.Text.Json;

namespace MultimachineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultimachineController : ControllerBase
    {
        ApplicationSettings _applicationSettings;
        ConnectionStrings _connectionStrings;
        ClsUpdate _clsUpdate;
        
        public MultimachineController(ApplicationSettings applicationSettings,ConnectionStrings connectionStrings,ClsUpdate clsUpdate)
        { 
        _applicationSettings = applicationSettings; 
        _connectionStrings = connectionStrings;
        _clsUpdate = clsUpdate;
        }



        [HttpGet]
        [Route("get")]
        public IActionResult Getdata(string servername,string rundate)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Serializes the object into a raw JSON string
            // return JsonSerializer.Serialize(infolist, options);
           var _json =   _clsUpdate.GetList(servername,rundate);
            return Ok(new { success = true, message = _json });
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateIssue([FromBody] ClsMultiMachineInfo updatedRecord)
        {
            if (updatedRecord == null)
            {
                return BadRequest("Invalid record data payload.");
            }
            _clsUpdate.updateData(updatedRecord);
            //return Ok();
            // FIX: Pass a JSON payload back instead of leaving it empty
            return Ok(new { success = true, message = "Record updated successfully" });
        }


    } }
