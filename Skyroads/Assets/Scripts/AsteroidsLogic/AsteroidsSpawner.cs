using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField]
    private float currentSpawnChance = 1f;
    [SerializeField]
    private float defaultSpawnChance = 1f;
    [SerializeField]
    private float maxSpawnChance = 8f;

    [SerializeField]
    private GameObject asteroidPrefab;

    private List<GameObject> asteroids;
    private Queue<GameObject> asteroidsQueue;

    private int currentContacts = 0;
    private float firstSpawnBorder;
    private float secondSpawnBorder;

    public List<GameObject> Asteroids { get => asteroids; set => asteroids = value; }

    public void OnStart()
    {
        currentContacts = 0;
        currentSpawnChance = defaultSpawnChance;

        Asteroids = new List<GameObject>();
        asteroidsQueue = new Queue<GameObject>();
    }

    public void OnGameOver()
    {
        foreach (GameObject asteroid in Asteroids)
        {
            Destroy(asteroid);
        }
    }

    public void Enqueue(GameObject asteroid)
    {
        asteroid.SetActive(false);

        asteroidsQueue.Enqueue(asteroid);
    }

    private void TryDequeue(float xPos)
    {
        GameObject asteroid;

        if (asteroidsQueue.Count == 0)
        {
            asteroid = Instantiate(asteroidPrefab, new Vector3(xPos, transform.position.y, transform.position.z),
                Quaternion.identity);
            Asteroids.Add(asteroid);
        }
        else
        {
            asteroid = asteroidsQueue.Dequeue();
            asteroid.SetActive(true);
            asteroid.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
    }

    private void Awake()
    {
        float spawnMargin = 1f;
        firstSpawnBorder = transform.localScale.x / -2 + spawnMargin;
        secondSpawnBorder = transform.localScale.x / 2 - spawnMargin;
    }

    private void FixedUpdate()
    {
        if (currentSpawnChance < maxSpawnChance)
        {
            currentSpawnChance += Time.deltaTime / 10;
        }

        if (currentContacts <= 1)
        {
            float generatedResult = Random.Range(0f, 100f);
            if (generatedResult <= currentSpawnChance)
            {
                float randomPositionX = Random.Range(firstSpawnBorder, secondSpawnBorder);

                TryDequeue(randomPositionX);
            }
        }
    }

    private void OnTriggerEnter()
    {
        currentContacts++;
    }

    private void OnTriggerExit()
    {
        currentContacts--;
    }
}
