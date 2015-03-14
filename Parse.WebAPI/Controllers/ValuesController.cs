using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Razor.Parser;

namespace Parse.WebAPI.Controllers
{
    public class DocFileController : ApiController
    {
        static string response;
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return response;
        }

        // POST api/values
        public void Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                var filename = httpRequest.Files[0].FileName;

                for (var i = 0; i <= httpRequest.Files.Count - 1; i++)
                {
                    var postedFile = httpRequest.Files[i];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }

                var pl = new Parser.ParseLogic();
                response = Parser.ParseHelper.SerializeToJson(pl.ReadReports(docfiles));
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}