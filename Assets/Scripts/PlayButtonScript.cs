using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textBomb;
    [SerializeField]
    public TMP_Text textBombButton;
    [SerializeField]
    public TMP_Text textEMP;
    [SerializeField]
    public TMP_Text textEMPButton;
    [SerializeField]
    public TMP_Text textMagnet;
    [SerializeField]
    public TMP_Text textMagnetButton;
    [SerializeField]
    public TMP_Text textShield;
    [SerializeField]
    public TMP_Text textShieldButton;
    [SerializeField]
    public TMP_Text textMining;
    [SerializeField]
    public TMP_Text textMiningButton;
    [SerializeField]
    public TMP_Text textXP;

    List<int> costsBomb = new List<int> {50, 150, 400, 750, 1500, 0};
    List<int> costsEmp = new List<int> {100, 250, 600, 1000, 1700, 0};
    List<int> costsMagnet = new List<int> {75, 200, 500, 800, 1500, 0};
    List<int> costsBase = new List<int> {10, 100, 300, 600, 1200, 0};
    List<int> costsMining = new List<int> {5, 50, 200, 800, 2000, 0};	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame()
    {
        Debug.Log("Play Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void GoBackToStartScreen()
    {
        Debug.Log("Go Back To Start Screen");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void GoToSettings()
    {
        Debug.Log("Go To Settings");
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void ResetGame()
    {
        textBomb.text = "Bomb Lvl: 0";
        textBombButton.text = "50 XP";
        textEMP.text = "EMP Lvl: 0";
        textEMPButton.text = "100 XP";
        textMagnet.text = "Magnet Lvl: 0";
        textMagnetButton.text = "75 XP";
        textShield.text = "Shield Lvl: 0";
        textShieldButton.text = "10 XP";
        textMining.text = "Mining Lvl: 0";
        textMiningButton.text = "5 XP";
        textXP.text = "6000";
    }
    public void IncreaseBomb()
    {
        int bombLevel = textBomb.text[textBomb.text.Length - 1] - '0';
        int xp = int.Parse(textXP.text);
        int bombCost = costsBomb[bombLevel];
        if (bombLevel < 6 && textBombButton.text != "MAX" && xp >= bombCost) {
            int nextBombCost = costsBomb[bombLevel + 1];
            textBomb.text = "Bomb Lvl: " + (bombLevel + 1);
            textBombButton.text = nextBombCost + " XP";
            xp -= bombCost;
            textXP.text = xp.ToString();
            if (nextBombCost == 0) {
                textBombButton.text = "MAX";
                return;
            }
        } 
    }

    public void IncreaseEMP()
    {
        int empLevel = textEMP.text[textEMP.text.Length - 1] - '0';
        int xp = int.Parse(textXP.text);
        int empCost = costsEmp[empLevel];
        if (empLevel < 6 && textEMPButton.text != "MAX" && xp >= empCost) {
            int nextEmpCost = costsEmp[empLevel + 1];
            textEMP.text = "EMP Lvl: " + (empLevel + 1);
            textEMPButton.text = nextEmpCost + " XP";
            xp -= empCost;
            textXP.text = xp.ToString();
            if (nextEmpCost == 0) {
                textEMPButton.text = "MAX";
                return;
            }
        } 
    }

    public void IncreaseMagnet()
    {
        int magnetLevel = textMagnet.text[textMagnet.text.Length - 1] - '0';
        int xp = int.Parse(textXP.text);
        int magnetCost = costsMagnet[magnetLevel];
        if (magnetLevel < 6 && textMagnetButton.text != "MAX" && xp >= magnetCost) {
            int nextMagnetCost = costsMagnet[magnetLevel + 1];
            textMagnet.text = "Magnet Lvl: " + (magnetLevel + 1);
            textMagnetButton.text = nextMagnetCost + " XP";
            xp -= magnetCost;
            textXP.text = xp.ToString();
            if (nextMagnetCost == 0) {
                textMagnetButton.text = "MAX";
                return;
            }
        }
    }

    public void IncreaseShield()
    {
        int shieldLevel = textShield.text[textShield.text.Length - 1] - '0';
        int xp = int.Parse(textXP.text);
        int shieldCost = costsBase[shieldLevel];
        if (shieldLevel < 6 && textShieldButton.text != "MAX" && xp >= shieldCost) {
            int nextShieldCost = costsBase[shieldLevel + 1];
            textShield.text = "Shield Lvl: " + (shieldLevel + 1);
            textShieldButton.text = nextShieldCost + " XP";
            xp -= shieldCost;
            textXP.text = xp.ToString();
            if (nextShieldCost == 0) {
                textShieldButton.text = "MAX";
                return;
            }
        }
    }

    public void IncreaseMining()
    {
        int miningLevel = textMining.text[textMining.text.Length - 1] - '0';
        int xp = int.Parse(textXP.text);
        int miningCost = costsMining[miningLevel];
        if (miningLevel < 6 && textMiningButton.text != "MAX" && xp >= miningCost) {
            int nextMiningCost = costsMining[miningLevel + 1];
            textMining.text = "Mining Lvl: " + (miningLevel + 1);
            textMiningButton.text = nextMiningCost + " XP";
            xp -= miningCost;
            textXP.text = xp.ToString();
            if (nextMiningCost == 0) {
                textMiningButton.text = "MAX";
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
