/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// Controls main game loop.
/// </summary>
/// <remarks>
/// Steps of game loop:
///     1. All players selects card to play.
///     2. Waits until all players selects their card.
///     3. Each card is compared agains the others and players
///     with most wins move on the game board.
///     4. Repeat from 1. if no player is on the last tile of the game board, otherwise game ends.
/// </remarks>
public class GameManager: MonoBehaviour
{
    private LiveData _liveData;
    private WaitUntil _waitUntilAllPlayersSelectCard;

    /// <summary>
    /// The game has ended notification.
    /// </summary>
    public static event Action<LiveData> GameEnded;

    private void OnDisable()
    {
        if(_liveData != null)
        {
            if(_liveData.Players != null)
            {
                foreach (Player player in _liveData.Players)
                    player.CardSelected -= CardSelectedHandler;
            }
        }
    }

    /// <summary>
    /// Initialize the game and start the game loop.
    /// </summary>
    /// <param name="gameSettings">Settings for the game.</param>
    /// <param name="reuseLiveData">Set to true if same live data should be used.</param>
    public void StartGame(GameSettings gameSettings, bool reuseLiveData)
    {
        if (reuseLiveData && (_liveData != null))
        {
            _liveData.Board.ResetGameBoard();
            gameSettings.RuleSettings.CoreRules.InitializePlayers(_liveData.Players);
        }
        else
        {
            UnityEngine.Random.InitState(gameSettings.Seed.GetSeed());
            GameRules rules = new GameRules(gameSettings.RuleSettings);
            List<Player> playerInstances = new List<Player>();
            foreach(Player player in gameSettings.Players)
            {
                Player playerInstance = Instantiate(player);
                playerInstances.Add(playerInstance);
                playerInstance.CardSelected += CardSelectedHandler;
                playerInstance.CardsInHand.AddRange(rules.GetStartingCards());
            }
            GameBoard board = new GameBoard(gameSettings.BoardSettings, playerInstances);
            gameSettings.RuleSettings.CoreRules.InitializePlayers(playerInstances);
            _liveData = new LiveData(rules, playerInstances, board);
            _waitUntilAllPlayersSelectCard = new WaitUntil(() => _liveData.AllPlayersSelectedCard());
        }

        StartCoroutine(GameLoop());
    }

    #region GameLoop
    private IEnumerator GameLoop()
    {
        while(!_liveData.Rules.IsAnybodyGameWinner(_liveData.Players, _liveData.Board))
        {
            SelectCardStep();
            if (!_liveData.AllPlayersSelectedCard())
                yield return _waitUntilAllPlayersSelectCard;
            BattleAndMoveStep();
        }
        GameEnded?.Invoke(_liveData);
    }

    /// <summary>
    /// All players select a card to play.
    /// </summary>
    private void SelectCardStep()
    {
        _liveData.ResetSelectedCardsCounter();
        foreach (Player player in _liveData.Players)
            player.SelectCardToPlay();
    }
    
    /// <summary>
    /// Player selected a card to play.
    /// </summary>
    private void CardSelectedHandler(Player player)
    {
        _liveData.IncreaseSelectedCardsCounter();
    }

    /// <summary>
    /// Each player compares their card with other players.
    /// Winners get to move on the game board.
    /// </summary>
    private void BattleAndMoveStep()
    {
        _liveData.Rules.SetHowManyTilesToMove(_liveData.Players, _liveData.Board);
        for (int i = 0; i < _liveData.Players.Count; i++)
            _liveData.Board.MoveForward(_liveData.Players[i]);
        DevelopmentDebug.Log($"Cards: {string.Join("\t", _liveData.Players.Select(player => player.SelectedCard))}");
        DevelopmentDebug.Log($"Move: {string.Join("\t", _liveData.Players.Select(player => player.TilesToMove))}");
        DevelopmentDebug.Log($"Tiles: {string.Join("\t", _liveData.Board.OccupiedTiles.Select(occupiedTile => occupiedTile.Value))}");
    }
    #endregion
}

/// <summary>
/// Holds references to live data used by Game Manager.
/// </summary>
public class LiveData
{
    private GameRules _rules;
    private List<Player> _players;
    private GameBoard _board;
    private int _selectedCardsCounter;

    public LiveData(GameRules rules, List<Player> players, GameBoard board)
    {
        Rules = rules;
        Players = players;
        Board = board;
    }

    public GameRules Rules { get => _rules; private set => _rules = value; }
    public List<Player> Players { get => _players; private set => _players = value; }
    public GameBoard Board { get => _board; private set => _board = value; }
    public int SelectedCardsCounter { get => _selectedCardsCounter; private set => _selectedCardsCounter = value; }

    #region SelectedCardsCounterMethods
    public void ResetSelectedCardsCounter()
    {
        SelectedCardsCounter = 0;
    }
    public void IncreaseSelectedCardsCounter()
    {
        SelectedCardsCounter++;
    }
    public bool AllPlayersSelectedCard()
    {
        return (SelectedCardsCounter == Players.Count);
    }
    #endregion
}
