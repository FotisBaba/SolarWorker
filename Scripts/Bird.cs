using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Transform target;
    public GameObject poo;
    private Animator animator;
    private bool pooCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.up*2;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] area = Physics.OverlapSphere(transform.position, 3, 1 << 3);
        
        if (area.Length>0)
        {
            if(target==null)
                target = area[0].transform;
        }

        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            animator.SetTrigger("Fly");
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, transform.position.y, target.position.z), .03f);

            if (transform.position.x == target.position.x && transform.position.z == target.position.z)
            {
                if (!pooCooldown)
                {
                    Instantiate(poo, transform.position, new Quaternion(0f, 0f, 0f, 0f));
                    pooCooldown = true;
                    StartCoroutine(PooCooldown());
                }
            }
        }
    }

    private IEnumerator PooCooldown()
    {
        yield return new WaitForSeconds(4f);
        pooCooldown = false;
    }
}
