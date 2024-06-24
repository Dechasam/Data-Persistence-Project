using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;
    public string playerName;
    public int bestScore;
    public string bestPlayerName; // Nouveau : le nom du joueur ayant le meilleur score

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }

    [System.Serializable]
    class SaveData
    {
        public int BestScore;
        public string BestPlayerName; // Nouveau : inclure le nom du joueur ayant le meilleur score
    }

    // Méthode pour mettre à jour le meilleur score
    public void UpdateBestScore(int newScore)
    {
        if (newScore > bestScore)
        {
            bestScore = newScore;
            bestPlayerName = playerName; // Mettre à jour le nom du joueur associé au meilleur score
            SaveGameData(); // Sauvegarder les données si le meilleur score est mis à jour
        }
    }

    // Méthode de sauvegarde unifiée
    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.BestScore = bestScore;
        data.BestPlayerName = bestPlayerName; // Sauvegarder aussi le nom du meilleur joueur
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Méthode de chargement unifiée
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.BestScore;
            bestPlayerName = data.BestPlayerName; // Charger le nom du joueur ayant le meilleur score
        }
        else
        {
            // Initialiser les valeurs par défaut si le fichier n'existe pas
            bestScore = 0;
            bestPlayerName = "None";
        }
    }
}
