using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string difficulty;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentCar", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty(string dif)
    {
        difficulty = dif;

        PlayerPrefs.SetString("CurrentDifficult", difficulty);

        //LoadGame();
    }

    public void LoadGame()
    {

        
        SceneManager.LoadScene("Game");
    }

    public void SetCar(int id)
    {

        if (id == 0)
        {
            Debug.Log("selected car " + id);
            PlayerPrefs.SetInt("CurrentCar", 0);
        }
        else if (id == 1)
        {
            Debug.Log("selected car " + id);
            PlayerPrefs.SetInt("CurrentCar", 1);
        }
        else if (id == 2)
        {
            Debug.Log("selected car " + id);
            PlayerPrefs.SetInt("CurrentCar", 2);
        }

        
    }
}
