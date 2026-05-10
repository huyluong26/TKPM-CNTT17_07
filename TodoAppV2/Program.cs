using System;
using System.Threading.Tasks;

namespace TodoAppV2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "StudentManagementDB";

            var studentRepository = new StudentRepository(connectionString, databaseName);
            IStudentService studentService = new StudentService(studentRepository);
            var studentUI = new StudentUI(studentService);

            await studentUI.StartAsync();
        }
    }
}