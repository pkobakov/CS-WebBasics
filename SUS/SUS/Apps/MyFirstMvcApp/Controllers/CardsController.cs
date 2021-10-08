namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;
    using System.Linq;

    public class CardsController : Controller
    {
 
        private readonly CardService cardService;

        public CardsController(CardService cardService)
        {
 
            this.cardService = cardService;
        }


        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cardViewModel = this.cardService.GetAll();

            return View(cardViewModel);
        
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();

            var cards = this.cardService.GetByUserId(userId);

            return this.View(cards);
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel model) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            //validation:
            
            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length<5 || model.Name.Length > 15)
            {
                return this.Error("Name should be between 5 and 15 characters long.");
            }

            if (string.IsNullOrWhiteSpace(model.Name) || !Uri.TryCreate(model.Image,UriKind.Absolute, out _))
            {
                return this.Error("Url cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(model.Keyword))
            {
                return this.Error("The keyword is required");
            }

            if (model.Attack<0)
            {
                return this.Error("Attack cannot be negative.");
            }

            if (model.Health < 0)
            {
                return this.Error("Health cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 200)
            {
                return this.Error("Description cannot be empty and must content less than 200 characters.");
            }

            var userId = this.GetUserId();
            var cardId = this.cardService.AddCard(model);
            this.cardService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse AddToCollection(int cardId) 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            this.cardService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        
        }

        public HttpResponse RemoveFromCollection(int cardId) 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            this.cardService.RemoveCardFromUserCollection(userId, cardId);

            return this.Redirect("/Cards/Collection");

        }
    }
}
