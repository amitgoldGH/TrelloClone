using Microsoft.EntityFrameworkCore;
using TrelloClone.Models;

namespace TrelloClone.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<BoardList> BoardLists { get; set; }

        public DbSet<KanbanBoard> Boards { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Membership> Memberships { get; set; } // Link table

        public DbSet<Assignment> Assignments { get; set; } // Link table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>().HasKey(mem => new { mem.UserId, mem.BoardId });
            
            modelBuilder.Entity<Membership>()
                .HasOne(user => user.User)
                .WithMany(mem => mem.Memberships)
                .HasForeignKey(user => user.UserId);

            modelBuilder.Entity<Membership>()
                .HasOne(board => board.KanbanBoard)
                .WithMany(mem => mem.Memberships)
                .HasForeignKey(board => board.BoardId);


            modelBuilder.Entity<Assignment>().HasKey(ass => new { ass.UserId, ass.CardId });
            modelBuilder.Entity<Assignment>()
                .HasOne(user => user.User)
                .WithMany(ass => ass.Assignments)
                .HasForeignKey(user => user.UserId);

            modelBuilder.Entity<Assignment>()
                .HasOne(card => card.Card)
                .WithMany(ass => ass.Assignments)
                .HasForeignKey(card => card.CardId);
        }
    }
}
