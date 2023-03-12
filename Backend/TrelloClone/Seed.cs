using System.Diagnostics.Metrics;
using TrelloClone.Data;
using TrelloClone.Models;

namespace TrelloClone
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {

            if (!dataContext.Memberships.Any())
            {

                var amituser = new User()
                {
                    Username = "AmitG",
                    Password = "password"
                };

                var memberships = new List<Membership>()
                {
                    new Membership()
                    {
                        User = amituser,
                        KanbanBoard = new KanbanBoard()
                        {
                            Title = "First Board",
                            BoardLists = new List<BoardList>()
                            {
                                new BoardList()
                                {
                                    Title = "First board's first list",
                                    Cards = new List<Card>()
                                    {
                                        new Card()
                                        {
                                            Title = "First board, first List, first Card",
                                            Description = "The first card that was listed on the first list of the first board",
                                            Status = 1,
                                            Comments = new List<Comment>()
                                            {
                                                new Comment()
                                                {
                                                    Text = "Some commenty text",
                                                    Author= amituser

                                                }
                                            },
                                        }
                                    }
                                }
                            }
                        }
                    }
                };


            
                dataContext.Memberships.AddRange(memberships);
                dataContext.SaveChanges();
            }
        }
    }
}
