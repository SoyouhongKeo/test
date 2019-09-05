using System;
using SwinGameSDK;

namespace BattleShip1
{

    /// <summary>

/// The battle phase is handled by the DiscoveryController.

/// </summary>
    static class DiscoveryController
    {

        /// <summary>
    /// Handles input during the discovery phase of the game.
    /// </summary>
    /// <remarks>
    /// Escape opens the game menu. Clicking the mouse will
    /// attack a location.
    /// </remarks>
        public static void HandleDiscoveryInput()
        {
            if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
                GameController.AddNewState(GameState.ViewingGameMenu);

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
                DoAttack();
        }

        /// <summary>
    /// Attack the location that the mouse if over.
    /// </summary>
        private static void DoAttack()
        {
            Point2D mouse;

            mouse = SwinGame.MousePosition();

            // Calculate the row/col clicked
            int row, col;
            row = Convert.ToInt32(Math.Floor((double)(mouse.Y - (float)UtilityFunctions.FIELD_TOP) / (float)(UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
            col = Convert.ToInt32(Math.Floor((double)(mouse.X - (float)UtilityFunctions.FIELD_LEFT) / (float)(UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

            if ((row >= 0) & (row < GameController.HumanPlayer.EnemyGrid.Height))
            {
                if ((col >= 0) & (col < GameController.HumanPlayer.EnemyGrid.Width))
                    GameController.Attack(row, col);
            }
        }

        /// <summary>
    /// Draws the game during the attack phase.
    /// </summary>s
        public static void DrawDiscovery()
        {
            const int SCORES_LEFT = 172;
            const int SHOTS_TOP = 157;
            const int HITS_TOP = 206;
            const int SPLASH_TOP = 256;

            if ((SwinGame.KeyDown(KeyCode.vk_LSHIFT) | SwinGame.KeyDown(KeyCode.vk_RSHIFT)) & SwinGame.KeyDown(KeyCode.vk_c))
                UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
            else
                UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);

            UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
            UtilityFunctions.DrawMessage();

            SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), (float)SCORES_LEFT, (float)SHOTS_TOP);
            SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), (float)SCORES_LEFT, (float)HITS_TOP);
            SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), (float)SCORES_LEFT, (float)SPLASH_TOP);
        }
    }
}
