using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMSWebApi.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private ISmsProvider _smsProvider;
        public DataController(ISmsProvider smsProvider)
        {
            _smsProvider = smsProvider;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       

        [HttpPost]
        [Route("sendSms")]
        public object SendSms([FromBody] dynamic payload)
        {
            var response = _smsProvider.SendSms(payload);
            return response;
        }



    }
}
