/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// General representation of an AI player.
/// </summary>
/// <remarks>
/// It selects a random card each time.
/// In theory it can't lose in the long run, because playing mixed strategy with equal probability
/// for each card in Rock Paper Scissors is a Nash-equillibrium.
/// </remarks>
[CreateAssetMenu(fileName = "AIPlayer", menuName = "ScriptableObjects/Player/AIPlayer")]
public class AIPlayer : Player
{
    public override void SelectCardToPlay()
    {
        int cardIndex = Random.Range(0, CardsInHand.Count);
        SelectedCard = CardsInHand[cardIndex];
        InvokeCardSelectedEvent();
    }
}
