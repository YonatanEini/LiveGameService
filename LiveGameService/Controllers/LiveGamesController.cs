using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LiveGameService.Dtos;
using LiveGameService.objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiveGameService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiveGamesController : ControllerBase
    {
        public GameResultManager GamesManager { get; set; }
        public LiveGamesController()
        {
            this.GamesManager = GameResultManager.GetInsatnce();
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine("Service Is Alive");
            return true;
        }

        [HttpGet("GetAllGames")]
        public IEnumerable<GameResult> GetLiveGames()
        {
            return this.GamesManager.LiveGamesDict.Values;
        }

        [HttpPost("InitGame")]
        public string InitializeLiveGame([FromBody] GameInitDto gameDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string generatedNewGameId = this.GamesManager.AddGame(gameDto);
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return generatedNewGameId;
                }
                catch
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return null;
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        [HttpPut("UpdateResult")]
        public async Task<HttpResponseMessage> UpdateLiveGame([FromBody] ScoreUpdateDto updatedScore)
        {
            if (ModelState.IsValid)
            {
                bool flag = await this.GamesManager.UpdateScore(updatedScore);
                HttpResponseMessage responseObject = flag ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return responseObject;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}