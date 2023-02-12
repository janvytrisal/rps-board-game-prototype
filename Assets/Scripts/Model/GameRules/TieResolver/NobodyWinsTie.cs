/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Nobody wins when tie between players happens.
/// </summary>
[CreateAssetMenu(fileName = "NobodyWinsTie", menuName = "ScriptableObjects/TieResolver/NobodyWinsTie")]
public class NobodyWinsTie : TieResolver
{
    private readonly List<Player> _empty = new List<Player>();

    public override List<Player> WhoWinsTie(List<Player> players, GameBoard board)
    {
        return _empty;
    }
}
