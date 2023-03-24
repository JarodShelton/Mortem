using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private string text1;
    public GameObject textBox;
    public float startTime = 60f;
    private float time;
    private float countdown;
    private int sec;
    private int ms;

    public AudioSource source;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        countdown = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        countdown -= Time.deltaTime;
        if (countdown>0)
        {
            sec = (int)(countdown % 60);
            ms = ((int)(countdown * 100) % 100);
            text1 = ((int)countdown / 60).ToString() + ":" + sec.ToString("00") + "." + ms.ToString();
            textBox.GetComponent<Text>().text = text1;
        }
        else
        {
            PlayerPrefs.SetFloat("score", time);
            PlayerPrefs.Save();
            Debug.Log("Time survived: "+ time + " seconds");
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("Menu");
            source.PlayOneShot(deathSound, 0.5f);
        }
    }

    public void AddTime(float inc)
    {
        countdown += inc;
    }
    public void MultiplyTime(float mult)
    {
        countdown*=mult;
    }
}
