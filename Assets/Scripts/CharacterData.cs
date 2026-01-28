using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Fighter/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    public int maxHealth = 100;
    public int attackDamage = 10;
    public float speed = 5f;
    public float jumpForce = 7f;

    // Pour plus tard : sprites, animations, ragdoll prefabs
    public GameObject characterPrefab;
}

