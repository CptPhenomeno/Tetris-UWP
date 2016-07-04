using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel.Resources;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            var width = Tetris.Resources.NumOfColumns * Tetris.Resources.BlockWidth;
            var height = Tetris.Resources.NumOfRows * Tetris.Resources.BlockHeight + 2;
            ApplicationView.PreferredLaunchViewSize = new Size { Height = height, Width = width };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

    }

}
