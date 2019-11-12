using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementBalle : MonoBehaviour
{
    const float Threashold = 0.005f;
    const float VitesseDeSteering = 0.75f;
    [SerializeField]
    const float VitesseDAcceleration = .75f;
    const float VitesseDeFreinage= 1.5f;


    Vector3 ForceRes;
    private Transform Balle { get; set; }

    Vector3 PositionPrecedente;
    Vector3 Direction;
    Vector3 DirectionPréc;

    public float vitesseBalle
    {
        get
        {
            return this.Direction.magnitude;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Balle = this.transform;
        PositionPrecedente = Balle.position - Vector3.forward;
        ForceRes = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        Direction = new Vector3((Balle.position.x - PositionPrecedente.x), 0, (Balle.position.z - PositionPrecedente.z)).magnitude != 0
                    ? new Vector3((Balle.position.x - PositionPrecedente.x), 0, (Balle.position.z - PositionPrecedente.z)) : Direction;

        if (Input.GetKey(KeyCode.W))
            ForceRes = ForceRes + Direction.normalized * VitesseDAcceleration; // + Threashold/2 pour éviter de rester à 0

        if (Input.GetKey(KeyCode.S))
        {
            ForceRes -= VitesseDeFreinage * Vector3.LerpUnclamped(Direction.normalized, Vector3.zero, VitesseDeFreinage / (VitesseDeFreinage + 1));


            //if (Direction.magnitude > 0.5f)
            //    ForceRes -= Direction;
            //else
            //    Balle.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)10; //Freeze la position X et Z
            //Balle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }



        if (Input.GetKey(KeyCode.A))
            ForceRes += DonnerVecteurNormal(Direction.normalized) * VitesseDeSteering;

        if (Input.GetKey(KeyCode.D))
            ForceRes -= DonnerVecteurNormal(Direction.normalized) * VitesseDeSteering;

        ForceRes = (!ToucheAuSol() ? ForceRes * 0.25f : ForceRes);

        if (ForceRes.magnitude > 0 || (PositionPrecedente - Balle.position).magnitude > Threashold)
        {
            PositionPrecedente.Set(Balle.position.x, 0, Balle.position.z);
            Balle.GetComponentInChildren<Rigidbody>().AddForce(ForceRes * VitesseDAcceleration * Time.deltaTime * 1000);
        }


        ForceRes = Vector3.zero;

        Debug.DrawLine(Balle.position, Balle.position + Direction, Color.blue, 3f);

        //Pour quitter
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private Vector3 DonnerVecteurNormal(Vector3 vecteur)
    {
        return new Vector3(-vecteur.z, vecteur.y, vecteur.x);
    }

    bool ToucheAuSol()
    {
        return Balle.GetComponentInChildren<Saut>().toucheAuSol;
    }
}
