using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;
    public Text scoreText;
    public Image PositionMarker;
    public Text livesText;
    public Text deathText;
    private float offsetX, offsetY;

    public float warningBorder = 1f;
    private bool reset;

    private Transform player;

    void Start()
    {
        reset = false;
        player = GameObject.FindWithTag("Player").transform;
        singleton = this;
        offsetX = Screen.width / 2;
        offsetY = Screen.height / 2;
    }


    void Update()
    {
        if (!reset)
        {
            if (player.transform.position.y > Screen.height / 100 - warningBorder &&
                !GetComponentInChildren<PositionMarker>() &&
                player.GetComponent<Player>().GetVelocity().y > 0)
            {
                var positionMarker = Instantiate(PositionMarker, new Vector3(50 * player.transform.position.x + offsetX, offsetY - Screen.height / 3, 0),
                Quaternion.identity, this.transform).GetComponent<PositionMarker>();

                positionMarker.Initialize(0);
            }
            else if (player.transform.position.x < -Screen.width / 100 + warningBorder &&
                !GetComponentInChildren<PositionMarker>() &&
                player.GetComponent<Player>().GetVelocity().x < 0)
            {
                var positionMarker = Instantiate(PositionMarker, new Vector3(offsetX + Screen.width / 2.5f, 50 * player.transform.position.y + offsetY, 0),
                Quaternion.identity, this.transform).GetComponent<PositionMarker>();

                positionMarker.Initialize(90);
            }
            else if (player.transform.position.y < -Screen.height / 100 + warningBorder &&
                !GetComponentInChildren<PositionMarker>() &&
                player.GetComponent<Player>().GetVelocity().y < 0)
            {
                var positionMarker = Instantiate(PositionMarker, new Vector3(50 * player.transform.position.x + offsetX, offsetY + Screen.height / 3, 0),
                Quaternion.identity, this.transform).GetComponent<PositionMarker>();

                positionMarker.Initialize(180);
            }
            else if (player.transform.position.x > Screen.width / 100 - warningBorder &&
                !GetComponentInChildren<PositionMarker>() &&
                player.GetComponent<Player>().GetVelocity().x > 0)
            {
                var positionMarker = Instantiate(PositionMarker, new Vector3(offsetX - Screen.width / 2.5f, 50 * player.transform.position.y - offsetY, 0),
                Quaternion.identity, this.transform).GetComponent<PositionMarker>();

                positionMarker.Initialize(270);
            }
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;

        if(lives <= 0)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        deathText.text = "YOU DIED\n\n Score: " + player.GetComponent<Player>().GetScore();
        reset = true;
        Destroy(StarFactory.singleton.gameObject);
        Invoke("ResetScene", 2f);
    }
    
    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
