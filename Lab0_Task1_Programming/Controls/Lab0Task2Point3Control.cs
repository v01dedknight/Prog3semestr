namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab0Task2Point3Control : UserControl
    {
        private readonly string _filePath =
            Path.Combine(AppContext.BaseDirectory, "people.csv");

        public Lab0Task2Point3Control()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object? sender, EventArgs e)
        {
            try
            {
                List<Person> generatedPeople = GeneratePeople();
                SaveToCsv(generatedPeople);

                List<Person> people = ReadFromCsv();

                List<Person> men = people.Where(p => p.Gender == "М").ToList();
                List<Person> women = people.Where(p => p.Gender == "Ж").ToList();

                double averageMenHeight = men.Average(p => p.Height);
                double averageWomenHeight = women.Average(p => p.Height);

                Person tallestMan = men.OrderByDescending(p => p.Height).First();
                Person tallestWoman = women.OrderByDescending(p => p.Height).First();

                resultTextBox.Text =
                    $"CSV-файл создан и считан. Записей: {people.Count}{Environment.NewLine}{Environment.NewLine}" +
                    $"Средний рост мужчин: {averageMenHeight:F2} см{Environment.NewLine}" +
                    $"Средний рост женщин: {averageWomenHeight:F2} см{Environment.NewLine}{Environment.NewLine}" +
                    $"Самый высокий мужчина: {tallestMan.LastName} {tallestMan.FirstName}, {tallestMan.Height} см{Environment.NewLine}" +
                    $"Самая высокая женщина: {tallestWoman.LastName} {tallestWoman.FirstName}, {tallestWoman.Height} см";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при выполнении задания: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static List<Person> GeneratePeople()
        {
            return new List<Person>
            {
                new Person("Иванов", "Иван", "М", Random.Shared.Next(165, 196)),
                new Person("Петров", "Алексей", "М", Random.Shared.Next(165, 196)),
                new Person("Сидоров", "Максим", "М", Random.Shared.Next(165, 196)),
                new Person("Смирнов", "Дмитрий", "М", Random.Shared.Next(165, 196)),
                new Person("Соколова", "Анна", "Ж", Random.Shared.Next(155, 186)),
                new Person("Попова", "Мария", "Ж", Random.Shared.Next(155, 186)),
                new Person("Волкова", "Елена", "Ж", Random.Shared.Next(155, 186)),
                new Person("Орлова", "Дарья", "Ж", Random.Shared.Next(155, 186))
            };
        }

        private void SaveToCsv(List<Person> people)
        {
            using var writer = new StreamWriter(
                _filePath,
                false,
                new System.Text.UTF8Encoding(true));
            writer.WriteLine("Фамилия;Имя;Пол;Рост");

            foreach (Person person in people)
            {
                writer.WriteLine(
                    $"{person.LastName};{person.FirstName};{person.Gender};{person.Height}");
            }
        }

        private List<Person> ReadFromCsv()
        {
            var people = new List<Person>();
            string[] lines = File.ReadAllLines(
                _filePath,
                System.Text.Encoding.UTF8);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');

                people.Add(new Person(
                    parts[0],
                    parts[1],
                    parts[2],
                    int.Parse(parts[3])));
            }

            return people;
        }

        private sealed class Person
        {
            public string LastName { get; }
            public string FirstName { get; }
            public string Gender { get; }
            public int Height { get; }

            public Person(string lastName, string firstName, string gender, int height)
            {
                LastName = lastName;
                FirstName = firstName;
                Gender = gender;
                Height = height;
            }
        }
    }
}
