using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mi9
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                string json = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                Mi9.Lib.Models.PayloadModel model = new Lib.Models.PayloadModel();
                try
                {
                    model.Read(json);
                    string s = model.Write();
                    Response.ContentType = "text/json";
                    Response.Write(s);
                }
                catch (Mi9.Lib.Exceptions.PayloadReadException pre)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pre.Message);
                }
                catch (Mi9.Lib.Exceptions.PayloadValidationException pve)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pve.Message);
                }
                catch (Mi9.Lib.Exceptions.PayloadWriteException pwe)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    d.Add("error", pwe.Message);
                }
                Response.ContentType = "text/json";
                Response.Write(JsonConvert.SerializeObject(d));
                return;
            }
            Response.ContentType = "text/plain";
            Response.Write("Invalid request. Only post allowed.");

        }
    }
}