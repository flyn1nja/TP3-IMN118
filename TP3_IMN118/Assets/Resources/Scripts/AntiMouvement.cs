using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiMouvement : MonoBehaviour
{
    int compteur;
    const int shakiness = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transform.parent.position + Vector3.down/3;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
