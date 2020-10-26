using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField]
    private int blocksCount = 10;

    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private GameObject roadParent;

    [SerializeField]
    private Vector3 startPos;

    public Vector3 StartPos { get => startPos; private set => startPos = value; }

    private void Awake()
    {
        StartPos = blockPrefab.transform.position;
    }

    private void Start()
    {
        float currentZ = StartPos.z;

        for (int i = 0; i < blocksCount; i++)
        {
            Instantiate(blockPrefab, new Vector3(blockPrefab.transform.position.x, blockPrefab.transform.position.y, currentZ),
                Quaternion.identity, roadParent.transform);

            currentZ += RoadController.PlacementStep;
        }
    }
}
