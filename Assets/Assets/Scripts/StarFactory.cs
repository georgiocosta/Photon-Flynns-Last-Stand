using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFactory : MonoBehaviour
{
    public static StarFactory singleton;

    public int maxStars;
    public int starCount;

    public Transform prefabStar;
    private Transform player;

    void Start()
    {
        singleton = this;
        maxStars = 2;
        starCount = 0;
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("CreateStar", 1f, 1f);
    }

    private void CreateStar()
    {
        if(starCount < maxStars)
        {
            Instantiate(prefabStar, new Vector3((Screen.width / 100 + 8f) * RandomCorner(), (Screen.height / 100 + 8f) * RandomCorner(), 0f), Quaternion.identity, this.transform);
            starCount++;
        }
    }

    public void ResetStars()
    {
        foreach(Transform item in transform)
        {
            if(item != this.transform)
            {
                Destroy(item.gameObject);
            }
        }
        starCount = 0;
    }

    private int RandomCorner()
    {
        return Random.Range(0, 2) * 2 - 1;
    }
}
