/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// AI player that is biased toward card that mirrors their avatar when selecting a card to play.
/// </summary>
[CreateAssetMenu(fileName = "AvatarAIPlayer", menuName = "ScriptableObjects/Player/AvatarAIPlayer")]
public class AvatarAIPlayer : AIPlayer
{
    [Range(0, 1)]
    [SerializeField]
    private float _avatarSelectionProbability;

    public override void SelectCardToPlay()
    {
        if (_avatarSelectionProbability > 0)
        {
            int avatarIndex = CardsInHand.IndexOf(Avatar);
            float avatarSelectionAttemptProbability = Random.value;
            if (avatarSelectionAttemptProbability <= _avatarSelectionProbability)
                SelectedCard = CardsInHand[avatarIndex];
            else
            {
                int selectedIndex = Random.Range(0, CardsInHand.Count - 1);
                if (selectedIndex == avatarIndex)
                    selectedIndex = (selectedIndex + 1) % CardsInHand.Count;
                SelectedCard = CardsInHand[selectedIndex];
            }
            InvokeCardSelectedEvent();
        }
        else
            base.SelectCardToPlay();
    }
}
