using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBooster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        transform.position = new Vector3(transform.position.x, .8f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * 10f * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().ChickenAmount++;
            other.GetComponent<Character>().booster1panel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
