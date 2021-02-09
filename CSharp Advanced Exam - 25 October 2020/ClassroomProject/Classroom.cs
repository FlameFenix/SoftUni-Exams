using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        private List<Student> addedStudents = new List<Student>();

        public Classroom(int capacity)
        {
            students = new List<Student>();
            Capacity = capacity;
        }

        public List<Student> students { get; set; }

        public int Capacity { get; set; }

        public int Count 
        { 
            get 
            { 
                return students.Count ; 
            } 
        }

        public string RegisterStudent(Student student)
        {
            string output = string.Empty;

            if(Count < Capacity)
            {
                students.Add(student);
                output = $"Added student {student.FirstName} {student.LastName}";
            }
            else
            {
                output = "No seats in the classroom"; 
            }

            return output; 
        }

        public string DismissStudent(string firstName, string lastName)
        {
            Student student = students.Find(x => x.FirstName == firstName && x.LastName == lastName);
            string output = string.Empty;
            if(students.Contains(student))
            {
                students.Remove(student);
                output = $"Dismissed student {student.FirstName} {student.LastName}";
            }
            else
            {
                output = "Student not found";
            }

            return output;
        }

        public string GetSubjectInfo(string subject)
        {
            Student student = students.Find(x => x.Subject == subject);

            string output = string.Empty;

            if(students.Contains(student))
            {
                output += $"Subject: {subject}" + Environment.NewLine;
                output += "Students:" + Environment.NewLine;
                foreach (var item in students.Where(x => x.Subject == subject))
                {
                    output += $"{item.FirstName} {item.LastName}" + Environment.NewLine;
                }
            }
            else
            {
                output = "No students enrolled for the subject";
            }

            return output.Trim();
        }

        public int GetStudentsCount()
        {
            return Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            Student student = students.Find(x => x.FirstName == firstName && x.LastName == lastName);

            return student;
        }
    }
}
