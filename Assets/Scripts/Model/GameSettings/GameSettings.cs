/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Preset game settings to be used to quickly start various game setups.
/// </summary>
[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private RuleSettings _ruleSettings;
    [SerializeField]
    private List<Player> _players;
    [SerializeField]
    private BoardSettings _boardSettings;
    [SerializeField]
    private RandomNumberGeneratorSeed _seed;

    /// <summary>
    /// Settings that modifies general game rules.
    /// </summary>
    public RuleSettings RuleSettings { get => _ruleSettings; }
    /// <summary>
    /// Non instantiated players that will play the game.
    /// </summary>
    public List<Player> Players { get => _players; }
    /// <summary>
    /// Selected board settings.
    /// </summary>
    public BoardSettings BoardSettings { get => _boardSettings; }
    /// <summary>
    /// Random seed to use for controlled randomness.
    /// </summary>
    public RandomNumberGeneratorSeed Seed { get => _seed; }
}
