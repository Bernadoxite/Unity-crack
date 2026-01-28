using UnityEngine;

// GameInitializer : source de vérité pour le jeu
public class GameInitializer : MonoBehaviour
{
    // Singleton : une seule instance active à la fois
    public static GameInitializer Instance;

    // Choix des personnages par index (0 à 5)
    public int player1Choice = 0;
    public int player2Choice = 0;

    // Nombre de rounds à gagner (best of 3 = 2)
    public int roundsToWin = 2;

    public CharacterData[] characterDatabase; // liste de tous les persos du jeu


    // On peut stocker le score actuel si besoin
    [HideInInspector] public int player1Rounds = 0; // a mettre dans le game manager?
    [HideInInspector] public int player2Rounds = 0;

    private void Awake()
    {
        // Singleton pattern : on garde seulement 1 GameInitializer
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // détruit doublon
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // reste entre les scènes
    }
}
