using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    //carga el menu
    public void PrincipalMenu()
    {
        SceneManager.LoadScene("UImenu");
    }
}
