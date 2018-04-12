using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamLights.Views;
using System.Linq;
using System.Threading.Tasks;

namespace XamLights.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        // Two dimensional array that represents the playing field
        private Tile[,] _tiles = new Tile[5,5];

        List<Task> _fieldsToFlip = new List<Task>();

        private bool _isAnimating;
        private bool _isActive;
        public bool IsActive
        {
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
            get { return _isActive; }
        }

        private bool _isFinished;
        public bool IsFinished
        {
            set
            {
                if (_isFinished != value)
                {
                    _isFinished = value;
                    OnPropertyChanged("IsFinished");
                }
            }
            get { return _isFinished; }
        }

        private string _buttonText;
        public string ButtonText
        {
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged("ButtonText");
                }
            }
            get { return _buttonText; }
        }

        public ICommand StartCommand { get; private set; }

        public GameViewModel()
        {
            ButtonText = "Start game";
            StartCommand = new Command(StartGame);
        }

        public void AddTile(Tile tile)
        {
            tile.Tapped += TileTapped;
            _tiles[tile.XPos, tile.YPos] = tile;
        }

        private async void StartGame()
        {
            if (IsActive || IsFinished)
            {
                await Restart();
            };

            ButtonText = "Randomize & start over";

            IsFinished = false;
            IsActive = true;
            var game = Games.RandomGame();

            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if(game[x, y] == 'x')
                    {
                        _fieldsToFlip.Add(_tiles[x, y].Flip());
                    }
                }
            }
            await FlipFields();
        }

        private async Task Restart()
        {
            foreach (var tile in _tiles)
            {
                if (tile.FrontIsShowing)
                {
                    _fieldsToFlip.Add(tile.Flip());
                }
            }
            await FlipFields();
        }

        private async void TileTapped(object sender, TileTappedEventArgs e)
        {
            if (!IsActive || _isAnimating) return;

            // Check which fields we need to flip
            // The field itself
            _fieldsToFlip.Add(_tiles[e.XPos, e.YPos].Flip());

            // The field above
            if (e.YPos - 1 >= 0)
            {
                _fieldsToFlip.Add(_tiles[e.XPos, e.YPos - 1].Flip());
            }

            // The field underneath
            if (e.YPos + 1 < 5)
            {
                _fieldsToFlip.Add(_tiles[e.XPos, e.YPos + 1].Flip());
            }

            // The field to the left
            if (e.XPos - 1 >= 0)
            {
                _fieldsToFlip.Add(_tiles[e.XPos - 1, e.YPos].Flip());
            }

            // The field to the right
            if (e.XPos + 1 < 5)
            {
                _fieldsToFlip.Add(_tiles[e.XPos + 1, e.YPos].Flip());
            }

            await FlipFields();
            CheckPuzzle();
        }

        private async Task FlipFields()
        {
            _isAnimating = true;
            await Task.WhenAll(_fieldsToFlip);

            _fieldsToFlip.Clear();
            _isAnimating = false;
        }

        private void CheckPuzzle()
        {
            var allFrontsAreShowing = true;
            foreach(var tile in _tiles)
            {
                if (!tile.FrontIsShowing)
                {
                    allFrontsAreShowing = false;
                    continue;
                }
            }

            if(allFrontsAreShowing)
            {
                PuzzleComplete();
            }
        }

        private void PuzzleComplete()
        {
            IsFinished = true;
            IsActive = false;
        }
    }
}
