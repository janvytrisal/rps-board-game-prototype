/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Winners move by greater amount of tiles than their peers if
/// they won with their avatar card.
/// </summary>
/// <remarks>
/// Each player will have a random card assigned as an avatar at the start of the game.
/// Card avatars in a Rock Paper Scissors game are: Rock, Paper, Scissors.
/// Assigned card avatar is a public information.
/// </remarks>
[CreateAssetMenu(fileName = "AvatarCoreRules", menuName = "ScriptableObjects/CoreRules/AvatarCoreRules")]
public class AvatarCoreRules : ClassicCoreRules
{
    [SerializeField]
    private int _additionalTilesToMove = 1;

    /// <summary>
    /// Move winners by additional amount of tiles if they win with same card as is their avatar.
    /// </summary>
    /// <param name="players">Players that won the round.</param>
    public override void SetHowManyTilesWinnersMove(List<Player> players)
    {
        foreach(Player player in players)
        {
            if (player.Avatar == player.SelectedCard)
                player.TilesToMove = tilesToMove + _additionalTilesToMove;
            else
                player.TilesToMove = tilesToMove;
        }
    }
}
