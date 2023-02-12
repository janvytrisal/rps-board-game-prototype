/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Base element for player vs player interaction.
/// </summary>
/// <remarks>
/// Win mask is compared agains card mask of other player to determine the winner.
/// </remarks>
[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject
{
    [Tooltip("What bits identifies this card.")]
    [SerializeField]
    private IntegerMask _cardMask;
    [Tooltip("What bits this card wins against.")]
    [SerializeField]
    private IntegerMask _winMask;

    /// <summary>
    /// Binary mask of the card.
    /// </summary>
    public IntegerMask CardMask { get => _cardMask; }
    /// <summary>
    /// Binary mask the card mask wins against.
    /// </summary>
    public IntegerMask WinMask { get => _winMask; }
}
