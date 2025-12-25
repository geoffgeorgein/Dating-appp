using System;
using System.Security.Cryptography.X509Certificates;
using dating_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace dating_app.Data;


    public class AppDbContext(DbContextOptions options): DbContext(options)
    {
        public DbSet<AppUser> Users {get; set;}
    }

