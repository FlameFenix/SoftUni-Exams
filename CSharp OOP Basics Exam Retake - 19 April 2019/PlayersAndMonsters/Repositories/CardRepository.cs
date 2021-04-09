using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Repositories
{
    public class CardRepository : ICardRepository
    {
        private List<ICard> cards;
        public CardRepository()
        {
            cards = new List<ICard>();
        }
        public int Count
        {
            get => this.cards.Count;
        }

        public IReadOnlyCollection<ICard> Cards
        {
            get => this.cards.ToList().AsReadOnly();
        }

        public void Add(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            ICard existingCard = cards.FirstOrDefault(x => x.Name == card.Name);

            if (cards.Contains(existingCard))
            {
                throw new ArgumentException($"Card {card.Name} already exists!");
            }

            cards.Add(card);
        }

        public ICard Find(string name)
        {
            ICard card = cards.FirstOrDefault(x => x.Name == name);

            if(card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            return card;
        }

        public bool Remove(ICard card)
        {
            if(card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            return cards.Remove(card);
        }
    }
}
