using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public double time { get; set; }
    public double bestTime { get; set; }
    bool timerParti { get; set; }
    GameObject Timer { get; set; }
    GameObject Vitesse { get; set; }
    GameObject BestTime { get; set; }
    float vitesse_;

    // Start is called before the first frame update
    void Start()
    {
        Timer = GameObject.Find("Timer");
        Vitesse = GameObject.Find("Vitesse");
        BestTime = GameObject.Find("BestTime");
        bestTime = int.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vitesse.GetComponentInChildren<Text>().text = "Vitesse : "
            + ((GameObject.FindGameObjectWithTag("Player")
            .GetComponent<MouvementBalle>().vitesseBalle) * 200).ToString("n2");
        if (timerParti)
        {
            Timer.GetComponentInChildren<Text>().text = "Temps : " + time.ToString("n2");
            time += Time.deltaTime;
        }

        /*
        vitesse_ = ((GameObject.FindGameObjectWithTag("Player")
            .GetComponent<MouvementBalle>().vitesseBalle) * 200);
        */


    }
    private void OnTriggerEnter(Collider other)
    {
        CommencerTimer();
    }

    private void CommencerTimer()
    {
        time = 0;
        timerParti = true;
    }
    public void ArrêterTimer()
    {
        timerParti = false;
        if (time < bestTime)
        {
            bestTime = time;
            Timer.GetComponentInChildren<Text>().text = "Meilleur temps battu! : " + bestTime.ToString("n2");
            BestTime.GetComponentInChildren<Text>().text = "Meilleur temps : " + bestTime.ToString("n2");
        }

    }
}
