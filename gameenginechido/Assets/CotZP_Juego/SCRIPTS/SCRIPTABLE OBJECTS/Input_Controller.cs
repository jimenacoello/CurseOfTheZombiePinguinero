using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    public static Input_Controller Instance;

    [Header("Configuración de Inputs")]
    public InputSO actualInputConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ------------------------------
    // Metodos de Movimiento
    // ------------------------------
    public float HorizontalMovement()
    {
        return Input.GetAxis("Horizontal");
    }

    public float VerticalMovement()
    {
        return Input.GetAxis("Vertical");
    }

    // ------------------------------
    // Metodos de Acciones
    // ------------------------------
    public bool JumpInput()
    {
        return Input.GetKeyDown(actualInputConfig.jump);
    }

    public bool InteractInput()
    {
        return Input.GetKeyDown(actualInputConfig.interact);
    }

    public bool PauseInput()
    {
        return Input.GetKeyDown(actualInputConfig.pause);
    }

    public bool RunInput()
    {
        return Input.GetKey(KeyCode.LeftShift);

    }


    // ------------------------------
    // Mouse
    // ------------------------------
    public float MouseScroll()
    {
        return Input.mouseScrollDelta.y;
    }

    public Vector2 MousePos()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}
