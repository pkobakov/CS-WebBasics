namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels.Cards;
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
        public HttpResponse DoAdd(AddCardInputModel model) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            //validation:
            
            if (model.Name.Length<5)
            {
                return this.Error("Name should be at least 5 characters long.");
            }
           db.Cards.Add ( new Card
            {
                Attack = model.Attack,
                Health = model.Health,
                Description =model.Description,
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword
           });

            db.SaveChanges();

            return this.Redirect("/Cards/All");
        }
    }
}
