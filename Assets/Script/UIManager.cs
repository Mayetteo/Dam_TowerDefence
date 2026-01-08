using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI playerHPText;

    public void UpdatePlayerHP(int playerHP)
    {
        playerHPText.text = playerHP.ToString();
    }
}
