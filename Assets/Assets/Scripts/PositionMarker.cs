using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMarker : MonoBehaviour
{
    private Transform player;
    private int orientation = 0;
    
    public float offsetX, offsetY;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offsetX = Screen.width / 2;
        offsetY = Screen.height / 2;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(orientation == 0)
        {
            transform.position = new Vector3(50 * player.transform.position.x + offsetX, offsetY - Screen.height / 3, 0);
            //if (player.transform.position.y > Screen.height / 100) 
            if (player.transform.position.y < Screen.height / 100 - UIManager.singleton.warningBorder)
                Destroy(gameObject);
        }
        else if (orientation == 90)
        {
            transform.position = new Vector3(offsetX + Screen.width / 2.5f, 50 * player.transform.position.y + offsetY, 0);
            if (player.transform.position.x > -Screen.width / 100 + UIManager.singleton.warningBorder)
                Destroy(gameObject);
        }
        else if (orientation == 180)
        {
            transform.position = new Vector3(50 * player.transform.position.x + offsetX, offsetY + Screen.height / 3, 0);
            if (player.transform.position.y > -Screen.height / 100 + UIManager.singleton.warningBorder)
                Destroy(gameObject);
        }
        else if (orientation == 270)
        {
            transform.position = new Vector3(offsetX - Screen.width / 2.5f, 50 * player.transform.position.y + offsetY, 0);
            if (player.transform.position.x < Screen.width / 100 - UIManager.singleton.warningBorder)
                    Destroy(gameObject);
        }
    }

    public void Initialize(int rotation)
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        orientation = rotation;
    }
}
