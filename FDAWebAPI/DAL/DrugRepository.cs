using System;
using System.Collections.Generic;
using System.Linq;
using FDAWebAPI.DAL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace FDAWebAPI.DAL
{
    public class DrugRepository : IDrugRepository
    {
        public List<Drug> GetMainIngredients(String reaction)
        {
            const int amount = 10;
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
                WebResponse response = null;
                try
                {
                    response = request.GetResponse();
                }
                catch (Exception)
                {
                    return null;
                }

                if (response != null)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        List<Drug> drugsList = null;
                        var results = reader.ReadToEnd();
                        if (drugs != null)
                        {
                            JObject o = JObject.Parse(results);
                            drugsList = JsonConvert.DeserializeObject<List<Drug>>(o["results"].ToString());
                            return drugsList.OrderByDescending(drug => drug.Count).Take(amount).ToList();
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}