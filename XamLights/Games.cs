using System;
using System.Collections.Generic;

namespace XamLights
{
    public class Games
    {
        private static Random _random = new Random();
        private static Games _container;
        private static readonly List<char[,]> _games = new List<char[,]>();

        private Games()
        {
            // lvl1
            _games.Add(new char[5, 5] {
                {'x', 'x', 'x', 'x', 'x'},
                {'x', 'x', 'o', 'x', 'x'},
                {'x', 'o', 'o', 'o', 'x'},
                {'x', 'x', 'o', 'x', 'x'},
                {'x', 'x', 'x', 'x', 'x'},
            });

            // lvl2
            _games.Add(new char[5, 5] {
                {'x', 'o', 'x', 'x', 'x'},
                {'o', 'o', 'o', 'x', 'x'},
                {'x', 'o', 'x', 'o', 'x'},
                {'x', 'x', 'o', 'o', 'o'},
                {'x', 'x', 'x', 'o', 'x'},
            });

            // lvl3
            _games.Add(new char[5, 5] {
                {'x', 'x', 'o', 'x', 'x'},
                {'x', 'x', 'o', 'x', 'x'},
                {'o', 'o', 'x', 'o', 'o'},
                {'x', 'x', 'o', 'x', 'x'},
                {'x', 'x', 'o', 'x', 'x'},
            });

            // lvl4
            _games.Add(new char[5, 5] {
                {'o', 'x', 'x', 'x', 'x'},
                {'x', 'o', 'x', 'x', 'x'},
                {'x', 'x', 'o', 'x', 'x'},
                {'x', 'x', 'x', 'o', 'x'},
                {'x', 'x', 'x', 'x', 'o'},
            });

            // lvl5
            _games.Add(new char[5, 5] {
                {'o', 'o', 'o', 'o', 'o'},
                {'o', 'o', 'o', 'o', 'o'},
                {'x', 'x', 'x', 'x', 'x'},
                {'o', 'o', 'o', 'o', 'o'},
                {'o', 'o', 'o', 'o', 'o'},
            });

            // lvl6
            _games.Add(new char[5, 5] {
                {'x', 'x', 'x', 'x', 'x'},
                {'x', 'x', 'x', 'x', 'x'},
                {'o', 'x', 'o', 'x', 'o'},
                {'x', 'x', 'x', 'x', 'x'},
                {'x', 'x', 'x', 'x', 'x'},
            });

            // lvl7
            _games.Add(new char[5, 5] {
                {'o', 'x', 'o', 'x', 'o'},
                {'o', 'o', 'o', 'o', 'o'},
                {'o', 'o', 'x', 'o', 'o'},
                {'o', 'o', 'o', 'o', 'o'},
                {'o', 'x', 'o', 'x', 'o'},
            });

            // lvl8
            _games.Add(new char[5, 5] {
                {'o', 'x', 'x', 'o', 'o'},
                {'x', 'x', 'o', 'x', 'o'},
                {'x', 'o', 'x', 'o', 'x'},
                {'o', 'x', 'o', 'x', 'x'},
                {'o', 'o', 'x', 'x', 'o'},
            });
        }

        public static char[,] RandomGame()
        {
            if(_container == null){
                _container = new Games();
            }

            var randomLevel = _random.Next(_games.Count);
            return _games[randomLevel];
        }
    }
}