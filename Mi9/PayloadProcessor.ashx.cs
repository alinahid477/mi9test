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
            Mi9.Lib.Models.ErrorDTO errorDTO = new Lib.Models.ErrorDTO();
            //context.Response.AddHeader("Content-Disposition", "attachment;filename=context.Response.json");
            context.Response.ContentType = "application/json; charset=utf-8";
            string responseStr = "";
            try
            {
                if (!context.Request.HttpMethod.Equals("POST"))
                {
                    throw new Mi9.Lib.Exceptions.PayloadReadException("Could not decode request: Only post method is allowed");
                }

                string json = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();
                Mi9.Lib.Models.PayloadModel model = new Lib.Models.PayloadModel();


                model.Read(json);
                responseStr = model.Write();
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //context.Response.Write(s);
            }
            catch (Mi9.Lib.Exceptions.PayloadReadException pre)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pre.Message;
            }
            catch (Mi9.Lib.Exceptions.PayloadValidationException pve)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pve.Message;
            }
            catch (Mi9.Lib.Exceptions.PayloadWriteException pwe)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                errorDTO.Error = pwe.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorDTO.Error))
                    responseStr = JsonConvert.SerializeObject(errorDTO, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new Mi9.Lib.Utils.LowercaseContractResolver() });

                context.Response.Write(responseStr);
            }
            

            
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