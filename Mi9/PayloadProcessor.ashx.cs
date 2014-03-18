using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mi9
{
    /// <summary>
    /// Summary description for PayloadProcessor
    /// </summary>
    public class PayloadProcessor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                string json = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();
                Mi9.Lib.Models.PayloadModel model = new Lib.Models.PayloadModel();
                try
                {
                    model.Read(json);
                    string s = model.Write();
                    context.Response.ContentType = "text/json";
                    context.Response.Write(s);
                }
                catch (Mi9.Lib.Exceptions.PayloadReadException pre)
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pre.Message);
                }
                catch (Mi9.Lib.Exceptions.PayloadValidationException pve)
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pve.Message);
                }
                catch (Mi9.Lib.Exceptions.PayloadWriteException pwe)
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pwe.Message);
                }
                context.Response.ContentType = "text/json";
                context.Response.Write(JsonConvert.SerializeObject(d));
                return;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Invalid request. Only post allowed.");
            

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}