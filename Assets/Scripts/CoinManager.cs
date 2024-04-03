using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private static CoinManager instance;

    private int coins = 1000;

    public int wallCost = 10; // make it private!

    // �ffentliche Eigenschaft, um auf die M�nzen zuzugreifen
    public int Coins { get { return coins; } }

    // Initialisierungsmethode
    public void Init()
    {
        refreshDisplay();
    }

    // Singleton-Instanz
    public static CoinManager Instance
    {
        get
        {
            GameObject coinManagerObject = GameObject.Find("CoinManager");
            if (instance == null)
            {
                instance = coinManagerObject.AddComponent<CoinManager>();
            }
            return instance;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
       // Debug.Log("Added " + amount + " coins. Total coins: " + coins);
        refreshDisplay();
    }

    public bool SubtractCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            Debug.Log("Subtracted " + amount + " coins. Total coins: " + coins);
            refreshDisplay();
            return true;
        }
        else
        {
            Debug.Log("Not enough coins to subtract " + amount + " coins.");
            return false;
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    private void refreshDisplay()
    {
        GameObject.Find("CoinDisplay").GetComponent<TextMeshProUGUI>().text = "Coins: " + coins.ToString();

    }
}
