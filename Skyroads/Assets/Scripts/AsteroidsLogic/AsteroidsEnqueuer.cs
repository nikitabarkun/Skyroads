using UnityEngine;

public class AsteroidsEnqueuer : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AsteroidsSpawner asteroidsSpawner;

    private void Enqueue(GameObject asteroid)
    {
        gameController.AsteroidPassed();

        asteroidsSpawner.Enqueue(asteroid);
    }

    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SetActive(false);

        Enqueue(collider.gameObject);
    }
}
