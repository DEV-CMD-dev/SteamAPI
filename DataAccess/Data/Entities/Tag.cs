using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagTitle { get; set; } = string.Empty;
        public List<Game> Games { get; set; } = [];
    }
}
