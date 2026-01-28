using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public int playerNumber;
    public int charIndex;
    public Playerselect menuManager; // référence au MenuSelect

    public void OnClickSelect()
    {
        menuManager.SelectCharacter(playerNumber, charIndex);
    }
}

