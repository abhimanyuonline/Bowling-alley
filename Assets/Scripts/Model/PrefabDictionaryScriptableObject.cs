using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPrefabDictionary", menuName = "Custom/Prefab Dictionary")]
public class PrefabDictionaryScriptableObject : ScriptableObject {
    [SerializeField]
    private List<UiConnectorType> keys = new List<UiConnectorType>();

    [SerializeField]
    private List<string> values = new List<string>();

    public Dictionary<UiConnectorType, string> GetPrefabDictionary() {
        Dictionary<UiConnectorType, string> prefabDictionary = new Dictionary<UiConnectorType, string>();

        if (keys.Count == values.Count) {
            for (int i = 0; i < keys.Count; i++) {
                prefabDictionary[keys[i]] = values[i];
            }
        } else {
            Debug.LogError("Key and value lists have different lengths!");
        }

        return prefabDictionary;
    }

    public void SetPrefabDictionary(Dictionary<UiConnectorType, string> prefabDictionary) {
        keys.Clear();
        values.Clear();

        foreach (var kvp in prefabDictionary) {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }
}
