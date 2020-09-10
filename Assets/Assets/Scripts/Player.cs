using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : PhysicalObject
{
    public float bulletVelocity;
    public Transform bullet;

    private Camera mainCamera;
    private int score;
    private int lives;

    private AudioSource sfx;
    private BoxCollider col;
    private SkinnedMeshRenderer model;
    private ParticleSystem particles;

    float second = 0f;

    protected override void Start()
    {
        base.Start();
        lives = 2;
        mainCamera = Camera.main;
        sfx = GetComponent<AudioSource>();
        col = GetComponent<BoxCollider>();
        model = GetComponentInChildren<SkinnedMeshRenderer>();
        particles = GetComponent<ParticleSystem>();
        score = 0;
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MouseLook();
    }

    private void Update()
    {
        FireBullet();
        
        second += Time.deltaTime;
        if(second >= 1f)
        {
            SetScore();
            second = 0f;
        }
    }

    private void MouseLook()
    {
        float mouseX = Input.mousePosition.x - transform.position.x;
        float mouseY = Input.mousePosition.y - transform.position.y;

        Vector3 target = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 10f));

        transform.LookAt(target);
    }

    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particles.Play();
            rb.AddForce(transform.forward * -1 * bulletVelocity, ForceMode.Impulse);
            Instantiate(bullet, transform.localPosition + transform.forward, transform.rotation);
        }
    }

    private void SetScore()
    {
        score += 1;
        UIManager.singleton.SetScore(score);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Star"))
        {
            Die();
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    private void Die()
    {
        lives -= 1;
        UIManager.singleton.SendMessage("UpdateLives", lives);
        sfx.Play();
        col.enabled = false;
        model.enabled = false;
        
        if(lives > 0)
            Invoke("Respawn", 1f);
    }

    private void Respawn()
    {
        StarFactory.singleton.ResetStars();
        col.enabled = true;
        model.enabled = true;
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public int GetScore()
    {
        return score;
    }
}
