using Tetris.GameView.Tetraminoes;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.System;
using Windows.UI.Xaml.Input;
using System.Threading;
using Tetris.GameView.Blocks;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Tetris.GameView
{
    public sealed partial class GameView : UserControl
    {
        private const int SPEED_VAR = 10;

        private GameLogic gameTask;
        private bool speedUp;
        public Tetramino ActualTetramino { get; set; }
        private Image background;
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        public GameView()
        {
            this.InitializeComponent();
            GameCanvas.Width = CanvasWidth = Tetris.Resources.NumOfColumns * Tetris.Resources.BlockWidth;
            GameCanvas.Height = CanvasHeight = Tetris.Resources.NumOfRows * Tetris.Resources.BlockHeight;

            background = new Image();
            background.Source = new BitmapImage(new Uri("ms-appx:///Assets/grid_40.png"));
            background.Width = GameCanvas.Width;
            background.Height = GameCanvas.Height;
            background.SetValue(Canvas.LeftProperty, 0);
            background.SetValue(Canvas.TopProperty, 0);
            GameCanvas.Children.Add(background);

            Korobeiniki.Source = new Uri("ms-appx:///Assets/Korobeiniki.mp3");

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            gameTask = new GameLogic(this, CoreWindow.GetForCurrentThread().Dispatcher);
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Down)
            {
                gameTask.SpeedDown(SPEED_VAR);
                speedUp = false;
            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.P)
                System.Diagnostics.Debug.WriteLine("Here");

            if (args.VirtualKey == VirtualKey.Left)
                gameTask.MoveLeft();

            if (args.VirtualKey == VirtualKey.Right)
                gameTask.MoveRight();

            if (args.VirtualKey == VirtualKey.Up)
                gameTask.RotateTetramino();

            if (args.VirtualKey == VirtualKey.Down)
            {
                if (!speedUp)
                {
                    gameTask.SpeedUp(SPEED_VAR);
                    speedUp = true;
                }
            }
        }

        private void StartGameLoop(object sender, RoutedEventArgs e)
        {            
            gameTask.StartGame();
        }
        
        public void DrawTetramino()
        {
            ActualTetramino.Draw(GameCanvas);
        }

        public void DeleteElementFromCanvas(Block b)
        {
            b.DeleteBlock(GameCanvas);
        }
    } 

    public class GameLogic
    {
        private bool gameOver;
        private Task gameLoop;
        private GameView view;
        private CoreDispatcher dispatcher;

        private Block[,] landedBlocks;
        private byte[] blocksPerRow;

        private Tetramino actualTetramino;

        private readonly object moveMutex;
        private readonly object speedMutex;

        private bool bottomCollision;
        private bool leftCollision;
        private bool rightCollision;
        private bool pause;
        private int speed;

        private const int COLUMN = Tetris.Resources.NumOfColumns;
        private const int ROW = Tetris.Resources.NumOfRows;

        public GameLogic(GameView view, CoreDispatcher dispatcher)
        {
            this.view = view;
            this.dispatcher = dispatcher;
            moveMutex = new object();
            speedMutex = new object();
            speed = 1000;
            gameOver = false;
            landedBlocks = new Block[ROW, COLUMN];
            blocksPerRow = new byte[ROW];
            InitTask();
        }

        private void InitTask()
        {
            
            gameLoop = new Task(async () =>
            {   
                while (!gameOver)
                {
                    await CreateNewBlock();
                    await FallDown();
                    ClearCollisions();
                }

                System.Diagnostics.Debug.WriteLine("Game Over");
            });
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
                //actualTetramino = TetraminoesFactory.CreateTetramino(TetraminoShape.I);                
                CheckForBottomCollision();
                if (bottomCollision)
                    gameOver = true;
                else
                {
                    view.ActualTetramino = actualTetramino;
                    view.DrawTetramino();
                }
            });
        }

        public void TogglePause()
        {
            pause = !pause;
        }

        #region Speed
        public void SpeedUp(int dv)
        {
            lock (speedMutex)
            {
                speed /= dv;
            }
        }

        public void SpeedDown(int dv)
        {
            lock (speedMutex)
            {
                speed *= dv;
            }
        }
        
        #endregion

        #region Rotation
        public void RotateTetramino()
        {
            lock (moveMutex)
            {
                actualTetramino.Rotate(true);
                CheckRotation();
            }
        }

        private void CheckRotation()
        {
            var coord = actualTetramino.GetBlocksCoordinates();
            for (int c = 0; c < coord.Length; c += 2)
            {
                if (coord[c] < 0 || coord[c] >= view.CanvasHeight || coord[c + 1] < 0 || coord[c + 1] >= view.CanvasWidth ||
                    landedBlocks[(coord[c] / Tetris.Resources.BlockHeight), (coord[c + 1] / Tetris.Resources.BlockWidth)] != null)
                {
                    actualTetramino.Rotate(false);
                    break;
                }
            }
        }

        #region Wall Kick
        private void CheckRotationWithWallKick()
        {
            var coord = actualTetramino.GetBlocksCoordinates();
            bool pieceCollision = false;
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int minCollisionPoint = Int32.MaxValue;

            for (int c = 0; c < coord.Length; c += 2)
            {
                if (coord[c + 1] < min)
                    min = coord[c + 1];
                if (coord[c + 1] > max)
                    max = coord[c + 1];

                bool collision = landedBlocks[(coord[c] / Tetris.Resources.BlockHeight), (coord[c + 1] / Tetris.Resources.BlockWidth)] != null;
                pieceCollision |= collision;
                if (collision && coord[c + 1] < minCollisionPoint)
                    minCollisionPoint = coord[c + 1];
            }

            if (pieceCollision)
            {
                if (minCollisionPoint - min < max - minCollisionPoint)
                {
                    #region Check for left piece collision and eventually do a wall kick
                    actualTetramino.Rotate(false);
                    #endregion
                }

                else
                {
                    #region Check for right piece collision and eventually do a wall kick
                    actualTetramino.Rotate(false);
                    #endregion
                }
            }

            else
            {
                #region Check for left wall collision and eventually do a wall kick
                if (min < 0)
                {
                    int moves = -min / Tetris.Resources.BlockWidth;
                    bool canMove = true;

                    for (int i = 0; i < coord.Length; i += 2)
                    {
                        canMove &= landedBlocks[(coord[i] / Tetris.Resources.BlockHeight), (coord[i + 1] / Tetris.Resources.BlockWidth + moves)] == null;
                    }

                    if (canMove)
                        for (int m = 0; m < moves; m++)
                            actualTetramino.MoveRight();
                    else
                        actualTetramino.Rotate(false);
                }
                #endregion

                #region Check for right wall collision and eventually do a wall kick
                else if (max >= view.CanvasWidth)
                {
                    int moves = (max - (int)view.CanvasWidth) / Tetris.Resources.BlockWidth + 1;
                    bool canMove = true;

                    for (int i = 0; i < coord.Length; i += 2)
                    {
                        canMove &= landedBlocks[(coord[i] / Tetris.Resources.BlockHeight), (coord[i + 1] / Tetris.Resources.BlockWidth - moves)] == null;
                    }

                    if (canMove)
                        for (int m = 0; m < moves; m++)
                            actualTetramino.MoveLeft();
                    else
                        actualTetramino.Rotate(false);
                }
                #endregion
            }
        }
        #endregion

        #endregion

        #region Fall down
        private async Task FallDown()
        {
            int fallingSpeed;
            while (!bottomCollision)
            {
                lock (speedMutex)
                {
                    fallingSpeed = speed;
                }
                await Task.Delay(fallingSpeed);
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    lock (moveMutex)
                    {
                        CheckForBottomCollision();
                        if (!bottomCollision)
                            actualTetramino.Fall();
                        else
                            ClearRows();
                    }
                });                
            }
        }

        private void CheckForBottomCollision()
        {
            lock (moveMutex)
            {
                //First check for collision with other tetrominoes
                var blocks = actualTetramino.GetBlocks();
                int width = actualTetramino.GetBlockWidth();
                int height = actualTetramino.GetBlockHeight();

                int left, top;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    top = b.GetTop();
                    left = b.GetLeft();
                    grid_y = top / height;
                    grid_x = left / width;

                    if ((top + height) == view.CanvasHeight)
                    {
                        bottomCollision = true;
                        break;
                    }
                    else
                    {
                        if (landedBlocks[grid_y + 1, grid_x] != null)
                        {
                            bottomCollision = true;
                            break;
                        }
                    }
                }

                if (bottomCollision && !gameOver)
                {
                    foreach (Block b in blocks)
                    {
                        top = b.GetTop();
                        left = b.GetLeft();
                        grid_y = top / height;
                        grid_x = left / width;

                        landedBlocks[grid_y, grid_x] = b;
                        blocksPerRow[grid_y] += 1; 
                    }
                }
            }
        }
        #endregion

        #region Left move
        public void MoveLeft()
        {
            lock (moveMutex)
            {
                CheckForLeftCollision();
                if (!leftCollision)
                {
                    actualTetramino.MoveLeft();
                    rightCollision = false;
                }
                leftCollision = false;
            }
        }

        private void CheckForLeftCollision()
        {
            lock (moveMutex)
            {
                //First check for collision with other tetrominoes
                var blocks = actualTetramino.GetBlocks();
                int width = actualTetramino.GetBlockWidth();
                int height = actualTetramino.GetBlockHeight();

                int block_x, block_y;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    block_y = b.GetTop();
                    block_x = b.GetLeft();
                    grid_y = block_y / height;
                    grid_x = block_x / width;

                    if ((block_x - width) < 0)
                    {
                        leftCollision = true;
                        break;
                    }
                    else
                    {
                        if ((grid_x - 1) > 0 && landedBlocks[grid_y, grid_x - 1] != null)
                        {
                            leftCollision = true;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Right move
        public void MoveRight()
        {
            lock (moveMutex)
            {
                CheckForRightCollision();
                if (!rightCollision)
                {
                    actualTetramino.MoveRight();
                    leftCollision = false;
                }
                rightCollision = false;
            }
        }

        private void CheckForRightCollision()
        {
            lock (moveMutex)
            {
                //First check for collision with other tetrominoes
                var blocks = actualTetramino.GetBlocks();
                int width = actualTetramino.GetBlockWidth();
                int height = actualTetramino.GetBlockHeight();

                int block_x, block_y;
                int grid_x, grid_y;

                foreach (Block b in blocks)
                {
                    block_y = b.GetTop();
                    block_x = b.GetLeft();
                    grid_y = block_y / height;
                    grid_x = block_x / width;

                    if ((block_x + width) == view.CanvasWidth)
                    {
                        rightCollision = true;
                        break;
                    }
                    else
                    {
                        if ((grid_x + 1) < COLUMN && landedBlocks[grid_y, grid_x + 1] != null)
                        {
                            rightCollision = true;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        private void ClearCollisions()
        {
            bottomCollision = leftCollision = rightCollision = false;
        }

        private void ClearRows()
        {
            for (int row = 0; row < ROW; row++)
            {
                if (blocksPerRow[row] == COLUMN)
                {
                    blocksPerRow[row] = 0;
                    for (int col = 0; col < COLUMN; col++)
                    {
                        var b = landedBlocks[row, col];
                        view.DeleteElementFromCanvas(b);

                        landedBlocks[row, col] = null;

                        if (row != 0)
                        {
                            for (int p_row = row - 1; p_row >= 0; p_row--)
                            {
                                b = landedBlocks[p_row, col];

                                if (b != null)
                                {
                                    b.MoveDown();
                                    landedBlocks[p_row + 1, col] = b;
                                    landedBlocks[p_row, col] = null;
                                    blocksPerRow[p_row] -= 1;
                                    blocksPerRow[p_row + 1] += 1;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
