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
        isPause = false;
        GetDifficult();
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
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
        SetAIDifficult();
    }

    void SetAIDifficult()
    {
        foreach (GameObject ai in AICars)
        {
            ai.GetComponent<CarController>().SetAIDifficult(currentDifficult);
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!isPause && isStarted)
            {
                isPause = true;
                PausePanel.SetActive(true);
                Time.timeScale = 0;

            }


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
        Player.GetComponent<InputHandler>().enabled = true;
        Player.GetComponent<CarController>().enabled = true;

        foreach (GameObject car in AICars)
        {
            car.GetComponent<InputHandler>().enabled = true;
            car.GetComponent<CarController>().enabled = true;
            car.GetComponent<InputHandler>().canMove = true;
        }


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
        Player.GetComponent<InputHandler>().canMove = false;
        Player.GetComponent<CarController>().canMove = false;

        foreach (GameObject car in AICars)
        {
            car.GetComponent<InputHandler>().canMove = false;
            car.GetComponent<CarController>().canMove = false;
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



}