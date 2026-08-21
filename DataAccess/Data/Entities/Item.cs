using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        
        public int GameId { get; set; }
        public Game Game { get; set; }

        public bool IsTradable { get; set; } = true;
    }
}
