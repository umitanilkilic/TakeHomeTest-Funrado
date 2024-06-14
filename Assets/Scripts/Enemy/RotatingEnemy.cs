using System.Collections;
using UnityEngine;

public class RotatingEnemy : EnemyController
{
    [Header("Rotating Attributes")]
    public float rotationSpeed = 180f;
    public float rotationWaitTime = 2f;

    protected override void InitializeEnemy()
    {
        StartCoroutine(Rotating());
    }

    IEnumerator Rotating()
    {
        while (true)
        {
            yield return Rotate(180);
            yield return new WaitForSeconds(rotationWaitTime);
            yield return Rotate(-180);
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