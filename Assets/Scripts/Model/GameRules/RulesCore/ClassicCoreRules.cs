/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The winners will move by preset number of tiles.
/// </summary>
[CreateAssetMenu(fileName = "ClassicCoreRules", menuName = "ScriptableObjects/CoreRules/ClassicCoreRules")]
public class ClassicCoreRules : CoreRules
{
    [SerializeField]
    protected int tilesToMove = 1;
    [SerializeField]
    protected CardList allCardTypes;

    /// <summary>
    /// Assign random avatar card to each player.
    /// </summary>
    public override void InitializePlayers(List<Player> players)
    {
        foreach (Player player in players)
        {
            int cardTypeIndex = Random.Range(0, allCardTypes.Cards.Count);
            player.Avatar = allCardTypes.Cards[cardTypeIndex];
        }
    }

    public override void SetHowManyTilesWinnersMove(List<Player> players)
    {
        players.ForEach(player => player.TilesToMove = tilesToMove);
    }
}
