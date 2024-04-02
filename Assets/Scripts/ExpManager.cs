using TMPro;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    private static ExpManager instance;

    private int exp = 1000;    

    // �ffentliche Eigenschaft, um auf die M�nzen zuzugreifen
    public int Exp { get { return exp; } }

    // Initialisierungsmethode
    public void Init()
    {
        refreshDisplay();
    }

    // Singleton-Instanz
    public static ExpManager Instance
    {
        get
        {
            GameObject expManagerObject = GameObject.Find("ExpManager");
            if (instance == null)
            {
                instance = expManagerObject.AddComponent<ExpManager>();
            }
            return instance;
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        Debug.Log("Added " + amount + " exp. Total exp: " + exp);
        refreshDisplay();
    }

    public bool SubtractExp(int amount)
    {
        if (exp >= amount)
        {
            exp -= amount;
            Debug.Log("Subtracted " + amount + " exp. Total exp: " + exp);
            refreshDisplay();
            return true;
        }
        else
        {
            Debug.Log("Not enough exp to subtract " + amount + " exp.");
            return false;
        }
    }

    public int GetExp()
    {
        return exp;
    }

    private void refreshDisplay()
    {
        GameObject.Find("ExpDisplay").GetComponent<TextMeshProUGUI>().text = "Stardust: " + exp.ToString();

    }
}
