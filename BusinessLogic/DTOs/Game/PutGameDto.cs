using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Game
{
    public class PutGameDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string SystemRequirements { get; set; }
        public string CoverImageVertical { get; set; }
        public string CoverImageHorizontal { get; set; }
        public List<int> TagIds { get; set; }
    }
}
