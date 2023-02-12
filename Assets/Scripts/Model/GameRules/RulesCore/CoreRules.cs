/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines core rules for rule settings.
/// </summary>
public abstract class CoreRules : ScriptableObject
{
    /// <summary>
    /// Initialize players for the core rules.
    /// </summary>
    public abstract void InitializePlayers(List<Player> players);

    /// <summary>
    /// How many tiles should each winning player move on the game board.
    /// </summary>
    /// <param name="players">Winning players.</param>
    public abstract void SetHowManyTilesWinnersMove(List<Player> players);
}
