namespace EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookLibraryEF : DbContext
    {
        public BookLibraryEF()
            : base("name=BookLibraryContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }
        public virtual DbSet<DictBookGenre> DictBookGenre { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(e => e.Borrow)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DictBookGenre>()
                .HasMany(e => e.Book)
                .WithRequired(e => e.DictBookGenre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Borrow)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
