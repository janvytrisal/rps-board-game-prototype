/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Represents specific settings of the general game rules.
/// </summary>
[CreateAssetMenu(fileName = "RuleSettings", menuName = "ScriptableObjects/RuleSettings")]
public class RuleSettings : ScriptableObject
{
    [SerializeField]
    private TieResolver _tieResolver;
    [SerializeField]
    private CoreRules _coreRules;
    [SerializeField]
    private CardList _startingCards;

    public TieResolver TieResolver { get => _tieResolver; }
    public CoreRules CoreRules { get => _coreRules; }
    /// <summary>
    /// Cards each player will has at the start of the game.
    /// </summary>
    public CardList StartingCards { get => _startingCards; }
}
