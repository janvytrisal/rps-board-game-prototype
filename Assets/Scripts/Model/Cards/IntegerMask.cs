/*
 * Author: Jan Vytrisal
 */

using System;
using UnityEngine;

/// <summary>
/// Integer mask with cached binary string representation.
/// </summary>
[Serializable]
public class IntegerMask
{
    [SerializeField]
    private int _value;

    private string _cachedBinaryStringValue;
    private int _previousValue;

    public int Value { get => _value; }

    public override string ToString()
    {
        if ((Value != _previousValue) || string.IsNullOrEmpty(_cachedBinaryStringValue))
        {
            _cachedBinaryStringValue = Convert.ToString(Value, 2);
            _cachedBinaryStringValue = _cachedBinaryStringValue.PadLeft(32, '0');
            _previousValue = Value;
        }
        return _cachedBinaryStringValue;
    }

    public static int operator &(IntegerMask left, IntegerMask right)
    {
        if ((left == null) || (right == null))
            throw new ArgumentNullException();

        return left.Value & right.Value;
    }
}
