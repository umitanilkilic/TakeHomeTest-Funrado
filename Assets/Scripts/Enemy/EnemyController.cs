using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Threading;

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

    public LayerMask obstacleLayerMask;
    public float enemyVisionRange;

    public float angleOfVision;

    public int visionConeResolution = 60;

    private int currentWaypointIndex = 0;
    private Animator animator;
    private NavMeshAgent navMeshAgent;


    private Mesh visionConeMesh;

    private MeshFilter meshFilter;

    void Start()
    {
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        meshFilter = GetComponent<MeshFilter>();
        visionConeMesh = new Mesh();
        angleOfVision *= Mathf.Deg2Rad;

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

    void Update()
    {
        ComputeFieldOfView();
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


    void ComputeFieldOfView()
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] vertices = new Vector3[visionConeResolution + 1];
        vertices[0] = Vector3.zero;

        float angleIncrement = angleOfVision / (visionConeResolution - 1);
        float currentAngle = -angleOfVision / 2;

        Vector3 adjustedPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        for (int i = 0; i < visionConeResolution; i++)
        {
            float sine = Mathf.Sin(currentAngle);
            float cosine = Mathf.Cos(currentAngle);

            Vector3 raycastDirection = (transform.forward * cosine) + (transform.right * sine);
            Vector3 verticalForward = (Vector3.forward * cosine) + (Vector3.right * sine);

            if (Physics.Raycast(adjustedPos, raycastDirection, out RaycastHit hit, enemyVisionRange, obstacleLayerMask))
            {
                vertices[i + 1] = verticalForward * hit.distance;

                if (hit.collider.CompareTag("Player"))
                {
                    Attack();
                }
            }
            else
            {
                vertices[i + 1] = verticalForward * enemyVisionRange;
            }
            currentAngle += angleIncrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices;
        visionConeMesh.triangles = triangles;
        meshFilter.mesh = visionConeMesh;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        //Playeri öldür
    }

}
