using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Achievement
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GameId { get; set; }
        public string IconUrl { get; set; } = string.Empty;
    }
}
