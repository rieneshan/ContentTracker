using webapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace webapi.Models
{
    public class SeriesWatched
    {
        public int id { get; set; }

        public string username { get; set; }
        public string seriesName { get; set; }
        public DateTime insertdate { get; set; }
    }


}
