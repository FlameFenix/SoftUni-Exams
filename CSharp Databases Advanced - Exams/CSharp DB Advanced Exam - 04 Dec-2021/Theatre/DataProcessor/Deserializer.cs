namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Common;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Plays");

            XmlSerializer serializer = new XmlSerializer(typeof(ImportPlayDto[]), xmlRoot);

            StringReader sr = new StringReader(xmlString);

            StringBuilder sb = new StringBuilder();

            var dtos = (ImportPlayDto[]) serializer.Deserialize(sr);

            ICollection<Play> plays = new HashSet<Play>();

            foreach (var playDto in dtos)
            {
                if(!IsValid(playDto))    
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(float.Parse(playDto.Rating, CultureInfo.InvariantCulture) > GlobalConstants.RATING_MAX_RANGE ||
                    float.Parse(playDto.Rating, CultureInfo.InvariantCulture) < GlobalConstants.RATING_MIN_RANGE)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(playDto.Genre != "Drama" &&
                    playDto.Genre != "Comedy" &&
                    playDto.Genre != "Romance" &&
                    playDto.Genre != "Musical")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDurationValid = 
                    TimeSpan.TryParseExact(playDto.Duration, "c", CultureInfo.InvariantCulture, out TimeSpan duration);

                if(!isDurationValid || duration.TotalSeconds < 3600)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genre = (Genre) Enum.Parse(typeof(Genre), playDto.Genre);

                Play play = new Play()
                {
                    Title = playDto.Title,
                    Duration = duration,
                    Rating = float.Parse(playDto.Rating, CultureInfo.InvariantCulture),
                    Genre = genre,
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter,
                };

                plays.Add(play);
                sb.AppendLine(string.Format(SuccessfulImportPlay, play.Title, playDto.Genre, play.Rating));
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Casts");

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCastsDto[]), xmlRoot);

            StringReader sr = new StringReader(xmlString);

            StringBuilder sb = new StringBuilder();

            var dtos = (ImportCastsDto[])serializer.Deserialize(sr);

            ICollection<Cast> casts = new HashSet<Cast>();

            foreach (var castDto in dtos)
            {
                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Cast cast = new Cast()
                {
                    FullName = castDto.FullName,
                    IsMainCharacter = bool.Parse(castDto.IsMainCharacter),
                    PhoneNumber = castDto.PhoneNumber,
                    PlayId = int.Parse(castDto.PlayId)
                };

                casts.Add(cast);

                sb.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser"));
            }

            context.Casts.AddRange(casts);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<ImportTheathreWithTicketsDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            ICollection<Theatre> theatres = new HashSet<Theatre>();

            foreach (var theatreDto in dtos)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Theatre theatre = new Theatre()
                {
                    Name = theatreDto.Name,
                    Director = theatreDto.Director,
                    NumberOfHalls = theatreDto.NumberOfHalls
                };

                foreach (var ticketDto in theatreDto.Tickets)
                {
                    if(!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket()
                    {
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber,
                        PlayId = ticketDto.PlayId
                    };

                    theatre.Tickets.Add(ticket);
                }

                theatres.Add(theatre);
                sb.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }

            context.Theatres.AddRange(theatres);
            context.SaveChanges();

            return sb.ToString().Trim();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
