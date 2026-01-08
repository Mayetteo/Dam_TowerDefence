using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public UIManager uiManager;  

    public int MaxHP = 10;
    int currentHP = 10;

    public void Awake()
    {
        currentHP = MaxHP;
        uiManager.UpdatePlayerHP(currentHP);
    }

    public void PlayerHit(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            //TODO GAME OVER
        }
        uiManager.UpdatePlayerHP(currentHP);
    }

}
