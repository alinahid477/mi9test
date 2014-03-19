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
            Mi9.Lib.Models.ErrorDTO errorDTO = new Lib.Models.ErrorDTO();
            //Response.AddHeader("Content-Disposition", "attachment;filename=response.json");
            Response.ContentType = "application/json; charset=utf-8";
            string responseStr = "";
            try
            {
                if (!Request.HttpMethod.Equals("POST"))
                {
                    throw new Mi9.Lib.Exceptions.PayloadReadException("Could not decode request: Only post is allowed");
                }

                string json = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                Mi9.Lib.Models.PayloadModel model = new Lib.Models.PayloadModel();


                model.Read(json);
                responseStr = model.Write();
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Response.Write(s);
            }
            catch (Mi9.Lib.Exceptions.PayloadReadException pre)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pre.Message;
            }
            catch (Mi9.Lib.Exceptions.PayloadValidationException pve)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pve.Message;
            }
            catch (Mi9.Lib.Exceptions.PayloadWriteException pwe)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pwe.Message;

            }
            finally
            {
                if (!string.IsNullOrEmpty(errorDTO.Error))
                    responseStr = JsonConvert.SerializeObject(errorDTO, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new Mi9.Lib.Utils.LowercaseContractResolver() });

                Response.Write(responseStr);
            }

            
        }
    }
}