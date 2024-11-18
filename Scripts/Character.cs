using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public GameObject camera, interactPanel, victoryPanel, defeatPanel, booster1panel, booster2Panel, healthText;
    public Slider healthSlider;
    private int health = 10, chickenAmount;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int ChickenAmount
    {
        get => chickenAmount;
        set => chickenAmount = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Character = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (chickenAmount > 0)
            {
                health += 5;
                healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
                healthSlider.value = health;
                chickenAmount--;
                if(chickenAmount==0)
                    booster1panel.SetActive(false);
            }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Poo"))
        {
            health -= 3;
            healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
            healthSlider.value = health;
            Destroy(other.gameObject);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(GameObject.Find("GameManager"));
    }
}
