﻿using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class GetEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
