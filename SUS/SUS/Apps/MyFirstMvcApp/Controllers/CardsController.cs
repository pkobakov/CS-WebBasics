namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CardsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public HttpResponse Add()
        {
            return this.View();
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var data = new ApplicationDbContext();
            var cardViewModel = data.Cards.Select(x => new CardViewModel 
            { 
            
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            Health = x.Health,
            Attack = x.Attack, 
            Type = x.Keyword,
            Description = x.Description
        
            }).ToList();
        
            return View(cardViewModel);
        
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd() 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            //validation:
            
            if (Request.FormData["name"].Length<5)
            {
                return this.Error("Name should be at least 5 characters long.");
            }
           db.Cards.Add ( new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"]
           });

            db.SaveChanges();

            return this.Redirect("/Cards/All");
        }
    }
}
