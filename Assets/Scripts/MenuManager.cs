using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string difficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDifficulty(string dif)
    {
        difficulty = dif;

        PlayerPrefs.SetString("CurrentDifficult", difficulty);

        LoadGame();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
