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
            if (gameSession.State == Model.Game.GameState.Finished)
                throw new ReversiException($"Game has finished, scores are: {gameSession.PlayerOne}={gameSession.PlayerOneScore} - {gameSession.PlayerTwo}={gameSession.PlayerTwoScore}");

            gameSession.State = Model.Game.GameState.Ongoing;
            var disc = gameSession.NextPlayer == gameSession.PlayerOne ? 'X' : 'O';

            try
            {
                if (x == -1 && y == -1)
                {
                    var moves = Reversi.GetMoves(gameSession.Board as char[,], disc);
                    if (moves.Length != 0)
                        throw new ReversiException("Only allowed to skip when no moves are available");

                    // Two skips in a row = game finished
                    var lastMove = gameSession.Moves.Last();
                    if (lastMove.X == -1 && lastMove.Y == -1)
                    {
                        gameSession.State = Model.Game.GameState.Finished;
                    }
                }
                else
                {
                    gameSession.Board = Reversi.Move(gameSession.Board as char[,], x, y, disc);
                    
                }

                gameSession.Turn++;
                gameSession.Moves.Add(new Model.Game.Move { Disc = disc, X = x, Y = y });

                var otherDisc = disc == 'X' ? 'O' : 'X';
                if (GetPossibleMoves(gameSession.Board as char[,], disc).Length == 0 && GetPossibleMoves(gameSession.Board as char[,], otherDisc).Length == 0)
                    gameSession.State = Model.Game.GameState.Finished;

                return null;
            }
            catch (ReversiException e)
            {
                return $"You did somthing you shouldn't have done...{Environment.NewLine}{e.Message}";
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

        public bool IsRandy(string gameKey)
        {
            if (_gameDatabase.GetExistingGame(gameKey).PlayerTwo.StartsWith("randy_"))
                return true;
            return false;
        }
    }
}
