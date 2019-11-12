using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saut : MonoBehaviour
{
    public bool toucheAuSol;
    private Rigidbody Balle { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Balle = this.transform.parent.gameObject.GetComponentInChildren<Rigidbody>();
        toucheAuSol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && toucheAuSol)
        {
            Balle.AddForce(Vector3.up *5, ForceMode.Impulse);
            Debug.Log("Space pressed");
        }
    }
    
    private void OnTriggerStay(Collider obj)
    {
        if (obj.transform != this.transform.parent)
        {
            toucheAuSol = true;
            Debug.Log(obj.name);
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.transform != this.transform.parent)
        {
            toucheAuSol = false;
            Debug.Log(obj.name);
        }
    }
}
