using System;

namespace StudentApp
{
    public class StudentUI
    {
        private readonly StudentService studentService = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowStudents();
                ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        EditStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        SearchStudent();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ShowStudents()
        {
            var students = studentService.GetStudents();
            Console.WriteLine("=== DANH SÁCH SINH VIÊN ===");
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }
            if (students.Count == 0)
                Console.WriteLine("Chưa có sinh viên nào.");
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nChức năng:");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Sửa sinh viên");
            Console.WriteLine("3. Xoá sinh viên");
            Console.WriteLine("4. Tìm kiếm sinh viên");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn: ");
        }

        private void AddStudent()
        {
            Console.Write("Nhập tên: ");
            string name = Console.ReadLine();
            Console.Write("Nhập email: ");
            string email = Console.ReadLine();
            Console.Write("Nhập địa chỉ: ");
            string address = Console.ReadLine();
            Console.Write("Nhập tuổi: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Nhập điểm: ");
            double.TryParse(Console.ReadLine(), out double grade);

            if (!string.IsNullOrWhiteSpace(name))
                studentService.AddStudent(name, email, address, age, grade);
        }

        private void EditStudent()
        {
            Console.Write("Nhập ID sinh viên cần sửa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Nhập tên mới: ");
                string name = Console.ReadLine();
                Console.Write("Nhập email mới: ");
                string email = Console.ReadLine();
                Console.Write("Nhập địa chỉ mới: ");
                string address = Console.ReadLine();
                Console.Write("Nhập tuổi mới: ");
                int.TryParse(Console.ReadLine(), out int age);
                Console.Write("Nhập điểm mới: ");
                double.TryParse(Console.ReadLine(), out double grade);

                studentService.EditStudent(id, name, email, address, age, grade);
            }
        }

        private void DeleteStudent()
        {
            Console.Write("Nhập ID sinh viên cần xoá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                studentService.RemoveStudent(id);
        }

        private void SearchStudent()
        {
            Console.Write("Nhập từ khoá (Id, Tên, Địa chỉ, Điểm): ");
            string keyword = Console.ReadLine();
            var results = studentService.SearchStudents(keyword);
            Console.WriteLine("=== KẾT QUẢ TÌM KIẾM ===");
            foreach (var s in results)
            {
                Console.WriteLine(s);
            }
            if (results.Count == 0)
                Console.WriteLine("Không tìm thấy sinh viên nào.");
        }
    }
}
