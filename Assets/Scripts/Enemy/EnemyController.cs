using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Threading;

public abstract class EnemyController : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public TextMeshProUGUI levelText;
    public int enemyLevel = 10;
    public LayerMask obstacleLayerMask;
    public float enemyVisionRange;
    public float angleOfVision;
    public int visionConeResolution = 60;

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    private Mesh visionConeMesh;
    private MeshFilter meshFilter;

    protected virtual void Start()
    {
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        meshFilter = GetComponent<MeshFilter>();
        visionConeMesh = new Mesh();
        angleOfVision *= Mathf.Deg2Rad;

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        InitializeEnemy();
    }

    protected abstract void InitializeEnemy();

    void Update()
    {
        ComputeFieldOfView();
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

    void Attack()
    {
        animator.SetTrigger("Attack");
        //Playeri öldür
    }
}