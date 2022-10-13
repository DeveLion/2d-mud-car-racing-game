using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public TMP_Text countdownText, finishPosText;
    public GameObject StartPanel, GamePlayPanel,PausePanel, FinishPanel;

    public GameObject Player;

    [SerializeField] GameObject[] AICars;

    public int countDownTimer = 3;
    public bool wait = true;
    bool isStarted = false;
    bool isPause;

    [SerializeField] string currentDifficult;

    [SerializeField] GameObject[] PlayerCarPrefab;
    [SerializeField] GameObject[] AICarPrefab;
    [SerializeField] Transform PlayerSpwnPos;
    [SerializeField] Transform AIStartSpwnPos;


    public Cinemachine.CinemachineVirtualCamera c_VirtualCamera;




    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;
        GetDifficult();

        CreatePlayer();
        CreateAICars();
        

        isPause = false;

        c_VirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Pause();


        }
    }


    void CreatePlayer()
    {

        int pl_speed = 0;

        if (currentDifficult == "easy")
        {

            pl_speed = 25;
        }
        else if (currentDifficult == "medium")
        {

            pl_speed = 35;
        }
        else if (currentDifficult == "hard")
        {

            pl_speed = 45;
        }

        int id = PlayerPrefs.GetInt("CurrentCar");
        Debug.Log("car player " + id);
        GameObject go = Instantiate(PlayerCarPrefab[id], PlayerSpwnPos.position, Quaternion.identity);
        Player = go;

        Player.transform.GetChild(4).gameObject.layer = 17;

        c_VirtualCamera.Follow = Player.transform;

        Player.gameObject.GetComponent<CarControllerNew>().accelerationFactor = pl_speed;
        Player.gameObject.GetComponent<CarControllerNew>().maxSpeed = pl_speed;

    }

    void CreateAICars()
    {
        int n_ai = 3;
        int ai_dif = 0;
        int ai_speed = 0;

        if (currentDifficult == "easy")
        {
            n_ai = 3;
            ai_dif = 0;
            ai_speed = 20;
        }
        else if (currentDifficult == "medium")
        {
            n_ai = 5;
            ai_dif = 1;
            ai_speed = 30;
        }
        else if (currentDifficult == "hard")
        {
            n_ai = 10;
            ai_dif = 2;
            ai_speed =40;
        }

        AICars = new GameObject[n_ai];

        for (int i = 0; i < n_ai; i++)
        {
            GameObject go = Instantiate(AICarPrefab[ai_dif], AIStartSpwnPos.position, Quaternion.identity);
            AIStartSpwnPos.position = new Vector2(AIStartSpwnPos.position.x + 6f, AIStartSpwnPos.position.y);
            AICars[i] = go;
            SetGameLayerRecursive(go, i + 7);

            AICars[i].transform.GetChild(4).gameObject.layer = 17;
            AICars[i].transform.GetChild(4).gameObject.tag = "CP";

            //set game mode speed
            AICars[i].gameObject.GetComponent<CarControllerNew>().accelerationFactor = ai_speed;
            AICars[i].gameObject.GetComponent<CarControllerNew>().maxSpeed = ai_speed;

            if (i >= n_ai - 2)
            {
                AICars[i].gameObject.GetComponent<CarControllerNew>().accelerationFactor = ai_speed + 5;
                AICars[i].gameObject.GetComponent<CarControllerNew>().maxSpeed = ai_speed + 5;
                AICars[i].gameObject.GetComponent<CarInputHandle>().CanScaleAISpeed();
            }
        }


        //SetAIDifficult();


        PositionSystem.instance.Cars = new GameObject[n_ai + 1];

        PositionSystem.instance.Cars[0] = Player;

        PositionSystem.instance.SetCarsAtRuntime(AICars);

        
    }


    IEnumerator StartCountdown()
    {
        while (wait)
        {
            yield return new WaitForSeconds(1f);

            if (countDownTimer > 1)
            {
                countDownTimer -= 1;
                //Debug.Log(countDownTimer);
                countdownText.text = countDownTimer.ToString();
            }
            else
            {
                wait = false;
                StartPanel.SetActive(false);
                countdownText.gameObject.SetActive(false);
                isStarted = true;
                StopCoroutine(StartCountdown());

                StartRace();
            }
        }
    }

    void GetDifficult()
    {
        currentDifficult = PlayerPrefs.GetString("CurrentDifficult");
        //SetAIDifficult();
    }

    void SetAIDifficult()
    {
        foreach (GameObject ai in AICars)
        {
            //ai.GetComponent<CarController>().SetAIDifficult(currentDifficult);
            ai.GetComponent<CarController>().SetAIDifficult(currentDifficult);
        }
    }

    public void Pause()
    {
        if (!isPause && isStarted)
        {
            isPause = true;
            PausePanel.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void ClosePause()
    {


        if (isPause)
        {

            PausePanel.SetActive(false);
            isPause = false;
            Time.timeScale = 1;

        }

    }


    public void StartRace()
    {
        GamePlayPanel.SetActive(true);

        foreach (GameObject car in AICars)
        {
            //car.GetComponent<InputHandler>().enabled = true;
            //car.GetComponent<CarController>().enabled = true;
            car.GetComponent<CarInputHandle>().enabled = true;
            car.GetComponent<CarControllerNew>().enabled = true;
        }



        //Player.GetComponent<InputHandler>().enabled = true;
        //Player.GetComponent<CarController>().enabled = true;
        Player.GetComponent<CarInputHandle>().enabled = true;
        Player.GetComponent<CarControllerNew>().enabled = true;


       

        PositionSystem.instance.StartPositioning();


    }

    public void RaceFinished()
    {
        DisableAllCars();

        int pos = 4;
        pos = CheckRaceResults();


        if (pos == 1)
        {
            finishPosText.text = pos.ToString() + "ST";

        }
        else if (pos == 2)
        {
            finishPosText.text = pos.ToString() + "ND";

        }
        else if (pos == 3)
        {
            finishPosText.text = pos.ToString() + "RD";
        }
        else
        {
            finishPosText.text = pos.ToString() + "TH";
        }

        FinishPanel.SetActive(true);


    }


    int CheckRaceResults()
    {

        int pos = 4;
        pos = Player.transform.GetChild(5).GetComponent<CarCPManager>().CarPosition;

        return pos;
    }


    void DisableAllCars()
    {
        //Player.GetComponent<InputHandler>().canMove = false;
       // Player.GetComponent<CarController>().canMove = false;
        Player.GetComponent<CarInputHandle>().canMove = false;
        Player.GetComponent<CarControllerNew>().canMove = false;

        foreach (GameObject car in AICars)
        {
            //car.GetComponent<InputHandler>().canMove = false;
            //car.GetComponent<CarController>().canMove = false;
            car.GetComponent<CarInputHandle>().canMove = false;
            car.GetComponent<CarControllerNew>().canMove = false;
        }
    }


    public void ComeBackToMainScreen()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void SetGameLayerRecursive(GameObject _go, int _layer)
    {
        _go.layer = _layer;
        _go.tag = "AI";
        foreach (Transform child in _go.transform)
        {
            child.gameObject.layer = _layer;
            child.gameObject.tag = "AI";

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);

        }

       
    }



}