using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coin;
    CoinCollision coinCol;
    
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
        coinCol = GetComponent<CoinCollision>();
        
        InvokeCoins();

        //InvokeRepeating("Spawn", spawnTime, spawnTime);    
    }
    void Update()
    {
        //if (coinCol.coinDestroyed)
        //{
            //InvokeOneCoin();
        //}
    }


    public void InvokeCoins()
    {

        for (int i = 0; i < spawnPoints.Length; i++)
            Instantiate(coin, spawnPoints[i].position, transform.rotation);
    }
    public void InvokeOneCoin()
    {
        //Instantiate(coin, transform.position, transform.rotation);
        //coinCol.coinReAppear = true;
    }

    
}
