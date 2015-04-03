using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace MAWSFileExplorer
{
    public static class Utility
    {
        public static bool IsRunningInMAWS()
        {
            if (HttpContext.Current.Request.Headers["Host"].Contains("scm") || HttpContext.Current.Request.Headers["Host"].Contains("azurewebsites.net"))
                return true;
            else
                return false;
        }

        public static HttpResponseMessage ExceptionMessage(Exception ex)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Unable to load data!!!"),
                ReasonPhrase = ex.Message
            };
            return resp;
        }
    }
}