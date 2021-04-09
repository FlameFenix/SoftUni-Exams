namespace PlayersAndMonsters.Core
{
    using System;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Players;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories;
    using PlayersAndMonsters.Repositories.Contracts;

    public class ManagerController : IManagerController
    {
        private IPlayerRepository players;
        private ICardRepository cards;
        private IBattleField battlefield;

        public ManagerController()
        {
            players = new PlayerRepository();
            cards = new CardRepository();
            battlefield = new BattleField();
        }

        public string AddPlayer(string type, string username)
        {
            IPlayer player = null;

            if(type is nameof(Beginner))
            {
                player = new Beginner(cards, username);
            }
            else if(type is nameof(Advanced))
            {
                player = new Advanced(cards, username);
            }

            players.Add(player);
            return $"Successfully added player of type {type} with username: {username}";
        }

        public string AddCard(string type, string name)
        {
            ICard card = null;
            if(type == "Magic")
            {
                card = new MagicCard(name);
            }
            else if(type == "Trap")
            {
                card = new TrapCard(name);
            }

            cards.Add(card);
            return $"Successfully added card of type {type}Card with name: {name}";
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer player = players.Find(username);
            ICard card = cards.Find(cardName);
            player.CardRepository.Add(card);
            return $"Successfully added card: {cardName} to user: {username}";
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attacker = players.Find(attackUser);
            IPlayer victim = players.Find(enemyUser);
            battlefield.Fight(attacker, victim);
            return $"Attack user health {attacker.Health} - Enemy user health {victim.Health}";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in players.Players)
            {
                sb.AppendLine($"Username: {player.Username} - Health: {player.Health} – Cards {player.CardRepository.Cards.Count}");
                foreach (var item in player.CardRepository.Cards)
                {
                    sb.AppendLine($"Card: {item.Name} - Damage: {item.DamagePoints}");
                }
                sb.AppendLine("###");
            }

            return sb.ToString().Trim();
        }
    }
}
