using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class UserAchievement
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
        public DateTime UnlockedAt { get; set; }

    }
}
