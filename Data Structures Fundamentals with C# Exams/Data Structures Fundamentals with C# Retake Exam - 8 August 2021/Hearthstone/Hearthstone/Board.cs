using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    Dictionary<string, Card> cards = new Dictionary<string, Card>();
    public bool Contains(string name) => cards.ContainsKey(name);

    public int Count() => cards.Count;

    public void Draw(Card card)
    {
        if (cards.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }

        cards.Add(card.Name, card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
     => cards.Values.Where(x => x.Score >= start && x.Score <= end).OrderByDescending(x => x.Level);

    public void Heal(int health)
    {
        var lowerBound = int.MaxValue;

        Card lowerCard = null;

        foreach (var card in cards)
        {
            if (card.Value.Health < lowerBound)
            {
                lowerBound = card.Value.Health;
                lowerCard = card.Value;
            }
        }

        lowerCard.Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {

        return cards.Values.Where(x => x.Name.StartsWith(prefix))
            .OrderBy(x => string.Join("", x.Name.Reverse()))
            .ThenBy(x => x.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!Contains(attackerCardName) || !Contains(attackedCardName))
        {
            throw new ArgumentException();
        }

        var attackerCard = cards[attackerCardName];
        var attackedCard = cards[attackedCardName];

        if (attackerCard.Level != attackedCard.Level)
        {
            throw new ArgumentException();
        }

        if (attackedCard.Health <= 0 || attackedCard.Health <= 0)
        {
            return;
        }

        attackedCard.Health -= attackerCard.Damage;

        if (attackedCard.Health <= 0)
        {
            attackerCard.Score += attackedCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (!Contains(name))
        {
            throw new ArgumentException();
        }

        cards.Remove(name);
    }

    public void RemoveDeath()
    {
        var deathCards = cards.Values.Where(x => x.Health <= 0).ToList();

        foreach (var deathCard in deathCards)
        {
            cards.Remove(deathCard.Name);
        }
    }

    public IEnumerable<Card> SearchByLevel(int level)
        => cards.Values.Where(x => x.Level == level).OrderByDescending(x => x.Score);
}