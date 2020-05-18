using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FDAWebAPI.DAL.Model
{
    public class Drug
    {
        [JsonProperty("term")]
        public string Term { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}