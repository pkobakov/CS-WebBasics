namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.ViewModels.Cards;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CardService : ICardService
    {
        private readonly ApplicationDbContext db;

        public CardService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddCard(AddCardInputModel model)
        {
            var card = new Card
            {
                Attack = model.Attack,
                Health = model.Health,
                Description = model.Description,
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();
            return card.Id;
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            var cardViewModel = db.Cards.Select(x => new CardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Health = x.Health,
                Attack = x.Attack,
                Type = x.Keyword,
                Description = x.Description

            }).ToList();

            return cardViewModel;
        }

        public IEnumerable<CardViewModel> GetByUserId(string userId)
        {
            return this.db.UserCards.Where(x => x.UserId == userId)
                                    .Select(x => new CardViewModel
                                    {
                                        Id = x.CardId,
                                        Attack = x.Card.Attack,
                                        Health = x.Card.Health,
                                        Name = x.Card.Name,
                                        ImageUrl = x.Card.ImageUrl,
                                        Description = x.Card.Description,
                                        Type = x.Card.Keyword
                                    
                                    }).ToList();
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (this.db.UserCards.Any(x=>x.UserId == userId && x.CardId == cardId))
            {
                var error = "Card is already in the collection";
                Console.WriteLine(error);
            }

            this.db.UserCards.Add(new UserCard
            {
                
               UserId = userId,
               CardId = cardId
            });

            this.db.SaveChanges();
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCard = this.db.UserCards.FirstOrDefault(x => x.CardId == cardId && x.UserId == userId);

            if (userCard == null)
            {
                Console.WriteLine("This requested card is null");
            }

            this.db.UserCards.Remove(userCard);
            this.db.SaveChanges();


        }
    }
}
