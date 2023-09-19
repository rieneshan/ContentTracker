using webapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace webapi.Models
{
    public class EpisodesWatched
    {
        public int id { get; set; }

        public string username { get; set; }
        public string seriesName { get; set; }
        public string season { get; set; }
        public string episode { get; set; }
        public DateTime insertdate { get; set; }
    }


}
