/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game board made of tiles.
/// </summary>
/// <remarks>
/// Makes sure each player occupy exactly one tile at time.
/// </remarks>
public class GameBoard
{
    private BoardSettings _boardSettings;
    private Tile[] _tiles;
    private Dictionary<Player, int> _occupiedTiles;
    private List<Player> _players;

    /// <summary>
    /// Tiles of the game board. First is start, last is finish.
    /// </summary>
    public Tile[] Tiles { get => _tiles; private set => _tiles = value; }
    /// <summary>
    /// What tile index players occupy on the game board.
    /// </summary>
    public Dictionary<Player, int> OccupiedTiles { get => _occupiedTiles; private set => _occupiedTiles = value; }

    public GameBoard(BoardSettings settings, List<Player> players)
    {
        _boardSettings = settings;
        Tiles = new Tile[_boardSettings.NumberOfTiles];
        for (int i = 0; i < Tiles.Length; i++)
            Tiles[i] = new Tile();
        int startingIndex = 0;
        Tiles[startingIndex].AddPlayers(players);
        OccupiedTiles = new Dictionary<Player, int>();
        players.ForEach(player => OccupiedTiles.Add(player, startingIndex));
        _players = players;
    }

    #region MainMethods
    /// <summary>
    /// Moves player number of tiles forward.
    /// </summary>
    /// <remarks>
    /// It is expected tiles to move are set to player data.
    /// </remarks>
    public void MoveForward(Player player)
    {
        if (player.TilesToMove == 0)
            return;

        MoveToTile(player, OccupiedTiles[player] + player.TilesToMove);
    }
    public bool IsPlayerAtLastTile(Player player)
    {
        return OccupiedTiles[player] == (Tiles.Length - 1);
    }
    public int GetPlayerTile(Player player)
    {
        return OccupiedTiles[player];
    }

    /// <summary>
    /// Resets game board to the starting point.
    /// </summary>
    public void ResetGameBoard()
    {
        foreach (Player player in _players)
            MoveToTile(player, 0);
    }
    #endregion

    #region SupportMethods
    /// <summary>
    /// Makes sure tile index falls into tile array.
    /// </summary>
    private int ClampTileIndex(int index)
    {
        return Mathf.Clamp(index, 0, Tiles.Length - 1);
    }

    /// <summary>
    /// Moves player to specific index on the game board.
    /// </summary>
    private void MoveToTile(Player player, int index)
    {
        Tiles[OccupiedTiles[player]].RemovePlayer(player);
        int newIndex = ClampTileIndex(index);
        OccupiedTiles[player] = newIndex;
        Tiles[newIndex].AddPlayer(player);
    }
    #endregion
}

/// <summary>
/// Represents one tile on game board occupied by players.
/// </summary>
public class Tile
{
    private List<Player> _players;

    public Tile()
    {
        _players = new List<Player>();
    }

    public void AddPlayers(List<Player> players)
    {
        if (players == null)
        {
            DevelopmentDebug.LogWarning("Trying to add empty player list to the tile.");
            return;
        }

        _players.AddRange(players);
    }
    public void AddPlayer(Player player)
    {
        if (player == null)
        {
            DevelopmentDebug.LogWarning("Trying to add empty player to the tile.");
            return;
        }

        _players.Add(player);
    }
    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
    }
}
