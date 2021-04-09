using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if(attackPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if(enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if(attackPlayer.GetType().Name is nameof(Beginner))
            {
                attackPlayer.Health += 40;

                foreach (var item in attackPlayer.CardRepository.Cards)
                {
                    item.DamagePoints += 30;
                }
            }

            if (enemyPlayer.GetType().Name is nameof(Beginner))
            {
                enemyPlayer.Health += 40;

                foreach (var item in enemyPlayer.CardRepository.Cards)
                {
                    item.DamagePoints += 30;
                }
            }

            int attackCounst = 0;

            attackPlayer.Health = attackPlayer.Health + attackPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);
            enemyPlayer.Health = enemyPlayer.Health + enemyPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);

            while (!attackPlayer.IsDead || !enemyPlayer.IsDead)
            {
                if(enemyPlayer.IsDead)
                {
                    break;
                }
                if(attackPlayer.IsDead)
                {
                    break;
                }

                int count = 0;
                ICard attackerCard = null;
                foreach (var item in attackPlayer.CardRepository.Cards)
                {
                    count++;
                    if (count == 1)
                    {
                        attackerCard = item;
                        break;
                    }
                }
                ICard enemyCard = null;
                count = 0;
                foreach (var item in enemyPlayer.CardRepository.Cards)
                {
                    count++;
                    if (count == 1)
                    {
                        
                        enemyCard = item;
                        break;
                    }
                }

                
                while (!attackPlayer.IsDead || !enemyPlayer.IsDead)
                {
                    if(attackPlayer.IsDead)
                    {
                        break;
                    }
                    if(enemyPlayer.IsDead)
                    {
                        break;
                    }
                    if (attackCounst % 2 == 0)
                    {
                        // attackPlayer attack
                        enemyPlayer.TakeDamage(attackPlayer.CardRepository.Cards.Sum(x => x.DamagePoints));
                    }
                    else
                    {
                        // enemyPlayer Attack
                        attackPlayer.TakeDamage(enemyPlayer.CardRepository.Cards.Sum(x => x.DamagePoints));
                    }

                    attackCounst++;

                }
                

                
            }

        }
    }
}
