namespace TeisterMask.DataProcessor
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
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using TeisterMask.ImportDtos;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Projects");

            XmlSerializer serilizer = new XmlSerializer(typeof(ProjectDto[]), xmlRoot);

            StringReader reader = new StringReader(xmlString);

            StringBuilder sb = new StringBuilder();

            ProjectDto[] dtos = (ProjectDto[])serilizer.Deserialize(reader);

            HashSet<Project> projects = new HashSet<Project>();

            foreach (ProjectDto dto in dtos)
            {
                bool isDtoValid = IsValid(dto);

                if (!isDtoValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isOpenDateValid = DateTime.TryParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDateValue);

                if(!isOpenDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDueDateValid = DateTime.TryParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDateValue);

                DateTime? dueDateNullable = null;

                Project project = new Project()
                {
                    Name = dto.Name,
                    OpenDate = openDateValue,
                    DueDate = isDueDateValid ? dueDateValue : dueDateNullable
                };

                HashSet<Task> tasks = new HashSet<Task>();

                foreach (var taskDto in dto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskOpenDateValue);

                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskDueDateValue);

                    if(!isTaskOpenDateValid || !isTaskDueDateValid || taskOpenDateValue < openDateValue || taskDueDateValue > dueDateValue && dueDateValue != default)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task task = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDateValue,
                        DueDate = taskDueDateValue,
                        ExecutionType = (ExecutionType) taskDto.ExecutionType,
                        LabelType = (LabelType) taskDto.LabelType,

                    };

                    tasks.Add(task);
                }

                project.Tasks = tasks;

                projects.Add(project);

                sb.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            HashSet<Employee> employees = new HashSet<Employee>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee()
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Phone = dto.Phone,
                };

                HashSet<EmployeeTask> employeeTasks = new HashSet<EmployeeTask>();

                foreach (var taskId in dto.Tasks.Distinct())
                {
                    Task task = context.Tasks.FirstOrDefault(x => x.Id == taskId);

                    if(task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    EmployeeTask employeeTask = new EmployeeTask()
                    {
                        Employee = employee,
                        Task = task
                    };

                    employeeTasks.Add(employeeTask);
                }

                employee.EmployeesTasks = employeeTasks;
                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employeeTasks.Count));
            }

            context.Employees.AddRange(employees);
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