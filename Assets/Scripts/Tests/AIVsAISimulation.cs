/*
 * Author: Jan Vytrisal
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates preset number of games of AI vs AI and logs the results.
/// </summary>
public class AIVsAISimulation : MonoBehaviour
{
    [SerializeField]
    private GameSettings _settings;
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private int _totalGames = 1;

    private int _currentGame;
    private Dictionary<Player, GameResult> _results;
    private LiveData _lastLiveData;
    private bool _gameEndHandled;
    private DateTime _startTime;

    private void Awake()
    {
        _currentGame = 0;
        _results = new Dictionary<Player, GameResult>();
    }
    private void OnEnable()
    {
        GameManager.GameEnded += GameEndedHandler;
    }
    private void OnDisable()
    {
        GameManager.GameEnded -= GameEndedHandler;
    }
    private void Start()
    {
        StartCoroutine(StartSimulation());
    }

    private IEnumerator StartSimulation()
    {
        Debug.Log("Start");
        _startTime = DateTime.Now;
        while (_currentGame < _totalGames)
        {
            _gameEndHandled = false;
            _currentGame++;
            _manager.StartGame(_settings, true);
            yield return new WaitUntil(() => _gameEndHandled);
        }
    }
    /// <summary>
    /// Logs game result.
    /// </summary>
    /// <remarks>
    /// If single player is on the last tile, it gets a win.
    /// If more than one player are on the last tile, they get a tie.
    /// Every other player gets a loss.
    /// </remarks>
    private void GameEndedHandler(LiveData data)
    {
        if(_results.Count == 0)
            data.Players.ForEach(player => _results.Add(player, new GameResult()));

        List<Player> winners = data.Rules.WhoAreGameWinners(data.Players, data.Board);
        if (winners.Count == 1)
            _results[winners[0]].Wins++;
        else
            winners.ForEach(player => _results[player].Ties++);
        
        for(int i= 0; i < data.Players.Count; i++)
        {
            Player player = data.Players[i];
            if (!winners.Contains(player))
                _results[player].Loses++;
        }
        
        _lastLiveData = data;
        _gameEndHandled = true;

        if (_currentGame < _totalGames)
            ResultPrinter((string result) => DevelopmentDebug.Log(result));
        else
        {
            Debug.Log("-----------------");
            ResultPrinter((string result) => Debug.Log(result));
            Debug.Log($"Elapsed time: {DateTime.Now - _startTime}");
            Debug.Log("-----------------");
        }
    }

    private void ResultPrinter(Action<string> logger)
    {
        for (int i = 0; i < _lastLiveData.Players.Count; i++)
        {
            Player player = _lastLiveData.Players[i];
            logger($"Player {i}: {_results[player]}");
        }
    }
}

/// <summary>
/// Represents simulation results.
/// </summary>
/// <remarks>
/// Should be used to counts wins, ties, loses of player vs player.
/// </remarks>
public class GameResult
{
    public int Wins { get; set; }
    public int Ties { get; set; }
    public int Loses { get; set; }

    public GameResult()
    {
        Wins = 0;
        Ties = 0;
        Loses = 0;
    }

    public override string ToString()
    {
        return $"Wins: {Wins}, Ties: {Ties}, Loses: {Loses}";
    }
}
