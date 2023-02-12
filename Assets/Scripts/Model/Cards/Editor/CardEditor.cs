/*
 * Author: Jan Vytrisal
 */

using UnityEditor;

/// <summary>
/// Draws card mask and win mask with their binary representations.
/// </summary>
[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    private SerializedProperty _cardMask;
    private SerializedProperty _winMask;

    private void OnEnable()
    {
        _cardMask = serializedObject.FindProperty("_cardMask");
        _winMask = serializedObject.FindProperty("_winMask");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Card cardTarget = target as Card;

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject(cardTarget), GetType(), false); //Create a standard script line.

        EditorGUILayout.PropertyField(_cardMask);
        DrawLabel(cardTarget.CardMask);
        EditorGUILayout.PropertyField(_winMask);
        DrawLabel(cardTarget.WinMask);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLabel(IntegerMask mask)
    {
        EditorGUILayout.LabelField("Binary", mask.ToString());
    }
}
