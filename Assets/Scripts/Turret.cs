using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    public float damage = 10;
    public float range = 5;
    public float fireRate = 1;
    private float fireCountdown = 0;   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
