/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Seed preset in the Unity Editor.
/// </summary>
[CreateAssetMenu(fileName = "PresetSeed", menuName = "ScriptableObjects/RandomNumberGeneratorSeed/PresetSeed")]
public class PresetSeed : RandomNumberGeneratorSeed
{
    [SerializeField]
    private int _presetSeed;

    public override int GetSeed()
    {
        return _presetSeed;
    }
}
