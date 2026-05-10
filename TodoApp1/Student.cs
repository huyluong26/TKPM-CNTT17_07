namespace StudentApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Email} | {Address} | {Age} | {Grade}";
        }

        public string ToFileString()
        {
            return $"{Id}|{Name}|{Email}|{Address}|{Age}|{Grade}";
        }

        public static Student FromFileString(string line)
        {
            var parts = line.Split('|');
            return new Student
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Email = parts[2],
                Address = parts[3],
                Age = int.Parse(parts[4]),
                Grade = double.Parse(parts[5])
            };
        }
    }
}
