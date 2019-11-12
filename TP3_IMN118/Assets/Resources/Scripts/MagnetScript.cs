using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
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
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.attachedRigidbody == Balle)
        {

            other.attachedRigidbody.AddForce((Balle.transform.position - this.transform.position - new Vector3(0, this.transform.position.y -Balle.transform.position.y, 0))
                * forceLat + this.transform.up * forceHaut * (1 + Mathf.Pow(Balle.transform.position.y-this.transform.position.y, 2)), ForceMode.Force);
        }
    }
  
}
