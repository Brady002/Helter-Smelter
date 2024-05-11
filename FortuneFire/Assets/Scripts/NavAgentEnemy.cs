using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentEnemy : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    private Transform goal1;
    private Transform goal2;
    private bool flip;

    public float cooldownTime = 5f; 
    public NavMeshAgent agent;
    private bool isCooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        Invoke("StartTracking", 30f);
        InvokeRepeating("SwapGoals", 30f, 10f);
    }

    void Update() {
        goal1 = p1.transform;
        goal2 = p2.transform;
    }

    void StartTracking() {
        isCooldown = false;
        StartCoroutine(MoveTowardsGoalWithCooldown());
    }

    void SwapGoals() {
        if (flip) {
            agent.destination = goal2.position;
            flip = false;
        } else {
            agent.destination = goal1.position;
            flip = true;
        }
    }

    IEnumerator MoveTowardsGoalWithCooldown()
    {
        while (true)
        {
            if (!isCooldown)
            {
                isCooldown = true;
                agent.isStopped = false;
                yield return new WaitForSeconds(3f);
                agent.isStopped = true;
                yield return new WaitForSeconds(cooldownTime);
                isCooldown = false;
            }
            yield return null;
        }
    }



}