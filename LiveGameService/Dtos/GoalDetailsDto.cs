using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGameService.objects
{
    public class GoalDetailsDto
    {
        public PlayerDetailsDto GoalScoredPlayer { get; set; }
        public int GoalMinute { get; set; }
    }
}
