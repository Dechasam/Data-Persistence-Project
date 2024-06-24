using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText; // Texte pour afficher le meilleur score

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private string playerName; // Variable pour stocker le nom du joueur actuel

    void Start()
    {
        // Récupérer le nom du joueur depuis GameManager1
        if (GameManager1.Instance != null)
        {
            playerName = GameManager1.Instance.playerName;
        }

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // Afficher le meilleur score actuel au démarrage
        if (GameManager1.Instance != null)
        {
            BestScoreText.text = $"Best Score: {GameManager1.Instance.bestScore} by {GameManager1.Instance.bestPlayerName}";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MenuScene"); // Retourner au menu principal
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        // Afficher le nom du joueur actuel et le score
        ScoreText.text = $"Name: {playerName} Score: {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Vérifier et sauvegarder le meilleur score
        if (GameManager1.Instance != null)
        {
            GameManager1.Instance.UpdateBestScore(m_Points); // Mettre à jour et sauvegarder le meilleur score

            // Mettre à jour l'affichage du meilleur score avec le nom du joueur ayant le meilleur score
            BestScoreText.text = $"Best Score: {GameManager1.Instance.bestScore} by {GameManager1.Instance.bestPlayerName}";
        }
    }
}
