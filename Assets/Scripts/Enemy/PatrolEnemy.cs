using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Threading;

public class PatrolEnemy : EnemyController
{
    [Header("Patrol Attributes")]
    public Transform[] waypoints;
    public float waitTime = 3f;
    private int currentWaypointIndex = 0;

    protected override void InitializeEnemy()
    {
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (waypoints.Length > 0)
            {
                animator.SetBool("isRunning", true);
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
                yield return new WaitUntil(() => navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending);
                animator.SetBool("isRunning", false);
                yield return new WaitForSeconds(waitTime);
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                yield return null;
            }
        }
    }

    public override void Die()
    {
        navMeshAgent.SetDestination(transform.position);
        StopAllCoroutines();
        animator.Play("Death");
        StartCoroutine(DieAfterAnimation());
    }
}
