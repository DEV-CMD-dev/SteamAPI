using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class GameVersion
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string Version { get; set; } = string.Empty;
        public string PatchNotes { get; set; } = string.Empty;

    }
}
