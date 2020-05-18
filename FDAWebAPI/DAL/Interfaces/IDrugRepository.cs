using FDAWebAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDAWebAPI.DAL
{
    public interface IDrugRepository
    {
        List<Drug> GetMainIngredients(String reaction);
    }
}
