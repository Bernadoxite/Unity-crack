using UnityEngine;
using UnityEngine.SceneManagement;//pour le Loadscene

public class Playerselect : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    public int player1Choice = 0;//stocke index du perso choisi

    public int player2Choice = 0;

    public void SelectCharacter (int playerNumber, int charIndex) // les boutons du menu appellent cette fonction
    {
        if (playerNumber == 1)
        {
            player1Choice = charIndex;
        }

        else if (playerNumber == 2)
        {
            player2Choice = charIndex;
        }

        Debug.Log ("Joueur" + playerNumber + "a choisi" + playerPrefabs[charIndex].name);



    }


    public void StartGame() // fonction appellée par le bouton start
    {
        //PlayerPrefs.SetInt("Player1Selected", player1Choice); // les playerprefs seront lues par le start du game manager
        //PlayerPrefs.SetInt("Player2Selected", player2Choice);
        GameInitializer.Instance.player1Choice = player1Choice;
        GameInitializer.Instance.player2Choice = player2Choice;

        SceneManager.LoadScene("Fight");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Fight");
    }
}
