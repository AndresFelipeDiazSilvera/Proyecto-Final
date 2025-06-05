using UnityEngine;

public class PlayerMovementA : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 5f;
    
    private Rigidbody rb;
    
    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
        
        // Congelar la rotación para evitar que la cápsula se voltee
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        // Obtener input del teclado
        float inputX = Input.GetAxis("Horizontal"); // A y D
        float inputZ = Input.GetAxis("Vertical");   // W y S
        
        // Crear vector de movimiento
        Vector3 direccion = new Vector3(inputX, 0, inputZ);
        
        // Normalizar para evitar movimiento más rápido en diagonal
        direccion = direccion.normalized;
        
        // Aplicar movimiento
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }
}