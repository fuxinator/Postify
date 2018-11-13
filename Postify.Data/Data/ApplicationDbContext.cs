using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Postify.Data.Models;

namespace Postify.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>().ToTable("Post");
            builder.Entity<Comment>().ToTable("Comment");
            builder.Entity<PostLike>().ToTable("PostLike");
            builder.Entity<CommentLike>().ToTable("CommentLike");

            base.OnModelCreating(builder);
        }
    }
}
