using System;
using Xamarin.Forms;
using XamLights.ViewModels;

namespace XamLights.Views
{
    public class GamePage : ContentPage
    {
        private GameViewModel _vm;
        private Grid _playingField;

        public GamePage()
        {
            Title = "Xamarin Lights Out";

            _vm = new GameViewModel();
            _vm.PropertyChanged += OnViewModelPropertyChanged;
            this.BindingContext = _vm;

            var layout = new StackLayout();

            // Subtitle
            var subtitle = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "A Xamarin.Forms game based on 'Lights Out' with a Marvel-twist."
            };

            // Playingfield
            _playingField = new Grid()
            {
                Opacity = 0.5,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 10,
                RowSpacing = Constants.TILE_PADDING,
                ColumnSpacing = Constants.TILE_PADDING
            };

            for (var i = 0; i < 5; i++)
            {
                _playingField.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Constants.TILE_SIZE) });
                _playingField.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Constants.TILE_SIZE) });
            }

            // Create the tiles
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    var tile = new Tile(x, y);
                    _vm.AddTile(tile);
                    _playingField.Children.Add(tile, x, y);
                }
            }

            // Start game
            var startButton = new Button()
            {
                HorizontalOptions = LayoutOptions.Center
            };
            startButton.SetBinding(Button.CommandProperty, "StartCommand");
            startButton.SetBinding(Button.TextProperty, "ButtonText");

            // Rules
            var rules = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                Text = "Click on a tile to adjust the state of tile and the direct adject ones. Keep on clicking to reveal the complete Marvel picture."
            };

            // Notes
            var notes = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Text = "Created by Marcofolio.net based on the electronic game 'Lights Out' from 1995 by Tiger Electronics. Picture used from Marvels 'Avengers: Infinity War' 2018 movie."
            };

            layout.Children.Add(subtitle);
            layout.Children.Add(_playingField);
            layout.Children.Add(startButton);
            layout.Children.Add(rules);
            layout.Children.Add(notes);

            this.Content = layout;
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFinished" && _vm.IsFinished)
            {
                DisplayAlert("Done", "Well done, the Avengers are proud!", "OK");
            }

            if (e.PropertyName == "IsActive")
            {
                if (_vm.IsActive)
                {
                    _playingField.FadeTo(1);
                }
                else
                {
                    _playingField.FadeTo(0.5);
                }
            }
        }
    }
}
