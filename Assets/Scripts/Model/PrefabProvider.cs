using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPrefabProvider {
    GameObject GetPrefab(UiConnectorType type);
    void GetPrefabAsych(UiConnectorType type, UnityEngine.Events.UnityAction<GameObject> onLoadCallback);
}

public enum UiConnectorType {
   Steel_Ball,
   Rubber_Ball
}
public class PrefabProvider : MonoBehaviour, IPrefabProvider {
    public static IPrefabProvider instance;

    private void Awake() {
        if (instance != null)
            DestroyImmediate(this.gameObject);
        else
            instance = this;
    }


    [SerializeField]
    public PrefabDictionaryScriptableObject prefabDictionaryScriptableObject;

    public Dictionary<UiConnectorType, string> PrefabDictionary {
        get { return prefabDictionaryScriptableObject.GetPrefabDictionary(); }
        set { prefabDictionaryScriptableObject.SetPrefabDictionary(value); }
    }
    public string GetPrefabPath(UiConnectorType key) {
        if (prefabDictionaryScriptableObject != null) {
            Dictionary<UiConnectorType, string> prefabDictionary = prefabDictionaryScriptableObject.GetPrefabDictionary();
            if (prefabDictionary.TryGetValue(key, out string value)) {
                return value;
            }
        }
        Debug.LogError("Prefab dont exit at the location " + key);
        return null;
    }

    /// <summary>
    /// Returns a loaded object that you have to instantiate
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetPrefab(UiConnectorType type) {
        string path = GetPrefabPath(type);
        path = RemovePathPrefix(path);
        GameObject loadedObject = Resources.Load<GameObject>(path);
        return loadedObject;
    }

    /// <summary>
    /// calls a method with a loaded gameobject that you have to instatntiate
    /// </summary>
    /// <param name="type"></param>
    /// <param name="onLoadCallback"></param>
    public void GetPrefabAsych(UiConnectorType type, UnityEngine.Events.UnityAction<GameObject> onLoadCallback) {
        string path = GetPrefabPath(type);
        path = RemovePathPrefix(path);
        StartCoroutine(LoadAsych(path, onLoadCallback));
    }

    IEnumerator LoadAsych(string path, UnityEngine.Events.UnityAction<GameObject> onLoadCallback) {
        ResourceRequest request = Resources.LoadAsync<GameObject>(path);
        float timeout = 2;
        while(!request.isDone || timeout > 0) {
            yield return request;
            timeout -= Time.deltaTime;
        }
        if (request.isDone) {
            yield return request;
            GameObject createdObject = request.asset as GameObject;
            yield return request;
            onLoadCallback?.Invoke(createdObject);
        } else {
            Debug.LogError("Asset didnt load in time " + path);
        }
    }

    string RemovePathPrefix(string path) {
        int index = path.IndexOf("Resources/");
        if (index >= 0) {
            string substring = path.Substring(index + 10);
            int extensionIndex = substring.LastIndexOf(".prefab");
            if (extensionIndex >= 0) {
                return substring.Substring(0, extensionIndex);
            }
            return substring;
        }

        return path; // Return the original path if "Resources/" is not found
    }

    /// <summary>
    /// only call this in state changes
    /// </summary>
    public void ClearUp() {
        Resources.UnloadUnusedAssets();
    }
}
