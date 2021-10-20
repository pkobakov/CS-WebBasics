namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardService
    {
        int AddCard(AddCardInputModel model);
        void AddCardToUserCollection(string userId, int cardId);
        void RemoveCardFromUserCollection(string userId, int cardId);
        IEnumerable<CardViewModel> GetAll();
        IEnumerable<CardViewModel> GetByUserId(string userId);

    }
}
