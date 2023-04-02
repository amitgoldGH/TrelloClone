using Microsoft.EntityFrameworkCore;
using TrelloClone.Models;

namespace TrelloClone.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

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

            base.OnModelCreating(modelBuilder);

            // Link table User M<->M KanbanBoard
            modelBuilder.Entity<Membership>().HasKey(mem => new { mem.UserId, mem.BoardId });
            modelBuilder.Entity<Membership>()
                .HasOne(mem => mem.User)
                .WithMany(usr => usr.Memberships)
                .HasForeignKey(mem => mem.UserId);

            modelBuilder.Entity<Membership>()
                .HasOne(mem => mem.KanbanBoard)
                .WithMany(board => board.Memberships)
                .HasForeignKey(mem => mem.BoardId);


            // Link table User M<->M Card
            modelBuilder.Entity<Assignment>().HasKey(ass => new { ass.UserId, ass.CardId });
            modelBuilder.Entity<Assignment>()
                .HasOne(ass => ass.User)
                .WithMany(usr => usr.Assignments)
                .HasForeignKey(ass => ass.UserId);

            modelBuilder.Entity<Assignment>()
                .HasOne(ass => ass.Card)
                .WithMany(card => card.Assignments)
                .HasForeignKey(ass => ass.CardId);


            //Many to one Comment M<->1 User
            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Author)
                .WithMany(author => author.Comments)
                .HasForeignKey(comment => comment.AuthorName);

            // Many to one Comment M<->1 Card
            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Card)
                .WithMany(card => card.Comments)
                .HasForeignKey(comment => comment.CardId);

            //  Many to one Card M<->1 BoardList
            modelBuilder.Entity<Card>()
                .HasOne(card => card.BoardList)
                .WithMany(bList => bList.Cards)
                .HasForeignKey(card => card.BoardListId);

            // Many to one BoardList M<->1 KanbanBoard
            modelBuilder.Entity<BoardList>()
                .HasOne(bList => bList.KanbanBoard)
                .WithMany(kBoard => kBoard.BoardLists)
                .HasForeignKey(bList => bList.BoardId);
        }
    }
}
