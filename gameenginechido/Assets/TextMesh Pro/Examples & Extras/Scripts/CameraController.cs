using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class CameraController : MonoBehaviour
{
    [Header("Sensibilidad y Suavizado")]
    public float sensibilidad = 2f;
    public float smoothness = 5f; // Delay de suavizado en la c�mara

    [Header("�ngulos de C�mara")]
    public float maxVerticalAngle = 80f;
    public float minVerticalAngle = -80f;

    [Header("Referencias")]
    public Transform player;

    [Header("Cursor")]
    public bool customLockMode = true;
    public CursorLockMode lockMode = CursorLockMode.Locked;

    private Vector2 mouseScaledPos;
    private Vector2 smoothedCam;
    private Vector2 camPos;

    private void Awake()
    {
        if (player == null)
        {
            player = transform.parent; // La c�mara suele ir como hijo del player
        }
    }

    private void Start()
    {
        if (customLockMode)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Input del mouse escalado por sensibilidad
        mouseScaledPos = Vector2.Scale(Input_Controller.Instance.MousePos(), Vector2.one * sensibilidad);

        // Suavizado con interpolaci�n
        smoothedCam = Vector2.Lerp(smoothedCam, mouseScaledPos, 1f / smoothness);

        // Sumamos los valores suavizados al �ngulo acumulado
        camPos += smoothedCam;

        // Clamp del �ngulo vertical
        camPos.y = Mathf.Clamp(camPos.y, minVerticalAngle, maxVerticalAngle);

        // Rotaci�n vertical (c�mara local)
        transform.localRotation = Quaternion.AngleAxis(-camPos.y, Vector3.right);

        // Rotaci�n horizontal (player global)
        player.localRotation = Quaternion.AngleAxis(camPos.x, Vector3.up);
    }
}
