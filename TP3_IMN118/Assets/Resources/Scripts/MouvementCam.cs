using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouvementCam : MonoBehaviour
{
    const float Threashold = 0.0005f;
    const float Snap = 360f;

    Transform Camera { get; set; }
    Transform Balle { get; set; }

    [SerializeField]
    float vitesseMvt = 10f;

    Vector3 ForceRes;
    Vector3 PositionPrecedente;
    Vector3 Direction;
    Vector3 DirectionPréc;
    // Start is called before the first frame update
    void Start()
    {
        Camera = FindObjectsOfType<Camera>().FirstOrDefault(c => c.name.StartsWith("Main")).transform;
        Balle = this.transform;
        Direction = Vector3.forward;
        DirectionPréc = Direction;
        PositionPrecedente.Set(Balle.position.x, 0, Balle.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //Direction = new Vector3((Balle.position.x - PositionPrecedente.x),
        //                        0,
        //                        (Balle.position.z - PositionPrecedente.z));

        Direction = new Vector3(Balle.GetComponentInChildren<Rigidbody>().velocity.x, 0, Balle.GetComponentInChildren<Rigidbody>().velocity.z);

        if (Direction.magnitude > Threashold || (Balle.position - PositionPrecedente).magnitude > Threashold*5)
        {
            GérerRotation();
            GérerPosition();
            //GérerMouvementBalle();
        }
        DirectionPréc = Direction;
    }

   
    private void GérerRotation()
    {
        if(Vector3.Angle(DirectionPréc, Direction) >= Threashold/10)
        {
            Camera.rotation = Quaternion.Euler(15, -1*Vector3.SignedAngle(Direction.normalized, Vector3.forward, Vector3.up), 0);
        }
          
    }

    private void GérerPosition()
    {
        if (Direction.magnitude > Threashold)
        {

            Camera.position = (-5 * Direction.normalized + 2 * Vector3.up + Balle.position);//Vector3.Slerp(Balle.position + 2*DirectionPréc.normalized, Balle.position + 2*Direction.normalized, 0.75f) + Vector3.up*2;
            PositionPrecedente.Set(Balle.position.x, 0, Balle.position.z);

            Camera.gameObject.GetComponentInChildren<Camera>().fieldOfView = 60.0f + 10*((float)Math.Pow(Direction.magnitude-DirectionPréc.magnitude, 2.0) / (DirectionPréc.magnitude+1)) +Direction.magnitude;
        }
    }
}
