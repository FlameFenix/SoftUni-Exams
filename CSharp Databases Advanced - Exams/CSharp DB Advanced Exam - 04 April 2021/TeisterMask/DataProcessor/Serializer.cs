namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Projects");
            XmlSerializer serializer = new XmlSerializer(typeof(ExportProjectDto[]), xmlRoot);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            var dtos = context.Projects.Where(x => x.Tasks.Any()).OrderByDescending(x => x.Tasks.Count()).ThenBy(x => x.Name);

            HashSet<ExportProjectDto> projects = new HashSet<ExportProjectDto>();

            foreach (var projectDto in dtos)
            {
                ExportProjectDto dto = new ExportProjectDto()
                {
                    ProjectName = projectDto.Name,
                    HasEndDate = projectDto.DueDate == null ? "No" : "Yes",
                    Tasks = projectDto.Tasks.Select(x => new ExportTaskDto()
                    {
                        Name = x.Name,
                        Label = x.LabelType.ToString()
                    })
                    .OrderBy(x => x.Name)
                    .ToArray(),

                    TasksCount = projectDto.Tasks.Count()
                };

                projects.Add(dto);
            }
            serializer.Serialize(sw, projects.ToArray(), namespaces);

            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .ToArray()
                .Select(x => new ExportEmployeeDto()
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks.Where(x => x.Task.OpenDate >= date).Select(x => new ExportEmployeeWithTasksDto()
                    {
                        TaskName = x.Task.Name,
                        OpenDate = x.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = x.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = x.Task.LabelType.ToString(),
                        ExecutionType = x.Task.ExecutionType.ToString(),
                    })
                    .OrderByDescending(x => DateTime.Parse(x.DueDate, CultureInfo.InvariantCulture))
                    .ThenBy(x => x.TaskName)
                    .ToArray()
                })
                .OrderByDescending(x => x.Tasks.Count())
                .ThenBy(x => x.Username)
                .Take(10);

            var dtos = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return dtos;
        }
    }
}