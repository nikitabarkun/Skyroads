using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private const float minAngle = -2f;
    private const float maxAngle = 2f;
    private const float speedMultiplier = 30f;

    private float rotationAngle = 0f;

    private Vector3 movementVector;

    private void Move()
    {
        movementVector = Vector3.back * GameController.CurrentBoostSpeedMultiplier * Time.deltaTime * speedMultiplier;
        transform.position += movementVector;
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotationAngle);
    }

    private void Awake()
    {
        rotationAngle = Random.Range(minAngle, maxAngle);
    }

    private void Update()
    {
        Move();
        Rotate();
    }

}
