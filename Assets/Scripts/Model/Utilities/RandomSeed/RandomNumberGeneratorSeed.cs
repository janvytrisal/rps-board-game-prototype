/*
 * Author: Jan Vytrisal
 */

using UnityEngine;

/// <summary>
/// Seed for random number generator.
/// </summary>
public abstract class RandomNumberGeneratorSeed : ScriptableObject
{
    /// <summary>
    /// Returns seed according to the class setup.
    /// </summary>
    public abstract int GetSeed();
}
