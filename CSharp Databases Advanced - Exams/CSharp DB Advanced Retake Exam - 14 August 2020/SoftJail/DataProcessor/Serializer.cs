namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = new HashSet<ExportPrisonersDto>();

            foreach (var prisonerId in ids)
            {
                Prisoner prisoner = context
                    .Prisoners
                    .FirstOrDefault(x => x.Id == prisonerId);

                ExportPrisonersDto exportPrisonerDto = new ExportPrisonersDto()
                {
                    Id = prisoner.Id,
                    CellNumber = prisoner.Cell.CellNumber,
                    Name = prisoner.FullName,
                    Officers = prisoner.PrisonerOfficers.Select(x => new ExportOfficersDto()
                    {
                        OfficerName = x.Officer.FullName,
                        Department = x.Officer.Department.Name
                    })
                    .OrderBy(x => x.OfficerName)
                    .ToArray(),

                    TotalOfficerSalary = prisoner.PrisonerOfficers.Select(x => x.Officer.Salary).Sum()
                };

                prisoners.Add(exportPrisonerDto);

            }

            var result = JsonConvert.SerializeObject(prisoners.OrderBy(x => x.Name).ThenBy(x => x.Id), Formatting.Indented);

            return result;

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var xmlRoot = new XmlRootAttribute("Prisoners");
            var serializer = new XmlSerializer(typeof(ExportPrisonerByNamesDto[]), xmlRoot);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var prisonerNames = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                              .ToArray();

            var exportPrisonersDto = new HashSet<ExportPrisonerByNamesDto>();

            foreach (var prisonerName in prisonerNames)
            {
                Prisoner prisoner = context.Prisoners.FirstOrDefault(x => x.FullName == prisonerName);

                ExportPrisonerByNamesDto exportPrisonerDto = new ExportPrisonerByNamesDto()
                {
                    Name = prisoner.FullName,
                    Id = prisoner.Id.ToString(),
                    IncarcerationDate = prisoner.IncarcerationDate.ToString("yyyy-MM-dd"),
                    Messages = prisoner.Mails.Select(x => new ExportMessagesDto()
                    {
                        Description = string.Join("", x.Description.Reverse())
                    }).ToArray()
                };

                exportPrisonersDto.Add(exportPrisonerDto);
            }

            serializer.Serialize(sw, exportPrisonersDto.OrderBy(x => x.Name).ThenBy(x => int.Parse(x.Id)).ToArray(), namespaces);

            return sb.ToString().Trim();
        }
    }
}