using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentApp
{
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly string filePath = "students.txt";

        public StudentRepository()
        {
            LoadFromFile();
            if (_students.Count > 0)
                _nextId = _students.Max(s => s.Id) + 1;
        }

        public List<Student> GetAll() => new(_students);

        public Student Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
            SaveToFile();
            return student;
        }

        public bool Update(int id, Student newStudent)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;
            student.Name = newStudent.Name;
            student.Email = newStudent.Email;
            student.Address = newStudent.Address;
            student.Age = newStudent.Age;
            student.Grade = newStudent.Grade;
            SaveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;
            _students.Remove(student);
            SaveToFile();
            return true;
        }

        public List<Student> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _students.Where(s =>
                s.Id.ToString() == keyword ||
                s.Name.ToLower().Contains(keyword) ||
                s.Address.ToLower().Contains(keyword) ||
                s.Grade.ToString() == keyword
            ).ToList();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(filePath)) return;
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line))
                    _students.Add(Student.FromFileString(line));
            }
        }

        private void SaveToFile()
        {
            File.WriteAllLines(filePath, _students.Select(s => s.ToFileString()));
        }
    }
}
