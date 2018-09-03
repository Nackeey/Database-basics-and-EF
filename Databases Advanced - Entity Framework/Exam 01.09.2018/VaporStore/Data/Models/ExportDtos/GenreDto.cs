using System;
using System.Collections.Generic;
using System.Text;
using VaporStore.Data.Models.ImportDtos;

namespace VaporStore.Data.Models.ExportDtos
{
    public class GenreDto
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public GameDto[] Games { get; set; }

        public int TotalPlayers { get; set; }
    }
}
