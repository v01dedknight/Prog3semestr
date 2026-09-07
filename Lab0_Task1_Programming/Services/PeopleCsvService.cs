using System.Text;
using Lab0_Task1_Programming.Models;

namespace Lab0_Task1_Programming.Services
{
    public static class PeopleCsvService
    {
        private static readonly (string LastName, string FirstName, string Gender)[] PeopleTemplates =
        {
            ("Иванов", "Иван", "М"),
            ("Петров", "Алексей", "М"),
            ("Сидоров", "Максим", "М"),
            ("Смирнов", "Дмитрий", "М"),
            ("Кузнецов", "Артём", "М"),
            ("Орлов", "Никита", "М"),
            ("Соколова", "Анна", "Ж"),
            ("Попова", "Мария", "Ж"),
            ("Волкова", "Елена", "Ж"),
            ("Орлова", "Дарья", "Ж"),
            ("Морозова", "Ольга", "Ж"),
            ("Лебедева", "София", "Ж")
        };

        public static List<Person> GeneratePeople()
        {
            var people = new List<Person>();

            foreach (var template in PeopleTemplates)
            {
                int height = template.Gender == "М"
                    ? Random.Shared.Next(165, 196)
                    : Random.Shared.Next(155, 186);

                people.Add(new Person
                {
                    LastName = template.LastName,
                    FirstName = template.FirstName,
                    Gender = template.Gender,
                    Height = height
                });
            }

            return people;
        }

        public static void Save(string filePath, IEnumerable<Person> people)
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var writer = new StreamWriter(
                filePath,
                false,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

            writer.WriteLine("Фамилия;Имя;Пол;Рост");

            foreach (Person person in people)
            {
                writer.WriteLine(
                    $"{person.LastName};{person.FirstName};{person.Gender};{person.Height}");
            }
        }

        public static List<Person> Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл с данными не найден.", filePath);
            }

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            var people = new List<Person>();

            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex].Trim();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(';');

                if (parts.Length != 4)
                {
                    throw new FormatException(
                        $"Некорректный формат строки {lineIndex + 1} в CSV-файле.");
                }

                string lastName = parts[0].Trim();
                string firstName = parts[1].Trim();
                string gender = parts[2].Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(firstName))
                {
                    throw new FormatException(
                        $"В строке {lineIndex + 1} не заполнены фамилия или имя.");
                }

                if (gender != "М" && gender != "Ж")
                {
                    throw new FormatException(
                        $"В строке {lineIndex + 1} пол должен быть указан как М или Ж.");
                }

                if (!int.TryParse(parts[3].Trim(), out int height) ||
                    height <= 0 || height > 300)
                {
                    throw new FormatException(
                        $"В строке {lineIndex + 1} указан некорректный рост.");
                }

                people.Add(new Person
                {
                    LastName = lastName,
                    FirstName = firstName,
                    Gender = gender,
                    Height = height
                });
            }

            return people;
        }
    }
}
