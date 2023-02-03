using LiveGameService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGameService.objects
{
    public class ScoreUpdateDto
    {
        [Required]
        public GameTeamsEnum ScoredTeam { get; set; }
        [Required]
        public GoalDetailsDto GoalDetails { get; set; }
        [Required]
        public string GameID { get; set; }
    }
}
