using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUpGame : MonoBehaviour
{
    public static SetUpGame instance;
    [SerializeField]
    GameObject[] gameSceneSetup;
    public int ballCount;
    public GameObject currentBall;
    public List<string> totalTypeBalls;
    public int metalCount = 3;
    public int rubberCount = 2;
    public Vector3 initialPosOfBall;
    public PhysicMaterial[] ballMaterials;
    public TMP_Text scoreText;

    public string setelctedMaterial;
    public TMP_Text setelctedMaterialDispalyText;
    public TMP_Text newRoundDispalyText;
    public TMP_Text bowlingCount;
    [HideInInspector]
    public int playCount = 0;
    public int maxPlayCount = 5;
    public Canvas gameCanvas;
    public GameObject ScoreList;
    public TextMeshProUGUI userName;

    public int totalScore = 0;

    public int collapsePinFactor = 0;
    public int touchPinFactor = 0;

    public bool _allowTouch;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        DestroyImmediate(this.gameObject);
    }
    void Start()
    {
        foreach (var item in gameSceneSetup)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < metalCount; i++)
        {
            totalTypeBalls.Add("Metal");
        }
        for (int i = 0; i < rubberCount; i++)
        {
            totalTypeBalls.Add("Rubber");
        }
    }
    public void InitSceneSetupForGame()
    {
        foreach (var item in gameSceneSetup)
        {
            item.gameObject.SetActive(true);
        }

        gameCanvas.gameObject.SetActive(true);

        if (playCount == 0)
        {
            int ranIndex = Random.Range(0, totalTypeBalls.Count);
            SelectBall(totalTypeBalls[ranIndex]);
            totalTypeBalls.RemoveAt(ranIndex);

        }
        currentBall.gameObject.SetActive(true);
        bowlingCount.text = "bowled: " + playCount.ToString() + "/"+maxPlayCount.ToString();
        playCount++;
        _allowTouch = true;

    }

    public void SelectBall(string type)
    {
        AudioManager.instance.NewballAudio();
        setelctedMaterial = type;
        setelctedMaterialDispalyText.text = setelctedMaterial;
        if (type == "Metal")
        {
            currentBall.transform.GetComponent<SphereCollider>().material = ballMaterials[0];
            currentBall.GetComponent<BallController>().collapsePinFactor = 10;
            currentBall.GetComponent<BallController>().touchPinFactor = 5;

        }
        else
        {
            currentBall.transform.GetComponent<SphereCollider>().material = ballMaterials[1];
            currentBall.GetComponent<BallController>().collapsePinFactor = 20;
            currentBall.GetComponent<BallController>().touchPinFactor = 15;
        }
        currentBall.gameObject.SetActive(true);
        SetPointFactor();

    }
    public void SetPointFactor()
    {
        collapsePinFactor = currentBall.GetComponent<BallController>().collapsePinFactor;
        touchPinFactor = currentBall.GetComponent<BallController>().touchPinFactor;
    }
    public void UpdateScore(int collapse, int touch)
    {
        totalScore += collapsePinFactor * collapse + touch * touchPinFactor;
        scoreText.text = "Score: " + totalScore.ToString();
    }


}
