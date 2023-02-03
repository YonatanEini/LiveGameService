using LiveGameService.Dtos;
using LiveGameService.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LiveGameService.objects
{
    public class GameResultManager
    {
        public Dictionary<string, GameResult> LiveGamesDict { get; set; }
        public Dictionary<string, KafkaProducer> ProducerScoresUpdateDict { get; set; }

        private static GameResultManager _instance = null;
        private readonly object _locker = new object();

        public static GameResultManager GetInsatnce()
        {
            return _instance ??= new GameResultManager(); 
        }
        private GameResultManager()
        {
            this.LiveGamesDict = new Dictionary<string, GameResult>();
            this.ProducerScoresUpdateDict = new Dictionary<string, KafkaProducer>();
        }

        public string AddGame(GameInitDto newGame)
        {
            Monitor.Enter(_locker);
            string gameId = GenerateGameID();
            GameResult newLiveGame = new GameResult(newGame);
            this.LiveGamesDict.Add(gameId, newLiveGame);
            this.ProducerScoresUpdateDict.Add(gameId, new KafkaProducer(gameId));
            Monitor.Exit(_locker);
            return gameId;
        }

        public bool RemoveGame(string gameID)
        {
            bool result = false;
            Monitor.Enter(_locker);
            try
            {
                this.LiveGamesDict.Remove(gameID);
                this.ProducerScoresUpdateDict.Remove(gameID);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                Monitor.Exit(_locker);
            }
            return result;
        }

        public async Task<bool> UpdateScore(ScoreUpdateDto updateDto)
        {
           if (this.LiveGamesDict.ContainsKey(updateDto.GameID))
           {
                this.LiveGamesDict[updateDto.GameID].UpdateGameResult(updateDto);
                await this.ProducerScoresUpdateDict[updateDto.GameID].WriteToKafka(updateDto);
                return true;
           }
            return false;
        }

        public string GenerateGameID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
