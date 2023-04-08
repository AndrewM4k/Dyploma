namespace Migartions.Dtos
{
    public class GetSportsmanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
