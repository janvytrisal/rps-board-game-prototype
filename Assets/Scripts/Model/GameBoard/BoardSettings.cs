/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Settings for the game board.
/// </summary>
[CreateAssetMenu(fileName = "BoardSettings", menuName = "ScriptableObjects/BoardSettings")]
public class BoardSettings : ScriptableObject
{
    [SerializeField]
    private int _numberOfTiles;

    /// <summary>
    /// Number of tiles on the game board.
    /// </summary>
    public int NumberOfTiles { get => _numberOfTiles; }
}
