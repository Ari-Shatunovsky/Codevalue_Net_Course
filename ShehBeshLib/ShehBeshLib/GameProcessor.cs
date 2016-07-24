using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShehBeshLib
{
    public enum GameState
    {
        NextTurn = 1,
        PlayerAWins,
        PlayerBWins
    }

    public class GameProcessor
    {
        private GameState GameState { get; set; }
        private GameField Field { get; set; }
        private IPlayer PlayerA { get; set; }
        private IPlayer PlayerB { get; set; }
        private IPlayer CurrentPlayer { get; set; }

        public IRenderEngine RenderEngine { get; set; }

        private GameLogic GameLogic { get; set; }

        public GameProcessor()
        {
            GameLogic = new GameLogic();
            PlayerA = new ComputerPlayer() { State = CellState.PlayerA };
            PlayerB = new ComputerPlayer() { State = CellState.PlayerB };
            RenderEngine = new ConsoleRenderEngine();
            GameLogic.TurnSucceed += (sender, eventArgs) => { RenderEngine.RenderFiled(Field); };
        }

        public GameProcessor(IPlayer playerA, IPlayer playerB, IRenderEngine renderEngine)
        {
            GameLogic = new GameLogic();
            PlayerA = playerA;
            PlayerB = playerB;
            RenderEngine = renderEngine;
            GameLogic.TurnSucceed += (sender, eventArgs) => { RenderEngine.RenderFiled(Field); };
        }

        public void CreateNewGame()
        {
            Field = GameLogic.InitGameField();
            RenderEngine.RenderFiled(Field);
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == PlayerA ? PlayerB : PlayerA;
        }

        private string GetPlayerName()
        {
            return CurrentPlayer == PlayerA ? "Player A" : "Player B";
        }

        public void SetFirstPlayer()
        {
            int[] playerADice = GameLogic.RollDice();
            RenderEngine.RenderMessage($"Player A dice is: {playerADice[0]}, {playerADice[1]}");
            int[] playerBDice = GameLogic.RollDice();
            RenderEngine.RenderMessage($"Player B dice is: {playerBDice[0]}, {playerBDice[1]}");
            if (playerADice[0] + playerADice[1] > playerBDice[0] + playerBDice[1])
            {
                CurrentPlayer = PlayerA;
            }
            else
            {
                CurrentPlayer = PlayerA;
            }
            RenderEngine.RenderMessage($"{GetPlayerName()} makes first turn: {playerADice[0]}, {playerADice[1]}");
        }

        public void StartGame()
        {
            RenderEngine.RenderFiled(Field);
            SetFirstPlayer();
            GameState = GameState.NextTurn;
            while (GameState == GameState.NextTurn)
            {
                RenderEngine.RenderMessage($"{GetPlayerName()} makes turn.");
                int[] dice = GameLogic.RollDice();
                RenderEngine.RenderMessage($"Dice is: {dice[0]}, {dice[1]}");

                GameLogic.MakeTurn(CurrentPlayer, dice, Field);
                SwitchPlayer();
                if (Field.OutPlayerA.NumberOfCheckers == 15)
                {
                    GameState = GameState.PlayerAWins;
                    RenderEngine.RenderMessage("Player A wins!");
                    return;
                }
                if (Field.OutPlayerB.NumberOfCheckers == 15)
                {
                    GameState = GameState.PlayerBWins;
                    RenderEngine.RenderMessage("Player B wins!");
                    return;
                }
            }
        }
    }
}
