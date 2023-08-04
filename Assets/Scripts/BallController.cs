using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    bool launchingEnabled = false;
    Vector3 launchDirection;
    public float force = 100;
    public GameObject indicator;
    public GameObject directionPanel;
    bool launch = false;
    public int collapsePinFactor = 0;
    public int touchPinFactor = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        directionPanel.SetActive(false);
        SetUpGame.instance.initialPosOfBall = this.transform.position;
    }

    void Update()
    {
        if (!SetUpGame.instance._allowTouch)
            return;

        RepostionBall();

        if (Input.GetButton("Fire1") && !launchingEnabled)
        {
            DisplayDirectonindicator();

        }
        if (Input.GetButtonUp("Fire1") && launchingEnabled)
        {
            SetUpGame.instance._allowTouch = false;
            launch = true;
        }

    }

    void FixedUpdate()
    {
        if (!launch)
            return;

        LaunchBall();

    }
    void DisplayDirectonindicator()
    {
        directionPanel.SetActive(true);
        launchingEnabled = true;
    }
    void RepostionBall()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -0.4f)
        {
            Vector3 newPos = new(transform.position.x - 4, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, 2.0f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 0.4f)
        {
            Vector3 newPos = new(transform.position.x + 4, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, 2.0f * Time.deltaTime);
        }
    }

    void LaunchBall()
    {
        launchDirection = indicator.GetComponent<Transform>().position;
        launchDirection.y = 0.7f;
        Vector3 dir = launchDirection - GetComponent<Transform>().position;
        rb.AddForce(dir * force, ForceMode.Impulse);
        directionPanel.SetActive(false);
        launchingEnabled = false;
        launch = false;
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        obj.GetComponent<GameController>().CalculatePins();
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("pin"))
        {
            Debug.Log("touch" + collision.gameObject.name);
            AudioManager.instance.PinCollideAudio();
            LattestScore(0, 1);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            SetUpGame.instance.currentBall.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void LattestScore(int collides, int touch)
    {
        Debug.Log(collides + "touch" + touch);
        SetUpGame.instance.UpdateScore(collides, touch);
    }
}
