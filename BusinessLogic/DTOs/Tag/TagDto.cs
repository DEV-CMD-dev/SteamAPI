using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Tag
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Picture { get; set; }
    }
}
