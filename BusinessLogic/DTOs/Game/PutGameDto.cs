namespace BusinessLogic.DTOs.Game
{
    public class PutGameDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string SystemRequirements { get; set; }
        public string CoverImage { get; set; }
    }
}
