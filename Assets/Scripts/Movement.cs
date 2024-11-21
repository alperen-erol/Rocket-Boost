using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStr = 10f;
    [SerializeField] float rotationStr = 10f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        ThrustHandle();
        RotationHandle();
    }

    private void ThrustHandle()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStr * Time.fixedDeltaTime);
        }

    }
    private void RotationHandle()
    {
        rb.freezeRotation = true;
        float rotationInput = rotation.ReadValue<float>() * -1;
        transform.Rotate(Vector3.forward * rotationInput * rotationStr * Time.fixedDeltaTime);
        rb.freezeRotation = false;

    }
}
