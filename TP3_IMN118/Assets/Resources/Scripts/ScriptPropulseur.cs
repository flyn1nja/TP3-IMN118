using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPropulseur : MonoBehaviour
{
    [SerializeField]
    Rigidbody Balle;
    [SerializeField]
    float forceLat;
    [SerializeField]
    float forceHaut;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.collider.attachedRigidbody == Balle)
        {
            collision.collider.attachedRigidbody.AddForce((GameObject.FindGameObjectsWithTag("Player")[0].transform.position - this.transform.position) * forceLat + this.transform.up *forceHaut);
        }
    }
}
