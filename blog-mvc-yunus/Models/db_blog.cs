namespace blog_mvc_yunus.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class db_blog : DbContext
    {
        public db_blog()
            : base("name=db_blog")
        {
        }

        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Makaleler> Makalelers { get; set; }
        public virtual DbSet<Uyeler> Uyelers { get; set; }
        public virtual DbSet<Yetkilendirme> Yetkilendirmes { get; set; }
        public virtual DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
