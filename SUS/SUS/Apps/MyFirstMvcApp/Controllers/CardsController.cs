namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {

        public HttpResponse Add()
        {
            return this.View();
        }
        public HttpResponse All()
        {
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
        
            return View(new AllCardsViewModel{AllCards= cardViewModel});
        
        }

        public HttpResponse Collection()
        {
            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd() 
        {


           var data = new ApplicationDbContext();

            //validation:
            
            if (Request.FormData["name"].Length<5)
            {
                return this.Error("Name should be at least 5 characters long.");
            }
           data.Cards.Add ( new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"]
           });

            data.SaveChanges();

            return this.Redirect("/");
        }
    }
}
