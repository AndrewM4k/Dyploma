﻿using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Enums.Role Role { get; set; }
    }
}
