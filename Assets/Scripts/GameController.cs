using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject[] pins;
    public List<Vector3> pinsLocation;
    public List<Vector3> pinsRotation;

    int counts = 0;
    public List<GameObject> dropedPins;

    public GameObject scoreListParent;
    public GameObject scoreListObj;

    public int totalRound = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("ball");
        for (int i = 0; i < pins.Length; i++)
        {
            pinsLocation.Add(pins[i].transform.localPosition);
            pinsRotation.Add(pins[i].transform.localRotation.eulerAngles);
        }
    }

    void ResetGame()
    {
        foreach (var item in dropedPins)
        {
            item.SetActive(false);
        }
        SetUpGame.instance.InitSceneSetupForGame();
        SetUpGame.instance.currentBall.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(ResetBall());
       
    }

    IEnumerator ResetBall()
    {
        SetUpGame.instance.currentBall.transform.position = SetUpGame.instance.initialPosOfBall;
        SetUpGame.instance.currentBall.GetComponent<Rigidbody>().isKinematic = false;
        yield return null;

    }

    public void CalculatePins()
    {
        StartCoroutine(WaitForPut());
    }
    IEnumerator WaitForPut()
    {
        counts = 0;
        bool faulMove = true;
        yield return new WaitForSeconds(3.0f);
        foreach (GameObject obj in pins)
        {
            if (obj.GetComponent<Transform>().localRotation.eulerAngles.x < 258f || obj.GetComponent<Transform>().localRotation.eulerAngles.x > 282f)
            {
                if (obj.activeInHierarchy)
                {
                    counts++;
                    dropedPins.Add(obj);
                    ToastManager.Instance.ShowTost("Congratulations "+DataManager.Instance.PlayerName);
                    faulMove = false;
                }
            }
        }
        if (faulMove)
        {
            ToastManager.Instance.ShowTost("Oops "+DataManager.Instance.PlayerName);
            AudioManager.instance.PlayFaulAudio();
        }
        LattestScore(counts, 0);
        CheckForGameStatus();
        yield return null;
    }
    public void CheckForGameStatus()
    {
        if (dropedPins.Count == pins.Length || SetUpGame.instance.playCount == 5)
        {
            if (dropedPins.Count == pins.Length)
            {
                ToastManager.Instance.ShowTost(DataManager.Instance.PlayerName+", You have won."); 
                AudioManager.instance.PlayWinnerAudio();       
            }else{
                ToastManager.Instance.ShowTost("Oops "+DataManager.Instance.PlayerName+",Try on next round");
            }
            GameFinished();
        }
        else
        {
            ResetGame();
        }
    }

    public void GameFinished()
    {
        totalRound++;
        SetUpGame.instance.currentBall.SetActive(false);
        SetUpGame.instance.ScoreList.SetActive(true);
        GameObject g = Instantiate(scoreListObj, scoreListParent.transform);
        g.SetActive(true);
        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SetUpGame.instance.setelctedMaterial;
        g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score: "+SetUpGame.instance.totalScore.ToString();
        g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Pin Dropped: "+dropedPins.Count.ToString();

    }

    public void LattestScore(int collides, int touch)
    {
        SetUpGame.instance.UpdateScore(collides, touch);
    }
    public void RestartGame()
    {
        if (totalRound >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
         DisplayNewRound(totalRound);

        SetUpGame.instance.ScoreList.SetActive(false);
        dropedPins.Clear();
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.GetComponent<Rigidbody>().isKinematic = true;
            pins[i].SetActive(true);
            pins[i].transform.localPosition = pinsLocation[i];
            pins[i].transform.localEulerAngles = new Vector3(pinsRotation[i].x, pinsRotation[i].y, pinsRotation[i].z);
            pins[i].transform.GetComponent<Rigidbody>().isKinematic = false;
        }
        SetUpGame.instance.playCount = 0;
        SetUpGame.instance.totalScore = 0;
        SetUpGame.instance.UpdateScore(0, 0);
        ResetGame();
    }
    void DisplayNewRound(int rounds = 0){
        rounds++;
        SetUpGame.instance.newRoundDispalyText.text = "Round: "+rounds.ToString() + "/5";
        SetUpGame.instance.newRoundDispalyText.gameObject.SetActive(true);
        StartCoroutine(DisableRoundtext());
    }
    IEnumerator DisableRoundtext(){
        yield return new WaitForSeconds(2.0f);
        SetUpGame.instance.newRoundDispalyText.gameObject.SetActive(false);
        yield return null;
    }
    

}
