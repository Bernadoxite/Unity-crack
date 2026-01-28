using System.Collections; // Coroutines
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerHealth player1; //gestion PV et KO, script playerhealth
    public PlayerHealth player2;

    public Transform player1Spawn; // pt de spawn des joueurs
    public Transform player2Spawn;

    public int roundsToWin = 2; // best of 3

    private int player1Rounds = 0; //sore interne
    private int player2Rounds = 0;

    public bool isRoundActive = false; //plus de dégats possibles a la fin d'un round, pas de double ko possible

    public TextMeshProUGUI roundText; //winner et fin du jeu


    //public GameObject[] playerPrefabs; //tableau des préfabs, avec index de 0 à 5

    //private int player1Choice = 0; //stock du choix de perso par index
    //private int player2Choice = 0;

    void Start()
    {

        //player1Choice = GameInitializer.Instance.player1Choice;
        //player2Choice = GameInitializer.Instance.player2Choice;

        roundsToWin = GameInitializer.Instance.roundsToWin; //copie locale du roundstowin du gameinitializer, utilisée dans NextRound

        SpawnPlayers();

        StartRound();
    }

    void StartRound()
    {
        isRoundActive = true;

        roundText.gameObject.SetActive(false);

        ResetPlayer(player1, player1Spawn.position);
        ResetPlayer(player2, player2Spawn.position);


    }

    void EndRound(PlayerHealth loser)  
    {
        isRoundActive = false;

        roundText.gameObject.SetActive(true);

        if (loser == player1)
        {
            player2Rounds++;
            roundText.text = "PLAYER 2 is the best !!!";
        }
        else
        {
            player1Rounds++;
            roundText.text = "PLAYER 1 is the best !!!";
        }

        StartCoroutine(NextRound());

    }

    IEnumerator NextRound()
    {
        isRoundActive = false;
        yield return new WaitForSeconds(2f);

        if (player1Rounds >= roundsToWin || player2Rounds >= roundsToWin)
        {
            roundText.gameObject.SetActive(true);
            roundText.text = "CEFINI";
        }
        else
        {
            StartRound();
        }
    }



    //public void SelectCharacter(int playerNumber, int charIndex) // qui choisit quoi
    //{
    //    if (playerNumber == 1)
    //    {
    //        player1Choice = charIndex;
    //    }

    //    else if (playerNumber == 2)
    //    {
    //        player2Choice = charIndex;
    //    }

    //    Debug.Log("Joueur" + playerNumber + "a choisi" + playerPrefabs[charIndex].name);


    //}
   

    void SpawnPlayers()
    {
        
            // PLAYER 1
            CharacterData c1 = GameInitializer.Instance.characterDatabase[GameInitializer.Instance.player1Choice];

            GameObject p1 = Instantiate(c1.characterPrefab, player1Spawn.position, Quaternion.identity);
            player1 = p1.GetComponent<PlayerHealth>();

            p1.GetComponent<PlayerController>().gameManager = this;
            p1.GetComponent<PlayerCombat>().gameManager = this;

            PlayerInput input1 = p1.GetComponent<PlayerInput>();
            input1.SwitchCurrentActionMap("Player1");

            // PLAYER 2
            CharacterData c2 = GameInitializer.Instance.characterDatabase[GameInitializer.Instance.player2Choice];

            GameObject p2 = Instantiate(c2.characterPrefab, player2Spawn.position, Quaternion.identity);
            player2 = p2.GetComponent<PlayerHealth>();

            p2.GetComponent<PlayerController>().gameManager = this;
            p2.GetComponent<PlayerCombat>().gameManager = this;

            PlayerInput input2 = p2.GetComponent<PlayerInput>();
            input2.SwitchCurrentActionMap("Player2");

            // EVENTS
            player1.OnKO += OnPlayerKO;
            player2.OnKO += OnPlayerKO;

            // CAMERA
            PlayerCamera cam = FindFirstObjectByType<PlayerCamera>();
            cam.player1 = p1.transform;
            cam.player2 = p2.transform;
        

    }


    void ResetPlayer(PlayerHealth player, Vector3 spawnPos) //reset du joueur
    {
        player.ResetHealth();
        player.transform.position = spawnPos;
    }

    void OnPlayerKO(PlayerHealth loser) //  Le player qui a déclenhé OnplayerKO est déclaré le loser
    {
        if (!isRoundActive) return; //si round deja fini, pas de ko possible -> sécurité

        //isRoundActive = false; //pour bloquer tout mouvement (y comris jump)

        EndRound(loser);
  
    }

 
    
}

