using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Item
{
    public class PatchItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsTradable { get; set; }
    }
}
