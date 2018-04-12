using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamLights.Views
{
    public class Tile : Grid
    {
        public event EventHandler<TileTappedEventArgs> Tapped;

        private int _xPos;
        private int _yPos;
        private bool _frontIsShowing;
        public int XPos { get { return _xPos; } }
        public int YPos { get { return _yPos; } }
        public bool FrontIsShowing { get { return _frontIsShowing; } }

        private BoxView _background;
        private Image _foreground;

        public Tile(int xPos, int yPos)
        {
            _xPos = xPos;
            _yPos = yPos;

            // Background
            _background = new BoxView { Color = Constants.BACKSIZE_COLOR };

            // Foreground
            _foreground = new Image
            {
                RotationY = -90,
                Source = ImageSource.FromResource($"XamLights.images.row-{yPos + 1}-col-{xPos + 1}.jpg")
            };

            // Tapframe
            var tapFrame = CreateTapFrame();

            // The Background, Foreground and tapGrid are placed in the same Cell of the Grid
            // which causes them to stack on top of eachother
            Children.Add(_background, 0, 0);
            Children.Add(_foreground, 0, 0);
            Children.Add(tapFrame, 0, 0);
        }

        public async Task Flip()
        {
            if (_frontIsShowing)
            {
                await _foreground.RotateYTo(-90, 400, Easing.CubicIn);
                _foreground.Opacity = 0;
                await _background.RotateYTo(0, 400, Easing.CubicOut);

                _frontIsShowing = false;
            }
            else
            {
                await _background.RotateYTo(90, 400, Easing.CubicIn);
                _foreground.Opacity = 1;
                await _foreground.RotateYTo(0, 400, Easing.CubicOut);

                _frontIsShowing = true;
            }
        }

        private Frame CreateTapFrame()
        {
            var frame = new Frame()
            {
                WidthRequest = Constants.TILE_SIZE,
                HeightRequest = Constants.TILE_SIZE,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HasShadow = false
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnFrameTapped;
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            return frame;
        }

        protected virtual void OnFrameTapped(object sender, EventArgs e)
        {
            var handler = Tapped;
            if (handler != null)
            {
                handler(this, new TileTappedEventArgs() { XPos = _xPos, YPos = _yPos });
            }
        }
    }
}
