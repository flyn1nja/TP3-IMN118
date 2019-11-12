using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVictoire : MonoBehaviour
{
    [SerializeField]
    Rigidbody Balle;
    [SerializeField]
    GameObject ZoneDébut;

    bool attendFinParticules = false;
    float temps;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(attendFinParticules)
        {
            temps += Time.deltaTime;
            if (temps >= 5)
            {
                this.GetComponentInChildren<ParticleSystem>().Clear();
                this.GetComponentInChildren<ParticleSystem>().Stop();
                temps = 0;
                attendFinParticules = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (attendFinParticules)
            return;

        Debug.Log(other.transform.name);
        if (other.attachedRigidbody == Balle)
        {
            attendFinParticules = true;
            this.GetComponentInChildren<ParticleSystem>().Play();
            Debug.Log("Trajet réussi en " + temps.ToString() + "secondes");
            ZoneDébut.GetComponentInChildren<TimerScript>().ArrêterTimer();
        }
    }

}
