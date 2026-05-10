using System.Collections.Generic;

namespace StudentApp
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public List<Student> GetStudents() => _repo.GetAll();

        public Student AddStudent(string name, string email, string address, int age, double grade)
        {
            var student = new Student
            {
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };
            return _repo.Add(student);
        }

        public bool EditStudent(int id, string name, string email, string address, int age, double grade)
        {
            var student = new Student
            {
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };
            return _repo.Update(id, student);
        }

        public bool RemoveStudent(int id) => _repo.Delete(id);

        public List<Student> SearchStudents(string keyword) => _repo.Search(keyword);
    }
}
