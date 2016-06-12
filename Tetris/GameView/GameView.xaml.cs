using Tetris.GameView.Tetraminoes;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.System;
using Windows.UI.Xaml.Input;
using System.Threading;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Tetris.GameView
{
    public sealed partial class GameView : UserControl
    {
        private Tetris gameTask;
        public Tetramino ActualTetramino { get; set; }

        public GameView()
        {
            this.InitializeComponent();
            gameTask = new Tetris(this, CoreWindow.GetForCurrentThread().Dispatcher);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Left)
                ActualTetramino.MoveLeft();

            if (args.VirtualKey == VirtualKey.Right)
                ActualTetramino.MoveRight();
        }

        private void StartGameLoop(object sender, RoutedEventArgs e)
        {            
            gameTask.StartGame();
        }
        
        public void DrawTetramino()
        {
            ActualTetramino.Draw(GameCanvas);
        }
    }

    public class Tetris
    {
        private bool gameOver;
        private Task gameLoop;
        private byte[,] gameGrid;
        private GameView view;
        private CoreDispatcher dispatcher;
        private Tetramino actualTetramino;

        private bool collision;
        private int speed;

        public Tetris(GameView view, CoreDispatcher dispatcher)
        {
            this.view = view;
            this.dispatcher = dispatcher;
            Init();
        }

        private void Init()
        {
            speed = 500;
            gameOver = false;
            gameLoop = new Task(async () =>
            {
                while (!gameOver)
                {
                    await CreateNewBlock();
                    await Task.Delay(speed);
                    await MoveBlock();
                    collision = false;
                }

                System.Diagnostics.Debug.WriteLine("Game Over");
            });
            gameGrid = new byte[16, 10];
            collision = false;
        }

        

        public void StartGame()
        {
            gameLoop.Start();
        }
        //Game loop
        private async Task CreateNewBlock()
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                actualTetramino = TetraminoesFactory.CreateRandomTetramino();
                CheckForCollision();
                if (collision)
                    gameOver = true;
                else
                {
                    view.ActualTetramino = actualTetramino;
                    view.DrawTetramino();
                }
            });
        }

        private async Task MoveBlock()
        {
            while (!collision)
            {
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    CheckForCollision();
                    if (!collision)
                        actualTetramino.Fall();
                });
                await Task.Delay(speed);
            }
            
        }

        private async Task MoveLeftRight()
        {

        }

        private void CheckForCollision()
        {
            //First check for collision with other tetrominoes
            var blocksCoordinates = actualTetramino.GetBlocksCoordinates();
            int width = actualTetramino.GetBlockWidth();
            int height = actualTetramino.GetBlockHeight();

            int block_x, block_y;
            int grid_x, grid_y;

            foreach (Tuple<int,int> coord in blocksCoordinates)
            {
                block_y = coord.Item1;
                block_x = coord.Item2;
                grid_y = block_y / height;
                grid_x = block_x / width;

                if ((block_y + height) == view.Height)
                {
                    collision = true;
                    break;
                }
                else
                {
                    if (gameGrid[grid_y + 1, grid_x] != 0)
                    {
                        collision = true;
                        break;
                    }
                }
            }

            if (collision && !gameOver)
            {
                foreach (Tuple<int, int> coord in blocksCoordinates)
                {
                    grid_y = coord.Item1 / height;
                    grid_x = coord.Item2 / width;

                    gameGrid[grid_y, grid_x] = 1;
                }
            }
        }
    }
}
