using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await _studentRepository.GetAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await _studentRepository.UpdateAsync(id, student);
        }

        public async Task DeleteStudentAsync(string id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task<List<Student>> SearchStudentsAsync(string query)
        {
            return await _studentRepository.SearchAsync(query);
        }
    }
}
