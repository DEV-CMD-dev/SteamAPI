using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Achievement
{
    public class CreateAchievementDto
    {
        public string Name { get; set; }
        public int GameId { get; set; }
        public string? IconUrl { get; set; }
    }
}
