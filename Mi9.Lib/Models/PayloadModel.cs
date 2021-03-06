﻿using Mi9.Lib.Exceptions;
using Mi9.Lib.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi9.Lib.Models
{
    public class PayloadModel : BaseModel, IModel
    {
        public virtual RequestDTO InputPayload{get;private set;}
        public virtual ResponseDTO OutputPayload { get; private set; }

        public override void Validate()
        {
            if(InputPayload == null || InputPayload.Payload.Count < 1)
            {
                this.IsValid = false;
                throw new PayloadValidationException("Could not decode request: Empty payload found.");
            }
        }


        public void Read(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new PayloadReadException("Could not decode request: Empty payload found");
            }
            try
            {
                this.InputPayload = JsonConvert.DeserializeObject<RequestDTO>(source);
            }
            catch (Exception ex)
            {
                this.IsValid = false;
                throw new PayloadReadException("Could not decode request: JSON parsing failed", ex);
            }
        }

        public void DoQuery()
        {
            List<Mi9.Lib.Models.RequestDTO.PayloadObj> pyloads = this.InputPayload.Payload.Where(pl => pl.Drm == true && pl.EpisodeCount > 0).ToList();
            OutputPayload = new ResponseDTO();
            foreach(Mi9.Lib.Models.RequestDTO.PayloadObj p in pyloads)
            {
                OutputPayload.Response.Add(new ResponseDTO.ResponseObject(){
                    Image = p.Image != null ? p.Image.ShowImage : "",
                    Slug = p.Slug,
                    Title = p.Title
                });
            }
        }

        public string Write()
        {
            string output = null;
            this.Validate();
            try
            {
                if (OutputPayload == null || OutputPayload.Response.Count < 1)
                    this.DoQuery();
                output = JsonConvert.SerializeObject(this.OutputPayload, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new Mi9.Lib.Utils.LowercaseContractResolver() });
            }
            catch {
                this.IsValid = false;
                throw new PayloadWriteException("Could not decode request: JSON generation failed.");
            }
            return output;
        }
    }
}
