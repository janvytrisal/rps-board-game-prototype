/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Last player on board from tied players wins the tie or
/// nobody wins if players are on the same tile.
/// </summary>
[CreateAssetMenu(fileName = "LastPlayerWinsTie", menuName = "ScriptableObjects/TieResolver/LastPlayerWinsTie")]
public class LastPlayerWinsTie : NobodyWinsTie
{
    public override List<Player> WhoWinsTie(List<Player> players, GameBoard board)
    {
        List<Player> lastPlayers = new List<Player>();
        int minIndex = int.MaxValue;
        for (int i = 0; i < players.Count; i++)
        {
            int tileIndex = board.GetPlayerTile(players[i]);
            if(tileIndex < minIndex)
            {
                minIndex = tileIndex;
                lastPlayers.Clear();
                lastPlayers.Add(players[i]);
            }
            else if(tileIndex == minIndex)
                lastPlayers.Add(players[i]);
        }
        return (lastPlayers.Count == 1) ? lastPlayers : base.WhoWinsTie(players, board);
    }
}
