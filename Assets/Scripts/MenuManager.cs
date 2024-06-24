using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField PlayerNameInput;

    // M�thode pour d�marrer le jeu
    public void StartGame()
    {
        // Enregistre le nom du joueur dans GameManager1
        if (GameManager1.Instance != null)
        {
            GameManager1.Instance.playerName = PlayerNameInput.text;
        }

        // Charge la sc�ne du jeu principal
        SceneManager.LoadScene("GameScene");
    }

    // M�thode pour quitter le jeu
    public void ExitGame()
    {
        // Si on est dans l'�diteur de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si on est dans une build (jeu standalone)
        Application.Quit();
#endif
    }
}
