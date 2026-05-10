using System;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public class StudentUI
    {
        private readonly IStudentService _studentService;

        public StudentUI(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task StartAsync()
        {
            // Để hiển thị tiếng Việt trên Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("      QUẢN LÝ SINH VIÊN      ");
                Console.WriteLine("=============================");
                Console.WriteLine("1. Hiển thị tất cả sinh viên");
                Console.WriteLine("2. Thêm sinh viên mới");
                Console.WriteLine("3. Cập nhật thông tin sinh viên");
                Console.WriteLine("4. Xoá sinh viên");
                Console.WriteLine("5. Tìm kiếm sinh viên");
                Console.WriteLine("6. Thoát");
                Console.WriteLine("=============================");
                Console.Write("Chọn một chức năng: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowAllStudentsAsync();
                        break;
                    case "2":
                        await AddStudentAsync();
                        break;
                    case "3":
                        await UpdateStudentAsync();
                        break;
                    case "4":
                        await DeleteStudentAsync();
                        break;
                    case "5":
                        await SearchStudentsAsync();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Nhấn phím bất kỳ để thử lại...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ShowAllStudentsAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Danh sách Sinh viên ---");
            var students = await _studentService.GetAllStudentsAsync();

            if (students.Count == 0)
            {
                Console.WriteLine("Không tìm thấy sinh viên nào.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }
            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        private async Task AddStudentAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Thêm Sinh viên mới ---");
            
            var student = new Student();
            
            Console.Write("ID Sinh viên (nhập tùy ý hoặc để trống để tạo tự động): ");
            var inputId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputId))
            {
                student.Id = inputId;
            }
            else
            {
                student.Id = Guid.NewGuid().ToString("N").Substring(0, 8); // Tạo một ID ngẫu nhiên ngắn nếu để trống
            }
            
            Console.Write("Họ tên: ");
            student.Name = Console.ReadLine() ?? "";
            
            Console.Write("Email: ");
            student.Email = Console.ReadLine() ?? "";
            
            Console.Write("Địa chỉ: ");
            student.Address = Console.ReadLine() ?? "";
            
            Console.Write("Tuổi: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                student.Age = age;
            }
            
            Console.Write("Xếp loại (Grade): ");
            student.Grade = Console.ReadLine() ?? "";

            await _studentService.AddStudentAsync(student);
            Console.WriteLine("Thêm sinh viên thành công! Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        private async Task UpdateStudentAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Cập nhật Sinh viên ---");
            Console.Write("Nhập ID của Sinh viên cần cập nhật: ");
            var id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id)) return;

            var existingStudent = await _studentService.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên! Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Đang cập nhật Sinh viên: {existingStudent.Name}");
            Console.WriteLine("Để trống nếu bạn muốn giữ nguyên giá trị hiện tại.");

            Console.Write($"Họ tên ({existingStudent.Name}): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) existingStudent.Name = name;

            Console.Write($"Email ({existingStudent.Email}): ");
            var email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email)) existingStudent.Email = email;

            Console.Write($"Địa chỉ ({existingStudent.Address}): ");
            var address = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(address)) existingStudent.Address = address;

            Console.Write($"Tuổi ({existingStudent.Age}): ");
            var ageStr = Console.ReadLine();
            if (int.TryParse(ageStr, out int age)) existingStudent.Age = age;

            Console.Write($"Xếp loại ({existingStudent.Grade}): ");
            var grade = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(grade)) existingStudent.Grade = grade;

            await _studentService.UpdateStudentAsync(id, existingStudent);
            Console.WriteLine("Cập nhật sinh viên thành công! Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        private async Task DeleteStudentAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Xoá Sinh viên ---");
            Console.Write("Nhập ID của Sinh viên cần xoá: ");
            var id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id)) return;

            var existingStudent = await _studentService.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên! Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey();
                return;
            }

            await _studentService.DeleteStudentAsync(id);
            Console.WriteLine("Xoá sinh viên thành công! Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        private async Task SearchStudentsAsync()
        {
            Console.Clear();
            Console.WriteLine("--- Tìm kiếm Sinh viên ---");
            Console.Write("Nhập từ khóa tìm kiếm (Id, Họ tên, Địa chỉ, Xếp loại): ");
            var query = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(query)) return;

            var students = await _studentService.SearchStudentsAsync(query);

            if (students.Count == 0)
            {
                Console.WriteLine("Không có sinh viên nào khớp với từ khóa tìm kiếm.");
            }
            else
            {
                Console.WriteLine($"Tìm thấy {students.Count} sinh viên:");
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }
            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
    }
}
