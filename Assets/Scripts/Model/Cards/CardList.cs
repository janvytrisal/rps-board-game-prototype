/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Preset list of cards.
/// </summary>
[CreateAssetMenu(fileName = "CardList", menuName = "ScriptableObjects/CardList")]
public class CardList : ScriptableObject
{
    [SerializeField]
    private List<Card> _cards;

    public List<Card> Cards { get => _cards; }
}
