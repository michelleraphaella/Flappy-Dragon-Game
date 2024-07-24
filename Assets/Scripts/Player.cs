using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    public AudioClip pointSound;
    public AudioClip jumpSound;
    public AudioClip gameOverSound;
   
    public GameObject pausePanel;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        GameState.Instance.currentState = GameState.States.Playing;
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * strength;
            Audio.instance.PlaySound(jumpSound);
        }

        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }*/

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        pausePanel.SetActive(value: GameState.Instance.currentState == GameState.States.Paused);
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
        {
            FindObjectOfType<GameManager>().GameOver();
            Audio.instance.PlaySound(gameOverSound);
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
            Audio.instance.PlaySound(pointSound);
        }
    }

    public void OnFire()
    {
        if (GameState.Instance.isPlaying())
        {
            Weapon weapon = GetComponent<Weapon>();

            if (weapon != null)
            {
                weapon.Attack(false);
            }
        }
    }

    public void OnPause()
    {
        switch (GameState.Instance.currentState)
        {
            case (GameState.States.Playing):
            {
                GameState.Instance.currentState = GameState.States.Paused;
                Time.timeScale = 0;
                break;
            }

            case (GameState.States.Paused):
            {
                GameState.Instance.currentState = GameState.States.Playing;
                Time.timeScale = 1;
                break;
            }
        }
    }
}
