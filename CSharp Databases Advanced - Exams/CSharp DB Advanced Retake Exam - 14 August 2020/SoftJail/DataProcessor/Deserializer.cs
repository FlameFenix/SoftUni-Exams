namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Common;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<ImportDepartmentsDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            HashSet <Department> departments = new HashSet<Department>();

            foreach (var DepartmentDto in dtos)
            {
                bool isDtoValid = IsValid(DepartmentDto);

                if (!isDtoValid || DepartmentDto.Cells.Length == 0)
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                Department department = new Department()
                {
                    Name = DepartmentDto.Name
                };

                bool isCellsValid = true;

                foreach (var cellDto in DepartmentDto.Cells)
                {
                    bool isCellDtoValid = IsValid(cellDto);

                    if (!isCellDtoValid ||
                        int.Parse(cellDto.CellNumber) < GlobalConstants.CELL_RANGE_MIN ||
                        int.Parse(cellDto.CellNumber) > GlobalConstants.CELL_RANGE_MAX)
                    {
                        isCellsValid = false;
                        break;
                    }
                }

                if (!isCellsValid)
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                foreach (var cellDto in DepartmentDto.Cells)
                {

                    Cell cell = new Cell()
                    {
                        CellNumber = int.Parse(cellDto.CellNumber),
                        HasWindow = cellDto.HasWindow
                    };

                    department.Cells.Add(cell);
                }

                departments.Add(department);
                sb.AppendLine(string.Format(GlobalConstants.DEPARTMENT_SUCCESS_MESSAGE, department.Name, department.Cells.Count));
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            ICollection<Prisoner> prisoners = new HashSet<Prisoner>();

            foreach (var prisonerDto in dtos)
            {
                bool isDtoValid = IsValid(prisonerDto);

                bool isIncarcerationDateValid = 
                    DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy",CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime incarcerationDate);

                if (!isDtoValid || 
                    int.Parse(prisonerDto.Age) < GlobalConstants.PRISONER_AGE_RANGE_MIN ||
                    int.Parse(prisonerDto.Age) > GlobalConstants.PRISONER_AGE_RANGE_MAX || 
                    !isIncarcerationDateValid ||
                    prisonerDto.Mails.Count() == 0)
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                Prisoner prisoner = new Prisoner()
                {
                    FullName = prisonerDto.FullName,
                    Age = int.Parse(prisonerDto.Age),               
                    Nickname = prisonerDto.Nickname,
                    IncarcerationDate = incarcerationDate,
                    
                };

                if(!string.IsNullOrWhiteSpace(prisonerDto.CellId))
                {
                    prisoner.CellId = int.Parse(prisonerDto.CellId);
                }
                
                if(!string.IsNullOrWhiteSpace(prisonerDto.Bail))
                {
                    prisoner.Bail = decimal.Parse(prisonerDto.Bail, CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrWhiteSpace(prisonerDto.ReleaseDate))
                {
                    bool isDateValid =
                        DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
                    if (isDateValid)
                    {
                        prisoner.ReleaseDate = releaseDate;
                    }   
                }
                bool isMailDtosValid = true;

                foreach (var mail in prisonerDto.Mails)
                {
                    bool isMailDtoValid = IsValid(mail);

                    if (!isMailDtoValid)
                    {
                        break;
                    }
                }

                if (!isMailDtosValid)
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                foreach (var mailDto in prisonerDto.Mails)
                {
                    Mail mail = new Mail()
                    {
                        Description = mailDto.Description,
                        Address = mailDto.Address,
                        Sender = mailDto.Sender
                    };

                    prisoner.Mails.Add(mail);
                }

                prisoners.Add(prisoner);
                sb.AppendLine(string.Format(GlobalConstants.PRISONER_SUCCESS_MESSAGE, prisoner.FullName, prisoner.Age));
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();
            
            return sb.ToString().Trim();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Officers");

            XmlSerializer serializer = new XmlSerializer(typeof(ImportOfficersDto[]), xmlRoot);

            StringBuilder sb = new StringBuilder();

            StringReader sr = new StringReader(xmlString);

            var dtos = (ImportOfficersDto[]) serializer.Deserialize(sr);

            var officers = new HashSet<Officer>(); 

            foreach (var officerDto in dtos)
            {
                if(!IsValid(officerDto) || 
                    decimal.Parse(officerDto.Money, CultureInfo.InvariantCulture) < 0)
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                if(officerDto.Position != "Guard" &&
                    officerDto.Position !=  "Watcher" &&
                    officerDto.Position !=  "Labour" && 
                    officerDto.Position != "Overseer")
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                if(officerDto.Weapon != "Knife" &&
                    officerDto.Weapon != "FlashPulse" &&
                    officerDto.Weapon != "ChainRifle" &&
                    officerDto.Weapon != "Pistol" &&
                    officerDto.Weapon != "Sniper")
                {
                    sb.AppendLine(GlobalConstants.ERROR_MESSAGE);
                    continue;
                }

                Position position = (Position)Enum.Parse(typeof(Position), officerDto.Position);

                Weapon weapon = (Weapon)Enum.Parse(typeof(Weapon), officerDto.Weapon);

                Officer officer = new Officer()
                {
                    FullName = officerDto.Name,
                    Salary = decimal.Parse(officerDto.Money, CultureInfo.InvariantCulture),
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = int.Parse(officerDto.DepartmentId)
                };

                foreach (var prisonerDto in officerDto.PrisonersId)
                {
                    Prisoner prisoner = context.Prisoners.FirstOrDefault(x => x.Id == int.Parse(prisonerDto.Id));

                    OfficerPrisoner officerPrisoner = new OfficerPrisoner()
                    {
                        Officer = officer,
                        Prisoner = prisoner
                    };

                    officer.OfficerPrisoners.Add(officerPrisoner);
                }

                officers.Add(officer);

                sb.AppendLine(string.Format(GlobalConstants.OFFICERS_SUCCESS_MESSAGE, officer.FullName, officer.OfficerPrisoners.Count));
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}