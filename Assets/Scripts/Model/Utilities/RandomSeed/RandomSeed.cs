/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Random seed as current date and time.
/// </summary>
[CreateAssetMenu(fileName = "RandomSeed", menuName = "ScriptableObjects/RandomNumberGeneratorSeed/RandomSeed")]
public class RandomSeed : RandomNumberGeneratorSeed
{
    public override int GetSeed()
    {
        return (int)System.DateTime.Now.Ticks; //Current date and time in ticks, where 10000 ticks == 1 millisecond. This conversion trims higher bits.
    }
}
