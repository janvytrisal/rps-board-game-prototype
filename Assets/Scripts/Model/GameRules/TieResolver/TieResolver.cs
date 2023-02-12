/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// How tie should be resolved.
/// </summary>
/// <remarks>
/// Implement specific tie resolver logic in derived classes.
/// </remarks>
public abstract class TieResolver : ScriptableObject
{
    /// <summary>
    /// Who should win when tie happens.
    /// </summary>
    /// <param name="players">Tied players.</param>
    /// <param name="board">Current game board.</param>
    public abstract List<Player> WhoWinsTie(List<Player> players, GameBoard board);
}
