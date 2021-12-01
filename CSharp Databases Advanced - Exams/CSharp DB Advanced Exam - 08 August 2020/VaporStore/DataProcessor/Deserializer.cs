namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Common;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var dtos = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

			var sb = new StringBuilder();

			var games = new HashSet<Game>();

			var developers = new HashSet<Developer>();

			var genres = new HashSet<Genre>();

			var tags = new HashSet<Tag>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
                }

				if (string.IsNullOrWhiteSpace(dto.Name) ||
					string.IsNullOrWhiteSpace(dto.Price) ||
					string.IsNullOrWhiteSpace(dto.ReleaseDate) ||
					string.IsNullOrWhiteSpace(dto.Genre) ||
					string.IsNullOrWhiteSpace(dto.Developer) ||
					dto.Tags.Length == 0)
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
                }

				if(decimal.Parse(dto.Price, CultureInfo.InvariantCulture) < decimal.Zero)
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
				}

				bool isDateValid = 
					DateTime.TryParseExact(dto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime realeaseDate);

                if (!isDateValid)
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
                }

				Developer developer = developers.FirstOrDefault(x => x.Name == dto.Developer);

				if(developer == null)
                {
					developer = new Developer()
					{
						Name = dto.Developer
					};

					developers.Add(developer);
                }

				Genre genre = genres.FirstOrDefault(x => x.Name == dto.Genre);

				if(genre == null)
                {
					genre = new Genre()
					{
						Name = dto.Genre
					};
					genres.Add(genre);
                }

				Game game = new Game()
				{
					Name = dto.Name,
					Price = decimal.Parse(dto.Price, CultureInfo.InvariantCulture),
					ReleaseDate = realeaseDate,
					Developer = developer,
					Genre = genre,
				};

				ICollection<GameTag> currentTags = new HashSet<GameTag>();

                foreach (var tagDto in dto.Tags)
                {
					Tag tag = tags.FirstOrDefault(x => x.Name == tagDto);

					if (tag == null) 
					{
						tag = new Tag()
						{
							Name = tagDto
						};

						tags.Add(tag);
					}

					GameTag gameTag = new GameTag()
					{
						Game = game,
						Tag = tag
					};

					currentTags.Add(gameTag);
                }

				game.GameTags = currentTags.ToArray();
				games.Add(game);
				sb.AppendLine(string.Format(GlobalConstants.SuccessfullyAddedGames, game.Name, game.Genre.Name, game.GameTags.Count));
			}

			context.Tags.AddRange(tags);
			context.Genres.AddRange(genres);
			context.Developers.AddRange(developers);
			context.Games.AddRange(games);
			context.SaveChanges();

			return sb.ToString().Trim();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var usersDto = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);

			var users = new HashSet<User>();

			var cards = new HashSet<Card>();

			var sb = new StringBuilder();

            foreach (var userDto in usersDto)
            {
				bool isDtoValid = IsValid(userDto);

                if (!isDtoValid || 
					string.IsNullOrWhiteSpace(userDto.FullName) ||
					string.IsNullOrWhiteSpace(userDto.Email) ||
					int.Parse(userDto.Age) < 3 || 
					int.Parse(userDto.Age) > 103 ||
					userDto.Cards.Length == 0)
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
                }

				User user = new User()
				{
					FullName = userDto.FullName,
					Username = userDto.Username,
					Email = userDto.Email,
					Age = int.Parse(userDto.Age),
				};

				bool isCardsValid = true;

				var usersCards = new HashSet<Card>();

                foreach (var cardDto in userDto.Cards)
                {
					bool isCardDtoValid = IsValid(cardDto);

					if (!isCardDtoValid) 
					{
						isCardDtoValid = false;
						break;
					}

					if(cardDto.Type != "Debit" && cardDto.Type != "Credit")
                    {
						isCardDtoValid = false;
						break;
                    }

					Card card = cards.FirstOrDefault(x => x.Number == cardDto.Number);

					if(card == null)
                    {
						card = new Card()
						{
							Number = cardDto.Number,
							Cvc = cardDto.CVC,
							Type = (CardType) Enum.Parse(typeof(CardType), cardDto.Type, true)
						};

						cards.Add(card);
					}

					usersCards.Add(card);
                }

                if (!isCardsValid)
                {
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
                }

				user.Cards = usersCards.ToArray();

				users.Add(user);

				sb.AppendLine(string.Format(GlobalConstants.SuccessfullyAddedUsersWithCards, user.Username, user.Cards.Count));
			}

			context.Cards.AddRange(cards);
			context.Users.AddRange(users);
			context.SaveChanges();

			return sb.ToString().Trim();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			XmlRootAttribute xmlRoot = new XmlRootAttribute("Purchases");

			XmlSerializer serializer = new XmlSerializer(typeof(ImportPurchaseDto[]), xmlRoot);

			StringReader reader = new StringReader(xmlString);

			StringBuilder sb = new StringBuilder();

			var dtos = (ImportPurchaseDto[]) serializer.Deserialize(reader);

			var purchases = new HashSet<Purchase>();

            foreach (var purchaseDto in dtos)
            {
				bool isDateValid = 
					DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
				bool isDtoValid = IsValid(purchaseDto);

				Card card = context.Cards.FirstOrDefault(x => x.Number == purchaseDto.Card);

				Game game = context.Games.FirstOrDefault(x => x.Name == purchaseDto.Title);

				if (purchaseDto.Type != "Retail" && purchaseDto.Type != "Digital" || 
					!isDateValid ||
					!isDtoValid ||
				    game == null ||
					card == null)
				{
					sb.AppendLine(GlobalConstants.ErrorMessage);
					continue;
				}

				Purchase purchase = new Purchase()
				{
					ProductKey = purchaseDto.Key,
					Game = game,
					Card = card,
					Date = date,
					Type = (PurchaseType)Enum.Parse(typeof(PurchaseType), purchaseDto.Type, true)
				};

				purchases.Add(purchase);
				sb.AppendLine(string.Format(GlobalConstants.SuccessfullyAddedPurchases, purchase.Game.Name, purchase.Card.User.Username));

			}

			context.Purchases.AddRange(purchases);
			context.SaveChanges();

			return sb.ToString().Trim();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}