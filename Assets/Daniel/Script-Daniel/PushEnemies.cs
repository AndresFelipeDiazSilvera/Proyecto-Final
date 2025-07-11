using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushEnemies : MonoBehaviour
{
    [SerializeField] float attackDuration = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Push(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Debug.Log("Se preciono el boton espacio para empujar a los enemigos");
            //TODO implementar logica para empujar a los enemigos
        }
    }

    public IEnumerator Attack()
    {
        //TODO implementar animacion
        yield return new WaitForSeconds(attackDuration);
    }
}
