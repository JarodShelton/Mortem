using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] static string stats;

    //0 - menu/options
    //1 - game
    //2 - game over
    public TextMeshProUGUI scoreField;
    void Start()
    {
        float score = PlayerPrefs.GetFloat("score");
        if (score > 0)
        {
            scoreField.text = "Time Survived: " + score.ToString();
            PlayerPrefs.SetFloat("score", 0f);
            PlayerPrefs.Save();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
