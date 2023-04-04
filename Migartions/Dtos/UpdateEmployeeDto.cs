namespace Migartions.Dtos
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Specialization { get; set; }
    }
}
