using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace webapi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<EpisodesWatched> episodesWatched { get; set; }
        public DbSet<SeriesWatched> seriesWatched { get; set; }

    }
}
