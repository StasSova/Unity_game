using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform cameraAnchor;
    private Vector3 cameraOffset;
    private InputAction lookAction;
    private Vector3 cameraAngles;
    private float cameraSensitivityHorizontal = 4.0f;
    private float cameraSensitivityVertical = 2.0f;
    private float cameraMaxV = 35.0f;
    private float cameraMinV = -75.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraOffset = this.transform.position - cameraAnchor.position; ;
    }

    private void Update()
    {
        Vector2 lookValue = Time.deltaTime * lookAction.ReadValue<Vector2>();
        cameraAngles.x = Mathf.Clamp(
            cameraAngles.x - lookValue.y * cameraSensitivityVertical,
            cameraMinV, cameraMaxV);
        cameraAngles.y += lookValue.x * cameraSensitivityHorizontal;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.eulerAngles = cameraAngles;
        this.transform.position = cameraAnchor.position +
            Quaternion.Euler(cameraAngles) * cameraOffset;
    }
}
