using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(string id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(string id, Student student);
        Task DeleteStudentAsync(string id);
        Task<List<Student>> SearchStudentsAsync(string query);
    }
}
