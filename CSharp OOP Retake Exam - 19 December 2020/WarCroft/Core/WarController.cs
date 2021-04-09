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
		private List<Character> characters;
		private List<Item> itemsPool;
		public WarController()
		{
			characters = new List<Character>();
			itemsPool = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			Character character = null;

			if (characterType is nameof(Warrior))
			{
				character = new Warrior(name);
				characters.Add(character);
			}
			else if (characterType is nameof(Priest))
            {
				character = new Priest(name);
				characters.Add(character);
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

			itemsPool.Add(item);

			return string.Format(SuccessMessages.AddItemToPool, itemName);
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			Character character = characters.FirstOrDefault(x => x.Name == characterName);

			if(character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			if(itemsPool.Count == 0)
            {
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

			Item lastItem = itemsPool.FirstOrDefault(x => x == itemsPool[itemsPool.Count - 1]);
			character.Bag.AddItem(lastItem);
			itemsPool.Remove(lastItem);

			return string.Format(SuccessMessages.PickUpItem, characterName, lastItem.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

			Character character = characters.FirstOrDefault(x => x.Name == characterName);

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

            foreach (var item in characters.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
				sb.AppendLine(string.Format(SuccessMessages.CharacterStats, item.Name, item.Health ,item.BaseHealth, item.Armor, item.BaseArmor, item.IsAlive ? "Alive" : "Dead"));
            }

			return sb.ToString().Trim();
		}

		public string Attack(string[] args)
		{
			string atackerName = args[0];
			string recieverName = args[1];

			Warrior atacker = (Warrior) characters.FirstOrDefault(x => x.Name == atackerName);

			if(atacker == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, atackerName));
            }

			Character reciever = characters.FirstOrDefault(x => x.Name == recieverName);

			if(reciever == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, recieverName));
			}

			if (!atacker.IsAlive || atacker.AbilityPoints <= 0)
            {
				throw new ArgumentException(ExceptionMessages.AttackFail, atackerName);
            }

			atacker.Attack(reciever);
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

			Priest healer = (Priest)characters.FirstOrDefault(x => x.Name == healerName);

			if(healer == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }

			Character reciever = characters.FirstOrDefault(x => x.Name == healingRecieverName);

			if(reciever == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingRecieverName));
			}

			if(!healer.IsAlive || healer.AbilityPoints <= 0)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal), healerName);
            }

			healer.Heal(reciever);
			return string.Format(SuccessMessages.HealCharacter, healer.Name, reciever.Name, healer.AbilityPoints, reciever.Name, reciever.Health);
		}
	}
}
