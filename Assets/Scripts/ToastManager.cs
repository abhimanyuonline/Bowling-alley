using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{

    public static ToastManager Instance;
    Vector2 DestinationPos= new(0,300);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    public GameObject tostObject;
    public Transform tostParent;
    public void ShowTost(string data)
    {
        GameObject tost = Instantiate(tostObject, tostParent);
        tost.GetComponent<RectTransform>().anchoredPosition = DestinationPos;
        tost.GetComponentInChildren<Text>().text = data;
        tost.transform.GetChild(1).gameObject.SetActive(false);
        Destroy(tost, 2f);
    }
}
