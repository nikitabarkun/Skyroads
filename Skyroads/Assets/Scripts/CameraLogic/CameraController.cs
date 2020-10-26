using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Animator animator;

    public void SetAnimatorState(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    private void Awake()
    {
        animator = camera.GetComponent<Animator>();
    }
}