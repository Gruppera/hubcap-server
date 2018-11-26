using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Hubcap.Game.Reversi;

namespace Hubcap.Api.Logic
{
    public class GameDatabase
    {
        private readonly ConcurrentDictionary<string, Model.Game> _database;

        public GameDatabase()
        {
            _database = new ConcurrentDictionary<string, Model.Game>();
        }

        public string AssignGame(string playerKey)
        {
            var game = _database.FirstOrDefault(x => string.IsNullOrEmpty(x.Value.PlayerTwo));

            if (game.Key is default(string))
            {
                var gameKey = Guid.NewGuid().ToString();
                var g = new Model.Game { Board = Reversi.GetInitialState(), PlayerOne = playerKey };

                var added = _database.TryAdd(gameKey, g);
                if (!added) throw new ApplicationException("Game already added");

                return gameKey;
            }

            game.Value.PlayerTwo = playerKey;
            return game.Key;
        }

        public Model.Game GetExistingGame(string gameKey)
        {
            var gameExists = _database.TryGetValue(gameKey, out var game);
            if (!gameExists) return null;

            return game;
        }

        public string AssignExistingGameWithPlayer(string playerKey, string opponent)
        {
            var game = _database.FirstOrDefault(x => string.IsNullOrEmpty(x.Value.PlayerTwo) && x.Value.PlayerOne == opponent);
            if (game.Key == null)
                return null;

            game.Value.PlayerTwo = playerKey;
            return game.Key;
        }

        public string AssignGameAgainstRandomBot(string playerKey)
        {
            throw new NotImplementedException();
        }
    }
}