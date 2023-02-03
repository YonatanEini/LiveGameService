using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGameService.Dtos
{
    public class GameInitDto
    {
        [Required]
        public string HomeTeamName { get; set; }
        [Required]
        public string AwayTeamName { get; set; }
        [Required]
        public DateTime LiveGameDate { get; set; }
    }
}
