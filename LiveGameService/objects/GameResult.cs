using LiveGameService.Dtos;
using System;

namespace LiveGameService.objects
{
    public class GameResult
    {
        public string HomeTeamName { get; set; }
        public string AwayGameName { get; set; }
        public LiveResult LiveResult { get; set; }
        public DateTime GameDateTime { get; set; }
        
        public GameResult(GameInitDto initalizeGameObject)
        {
            this.HomeTeamName = initalizeGameObject.HomeTeamName;
            this.AwayGameName = initalizeGameObject.AwayTeamName;
            this.GameDateTime = initalizeGameObject.LiveGameDate;
            this.LiveResult = new LiveResult();
        }

        public void UpdateGameResult(ScoreUpdateDto updatedScore)
        {
            this.LiveResult.UpdateScore(updatedScore);
        }
    }
}
