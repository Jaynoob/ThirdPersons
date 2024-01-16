using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkeletonController : MonoBehaviour
{
    public Transform[] targetPoint;
    public int currentPoint;
    
    public NavMeshAgent agent;
    public Animator Animator;

    public float waitAtPoint = 2f;
    private float waitCounter;

    // Start is called before the first frame update
    void Start()
    {
        waitAtPoint = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetPoint[currentPoint].position);
        if (agent.remainingDistance <= .2f)
        {
            if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;
                Animator.SetBool("Run", false);

            }
               
            
            else
        { 
            currentPoint++;
            waitCounter = waitAtPoint;
            Animator.SetBool ("Run",true);
        }
        }
        
            if (currentPoint >= targetPoint.Length)
            {
                currentPoint = 0;
            }
            agent.SetDestination(targetPoint[currentPoint].position);
        }
        

        
    }

