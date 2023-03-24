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
    private float time;
    private float countdown;
    private int sec;
    private int ms;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        countdown = 15f;
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
            text1 = ((int)countdown/60).ToString()+":"+ String.Format("{0:00}", sec) + "." + String.Format("{0:00}", ms);
            textBox.GetComponent<Text>().text = text1;
        }
        else
        {
            Debug.Log("Time survived: "+time);
            SceneManager.LoadScene("TEST LEVEL", LoadSceneMode.Single);
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
