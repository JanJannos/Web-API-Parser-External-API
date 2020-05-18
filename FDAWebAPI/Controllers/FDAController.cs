using FDAWebAPI.DAL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FDAWebAPI.Controllers
{
    public class FDAController : ApiController
    {
        // https://localhost:44344/api/fda
        [HttpGet]
        //[Route("api/FDA/{reaction=noval}")]
        [Route("api/FDA")]
        [Route("api/FDA/{reaction}")]
        public HttpResponseMessage Get(String reaction = null)
        {

            String apiUrl = "https://api.fda.gov/drug/event.json?search=patient.reaction.reactionmeddrapt.exact:<reaction>&count=patient.drug.medicinalproduct";
            if (!String.IsNullOrEmpty(reaction))
            {
                // replace reaction in reqest
                apiUrl = apiUrl.Replace("<reaction>", reaction);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            try
            {
                List<Drug> drugs = new List<Drug>();
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    List<Drug> myObjectList = null;
                    var results = reader.ReadToEnd();
                    if (drugs != null)
                    {
                        //dynamic data = JObject.Parse(results);
                        JObject o = JObject.Parse(results);
                        myObjectList = JsonConvert.DeserializeObject<List<Drug>>(o["results"].ToString());
                     
                        // manipulate data
                        //if (data.results != null)
                        //{
                        //   myObjectList = JsonConvert.DeserializeObject<List<Drug>>(data.results);
                        //}

                        //return Request.CreateResponse(HttpStatusCode.OK, data);
                        return Request.CreateResponse(HttpStatusCode.OK, myObjectList);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, "");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "JAN");
            }
        }


        public String Delete(int id)
        {
            throw new NotImplementedException();
        }

        public String Put(String s)
        {
            throw new NotImplementedException();
        }

        public String Post(String s)
        {
            throw new NotImplementedException();
        }

    }
}