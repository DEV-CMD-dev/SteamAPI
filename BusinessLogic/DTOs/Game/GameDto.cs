namespace BusinessLogic.DTOs.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DeveloperId { get; set; }
  
        public int PublisherId { get; set; }
    
        public DateOnly ReleaseDate { get; set; }
        public decimal Price { get; set; }
   
        public string SystemRequirements { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
     
    }
}
