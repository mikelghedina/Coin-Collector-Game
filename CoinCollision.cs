using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public int coinPoints = 10;
    MeshRenderer meshRender;
    SphereCollider sphereCol;
    public Light light1;
    public ParticleSystem particles;
    
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        sphereCol = GetComponent<SphereCollider>();
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            ScoreScript.score += coinPoints;
            StartCoroutine(EnableColndRend());
            
        }
        
    }
    IEnumerator EnableColndRend()
    {
        meshRender.enabled = false;
        sphereCol.enabled = false;
        particles.Stop();
        light1.enabled = false;
        yield return new WaitForSeconds(30);
        meshRender.enabled = true;
        sphereCol.enabled = true;
        particles.Play();
        light1.enabled = true;
    }
    
}
