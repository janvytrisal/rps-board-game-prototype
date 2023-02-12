/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Everybody wins when tie between players happens.
/// </summary>
[CreateAssetMenu(fileName = "EverybodyWinsTie", menuName = "ScriptableObjects/TieResolver/EverybodyWinsTie")]
public class EverybodyWinsTie : TieResolver
{
    public override List<Player> WhoWinsTie(List<Player> players, GameBoard board)
    {
        return players;
    }
}
