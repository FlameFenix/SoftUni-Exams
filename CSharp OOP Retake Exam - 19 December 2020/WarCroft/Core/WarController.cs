using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> party;
		private List<Item> pool;
		public WarController()
		{
			party = new List<Character>();
			pool = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			Character character = null;

			if (characterType is nameof(Warrior))
			{
				character = new Warrior(name);
				party.Add(character);
			}
			else if (characterType is nameof(Priest))
            {
				character = new Priest(name);
				party.Add(character);
            }
			else
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }
			return string.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string itemName = args[0];

			Item item = null;

			if(itemName is nameof(HealthPotion))
            {
				item = new HealthPotion();
            }
			else if(itemName is nameof(FirePotion))
            {
				item = new FirePotion();
            }
			else
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

			pool.Add(item);

			return string.Format(SuccessMessages.AddItemToPool, itemName);
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			Character character = party.FirstOrDefault(x => x.Name == characterName);

			if(character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			if(pool.Count == 0)
            {
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

			Item lastItem = pool.FirstOrDefault(x => x == pool[pool.Count - 1]);
			character.Bag.AddItem(lastItem);
			pool.Remove(lastItem);

			return string.Format(SuccessMessages.PickUpItem, characterName, lastItem.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

			Character character = party.FirstOrDefault(x => x.Name == characterName);

			if(character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			Item item = character.Bag.GetItem(itemName);

			character.UseItem(item);
			return string.Format(SuccessMessages.UsedItem, characterName, itemName);
		}

		public string GetStats()
		{
			StringBuilder sb = new StringBuilder();

            foreach (var item in party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
				sb.AppendLine(string.Format(SuccessMessages.CharacterStats, item.Name, item.Health ,item.BaseHealth, item.Armor, item.BaseArmor, item.IsAlive ? "Alive" : "Dead"));
            }

			return sb.ToString().Trim();
		}

		public string Attack(string[] args)
		{
			string atackerName = args[0];
			string recieverName = args[1];

			if(!party.Any(x => x.Name == atackerName))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, atackerName));
            }

			if(!party.Any(x => x.Name == recieverName))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, recieverName));
			}


			Character atacker = party.FirstOrDefault(x => x.Name == atackerName);
			Character reciever = party.FirstOrDefault(x => x.Name == recieverName);

			if (atacker.GetType().Name != "Warrior")
            {
				throw new ArgumentException(ExceptionMessages.AttackFail, atackerName);
            }
			Warrior warrior = (Warrior) atacker;
			warrior.Attack(reciever);
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format(SuccessMessages.AttackCharacter,
			atacker.Name, reciever.Name, atacker.AbilityPoints, reciever.Name, reciever.Health, reciever.BaseHealth, reciever.Armor, reciever.BaseArmor));

			if(!reciever.IsAlive)
            {
				sb.AppendLine($"{recieverName} is dead");
            }

			return sb.ToString().TrimEnd();
		}

		public string Heal(string[] args)
		{
			string healerName = args[0];
			string healingRecieverName = args[1];

			

			if(!party.Any(x => x.Name == healerName))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }

			if(!party.Any(x => x.Name == healingRecieverName))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingRecieverName));
			}

			Character healer = party.FirstOrDefault(x => x.Name == healerName);

			if(healer.GetType().Name != "Priest")
            {
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));

			}

			Character reciever = party.FirstOrDefault(x => x.Name == healingRecieverName);
			Priest priest = (Priest) healer;

			priest.Heal(reciever);

			return string.Format(SuccessMessages.HealCharacter, healer.Name, reciever.Name, healer.AbilityPoints, reciever.Name, reciever.Health);
		}
	}
}
