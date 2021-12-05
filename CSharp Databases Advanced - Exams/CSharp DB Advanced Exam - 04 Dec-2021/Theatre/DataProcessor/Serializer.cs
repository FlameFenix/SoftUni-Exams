namespace Theatre.DataProcessor
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres
                .ToArray()
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .Distinct();

            var dtos = new HashSet<ExportTheatresDto>();

            foreach (var theatre in theatres)
            {
                ExportTheatresDto exportDto = new ExportTheatresDto()
                {
                    Name = theatre.Name,
                    Halls = theatre.NumberOfHalls,
                    Tickets = theatre.Tickets.Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                                            .Select(x => new ExportTicketsDto()
                                            {
                                                Price = x.Price,
                                                RowNumber = x.RowNumber
                                            })
                                            .OrderByDescending(x => x.Price)
                                            .ToArray(),

                    TotalIncome = theatre.Tickets.Where(x => x.RowNumber >= 1 && x.RowNumber <= 5).Select(x => x.Price).Sum()
                };

                dtos.Add(exportDto);
            }

            dtos = dtos.OrderByDescending(x => x.Halls).ThenBy(x => x.Name).ToHashSet();

            var result = JsonConvert.SerializeObject(dtos , Formatting.Indented);

            return result;
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays.ToArray().Where(x => Convert.ToDouble(x.Rating) <= rating);

            XmlRootAttribute xmlRoot = new XmlRootAttribute("Plays");

            XmlSerializer serializer = new XmlSerializer(typeof(ExportPlaysDto[]), xmlRoot);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            StringWriter sw = new StringWriter(sb);

            ICollection<ExportPlaysDto> exportPlays = new HashSet<ExportPlaysDto>(); 

            foreach (var play in plays)
            {
                ExportPlaysDto exportDto = new ExportPlaysDto()
                {
                    Title = play.Title,
                    Duration = play.Duration.ToString("c"),
                    Genre = play.Genre.ToString(),
                    Rating = play.Rating == 0 ? "Premier" : play.Rating.ToString(),
                    Actors = play.Casts
                                 .Where(x => x.IsMainCharacter)
                                 .Select(x => new ExportActorDto()
                                 {
                                     FullName = x.FullName,
                                     MainCharacter = $"Plays main character in '{play.Title}'."
                                 })
                                 .OrderByDescending(x => x.FullName)
                                 .ToArray()
                };

                exportPlays.Add(exportDto);
            }

            exportPlays = exportPlays.OrderBy(x => x.Title).ThenByDescending(x => x.Genre).ToHashSet();

            serializer.Serialize(sw, exportPlays.ToArray(), namespaces);

            return sb.ToString().Trim();
        }
    }
}
