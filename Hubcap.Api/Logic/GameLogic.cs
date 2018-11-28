using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hubcap.Game.Reversi;

namespace Hubcap.Api.Logic
{
    public class GameLogic
    {
        private readonly GameDatabase _gameDatabase;

        public GameLogic(GameDatabase gameDatabase)
        {
            _gameDatabase = gameDatabase;
        }

        public string CreateGameSession(string playerKey)
        {
            var gameKey = _gameDatabase.AssignGame(playerKey);
            return gameKey;
        }

        public Model.Game GetGameSession(string gameKey)
        {
            var game = _gameDatabase.GetExistingGame(gameKey);
            return game;
        }

        public string UpdateGameState(Model.Game gameSession, int x, int y)
        {
            var disc = gameSession.NextPlayer == gameSession.PlayerOne ? 'X' : 'O';

            try
            {
                gameSession.Board = Reversi.Move(gameSession.Board as char[,], x, y, disc);
                gameSession.Turn++;
                return null;
            }
            catch (ReversiException e)
            {
                return "You did somthing you shouldn't have done... We're watching ಠ_ಠ";

                // TODO: return useful message (can still be funny though)
                //return e.Message;
            }

        }

        public string CreateGameSession(string playerKey, string opponent)
        {
            switch (opponent.ToLower())
            {
                case "randy":
                    return _gameDatabase.AssignGameAgainstRandomBot(playerKey);
                default:
                    return _gameDatabase.AssignExistingGameWithPlayer(playerKey, opponent);
            }
        }

        public (int x, int y)[] GetPossibleMoves(char[,] board, char disc)
        {
            return Reversi.GetMoves(board, disc);
        }
    }
}
