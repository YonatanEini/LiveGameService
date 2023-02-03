using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGameService.objects
{
    public class LiveResult
    {
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }

        public List<GoalDetailsDto> HomeTeamScorers { get; set; }
        public List<GoalDetailsDto> AwayTeamScoeres { get; set; }

        public LiveResult()
        {
            this.HomeTeamGoals = 0;
            this.AwayTeamGoals = 0;
            this.HomeTeamScorers = new List<GoalDetailsDto>();
            this.AwayTeamScoeres = new List<GoalDetailsDto>();
        }

        public void UpdateScore(ScoreUpdateDto updatedScore)
        {
            if (updatedScore.ScoredTeam == Enums.GameTeamsEnum.HOME_TEAM)
            {
                this.HomeTeamGoals++;
                this.HomeTeamScorers.Add(updatedScore.GoalDetails);
            }
            else
            {
                this.AwayTeamGoals++;
                this.AwayTeamScoeres.Add(updatedScore.GoalDetails);
            }
        }
    }
}
