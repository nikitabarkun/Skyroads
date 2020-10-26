using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField]
    private const int placementStep = 10;

    [SerializeField]
    private const int startSpeed = 5;

    [SerializeField]
    private const float maxspeed = 0.4f;

    private Vector3 movementVector;

    [SerializeField]
    private RoadSpawner spawner;

    private float timeSinceStartup = 0f;

    public static int PlacementStep => placementStep;

    public void OnStart()
    {
        movementVector = Vector3.forward * startSpeed * Time.deltaTime * GameController.CurrentBoostSpeedMultiplier;

        transform.position = spawner.StartPos;

        timeSinceStartup = 0f;
    }

    private void ShiftRoad()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlacementStep);
    }

    private void Update()
    {
        timeSinceStartup += Time.deltaTime;

        if (movementVector.z <= maxspeed)
        {
            movementVector = Vector3.forward * startSpeed * Time.deltaTime * timeSinceStartup * GameController.CurrentBoostSpeedMultiplier;
        }

        transform.position -= movementVector;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Spaceship")
        {
            ShiftRoad();
        }
    }
}