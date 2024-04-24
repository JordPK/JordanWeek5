using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRotationScript : MonoBehaviour
{
    public Transform pelvis;
    // Start is called before the first frame update
    void Start()
    {
        pelvis.rotation = Quaternion.Euler(90, 180, 270);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
