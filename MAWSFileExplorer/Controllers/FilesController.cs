using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace MAWSFileExplorer
{
    public class FilesController : ApiController
    {
        public HttpResponseMessage GetProduct()
        {
            string response;
            string fileLocation = WebConfigurationManager.AppSettings["location"];
            FileExplorer explorer = new FileExplorer();
            if (Utility.IsRunningInMAWS())
            {
                fileLocation = Environment.ExpandEnvironmentVariables(@"%HOME%");
            }
            try
            {
                response = JsonConvert.SerializeObject(explorer.GetDirectories(fileLocation), Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                var resp = Utility.ExceptionMessage(ex);
                throw new HttpResponseException(resp);
            }

            return new HttpResponseMessage { Content = new StringContent(response, System.Text.Encoding.UTF8, "application/json") };
        }

        public HttpResponseMessage Post([FromBody]string fileLocation)
        {
            string response;
            FileExplorer explorer = new FileExplorer();
            try
            {
                response = JsonConvert.SerializeObject(explorer.GetDirectories(fileLocation), Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                var resp = Utility.ExceptionMessage(ex);
                throw new HttpResponseException(resp);
            }

            return new HttpResponseMessage { Content = new StringContent(response, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}