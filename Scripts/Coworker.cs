using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Coworker : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private bool following = false, waving = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] birdArea = Physics.OverlapSphere(transform.position, 3f, 1 << 6);
        
        if (birdArea.Length>0)
        {
            Debug.Log("BIRD IN DISTANCE");
            agent.ResetPath();
            if (!waving)
            {
                Debug.Log("WAVE");
                waving = true;
                animator.SetTrigger("Wave");
                birdArea[0].GetComponent<Bird>().enabled = false;
                birdArea[0].GetComponent<Animator>().SetTrigger("Leave");
                this.enabled = false;
            }
        }
        
        Collider[] area = Physics.OverlapSphere(transform.position, 3, 1 << 3);
        
        if (area.Length>0 && Vector3.Distance(transform.position,
                GameObject.Find("GameManager").GetComponent<GameManager>().Character.transform.position) > 1f)
        {
            agent.SetDestination(GameObject.Find("GameManager").GetComponent<GameManager>().Character.transform.position);
            following = true;
        }
        else if (following && Vector3.Distance(transform.position,
                GameObject.Find("GameManager").GetComponent<GameManager>().Character.transform.position) <= 1f)
        {
            agent.ResetPath();
            following = false;
        }
        if (following && !waving && !animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            animator.SetTrigger("Walk");
        }
        else if(!following && !waving  && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Idle");
        }
        else if (waving && !animator.GetCurrentAnimatorStateInfo(0).IsName("Waving"))
        {
            animator.SetTrigger("Wave");
        }
    }
    
}
