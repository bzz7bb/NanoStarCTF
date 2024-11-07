using Microsoft.EntityFrameworkCore;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoStarCTF.Domain.Entitys.Submissions;

namespace NanoStarCTF.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<Challenge> CTFs { get; set; }

        public virtual DbSet<Hints> Hints { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<HomeAddress> Address { get; set; } 
                                         
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
    }
}
