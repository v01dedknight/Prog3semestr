namespace Lab0_Task1_Programming.Models
{
    public sealed class Person
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Height { get; set; }

        public string FullName => $"{LastName} {FirstName}";
    }
}
