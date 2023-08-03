using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{

    public static ToastManager Instance;
    public Vector2 DestinationPos= new Vector2(0,300);

    const string defaultWarningText = "Something went wrong.";


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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowNoInternetToast();
        }
    }
    public void ShowNoInternetToast()
    {
        ShowTost("Please check Your Connection.");
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
    public void ShowTost(string data, float time = 5.0f , bool closeButton = true ,bool defaultText = false ){
        GameObject tost = Instantiate(tostObject, tostParent);
        tost.GetComponent<RectTransform>().anchoredPosition = DestinationPos;
        if (defaultText)
        {
            data = defaultWarningText;    
        }
        tost.GetComponentInChildren<Text>().text = data;
        tost.transform.GetChild(1).gameObject.SetActive(closeButton);
        Destroy(tost, time);
    }
}
