using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private const int smoothMoveMultiplier = 3;
    private const int movingZoneWidth = 8;

    private float leftEdgeX;
    private float rightEdgeX;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource idleAudioSource;
    [SerializeField]
    private AudioSource arrivalAudioSource;

    private Vector3 defaultPosition;

    private Transform parentTransform;

    public void OnStart()
    {
        parentTransform.position = defaultPosition;

        animator.Play("Arriving");
        animator.SetBool("isDamageTaken", false);
        idleAudioSource.Play();
        arrivalAudioSource.Play();
    }

    public void OnGameOver()
    {
        idleAudioSource.Stop();
    }

    private void MoveTo(Vector3 position)
    {
        parentTransform.position = Vector3.Lerp(parentTransform.position, position, Time.deltaTime * smoothMoveMultiplier);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        PauseCanvasController.gameSoundsAudioSources.Add(idleAudioSource);
        PauseCanvasController.gameSoundsAudioSources.Add(arrivalAudioSource);

        defaultPosition = transform.position;
        leftEdgeX = (defaultPosition.x - movingZoneWidth) / 2f;
        rightEdgeX = (defaultPosition.x + movingZoneWidth) / 2f;

        parentTransform = transform.parent;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isTurningLeft", false);
            animator.SetBool("isTurningRight", false);

            MoveTo(new Vector3(0, parentTransform.position.y, parentTransform.position.z));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isTurningLeft", true);
            animator.SetBool("isTurningRight", false);

            MoveTo(new Vector3(leftEdgeX, parentTransform.position.y, parentTransform.position.z));
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isTurningLeft", false);
            animator.SetBool("isTurningRight", true);

            MoveTo(new Vector3(rightEdgeX, parentTransform.position.y, parentTransform.position.z));
        }

        if (Input.GetAxis("Space") > 0 && !gameController.IsBoostedAlready)
        {
            gameController.Boost();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Asteroid"))
        {
            gameController.DamageTaken();
            animator.SetBool("isDamageTaken", true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name.Contains("Asteroid"))
        {
            animator.SetBool("isDamageTaken", false);
        }
    }
}
