using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField PlayerNameInput;

    // Méthode pour démarrer le jeu
    public void StartGame()
    {
        // Enregistre le nom du joueur dans GameManager1
        if (GameManager1.Instance != null)
        {
            GameManager1.Instance.playerName = PlayerNameInput.text;
        }

        // Charge la scène du jeu principal
        SceneManager.LoadScene("GameScene");
    }

    // Méthode pour quitter le jeu
    public void ExitGame()
    {
        // Si on est dans l'éditeur de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si on est dans une build (jeu standalone)
        Application.Quit();
#endif
    }
}
