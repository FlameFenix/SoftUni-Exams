using System;
using System.Collections.Generic;
using System.Text;

public class Board : IBoard
{
    public bool Contains(string name)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public void Draw(Card card)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        throw new NotImplementedException();
    }

    public void Heal(int health)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        throw new NotImplementedException();
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        throw new NotImplementedException();
    }

    public void Remove(string name)
    {
        throw new NotImplementedException();
    }

    public void RemoveDeath()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        throw new NotImplementedException();
    }
}