using FDAWebAPI.DAL;
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
        private readonly IDrugRepository drugRepository = new DrugRepository();

        // https://localhost:44344/api/fda
        [HttpGet]      
        [Route("api/FDA")]
        [Route("api/FDA/{reaction}")]
        public HttpResponseMessage Get(String reaction = null)
        {
            List<Drug> drugList = drugRepository.GetMainIngredients(reaction);
            if (drugList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, drugList);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);          
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