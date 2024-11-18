using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : MonoBehaviour
{
    public bool canInteract = false, ready = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        GameObject.Find("GameManager").GetComponent<GameManager>().panels.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && ready)
            GameObject.Find("GameManager").GetComponent<GameManager>().Character.GetComponent<Character>().interactPanel.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        canInteract = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().Character.GetComponent<Character>().interactPanel.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        canInteract = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().Character.GetComponent<Character>().interactPanel.SetActive(false);
    }
}
