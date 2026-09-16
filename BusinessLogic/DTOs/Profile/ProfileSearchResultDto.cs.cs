using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Profile
{
    public class ProfileSearchResultDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? Avatar { get; set; }
    }
}
