using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Patrol, Stationary, Rotating }
    public EnemyType enemyType;

    public TextMeshProUGUI levelText;
    public int enemyLevel = 10;
    public Transform[] waypoints;
    public float waitTime = 3f;
    public float rotationSpeed = 180f;
    public float rotationWaitTime = 2f;
    private int currentWaypointIndex = 0;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        switch (enemyType)
        {
            case EnemyType.Patrol:
                StartCoroutine(Patrol());
                break;
            case EnemyType.Stationary:
                StartCoroutine(Stationary());
                break;
            case EnemyType.Rotating:
                StartCoroutine(Rotating());
                break;
        }
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

    IEnumerator Stationary()
    {
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator Rotating()
    {
        while (true)
        {
            // +180 derece dön
            yield return Rotate(180);
            // Bekle
            yield return new WaitForSeconds(rotationWaitTime);
            // -180 derece dön
            yield return Rotate(-180);
            // Bekle
            yield return new WaitForSeconds(rotationWaitTime);
        }
    }

    IEnumerator Rotate(float angle)
    {
        float rotated = 0;
        float targetRotation = Mathf.Abs(angle);
        float rotationDirection = Mathf.Sign(angle);

        while (rotated < targetRotation)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - rotated);
            transform.Rotate(0, rotationThisFrame * rotationDirection, 0);
            rotated += rotationThisFrame;
            yield return null;
        }
    }
}
