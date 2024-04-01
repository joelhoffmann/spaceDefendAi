using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int roundNumber;
    public int coinsPerRound;

    public int roundTimer = 10; // Zeit in Sekunden für eine Runde
    // Diese Methode wird einmal aufgerufen, wenn das Spiel startet
    private void Awake()
    {
        // Stellen Sie sicher, dass die CoinManager-Instanz erstellt wurde
        CoinManager.Instance.Init();
        //new RoundManager().Init();
        //EnemyManager.Instance.Init();
    }

    void Start()
    {
        StartNewRound();
        StartCoroutine(StartRoundTimer());
    }

    private IEnumerator StartRoundTimer()
    {
        while (roundTimer > 0)
        {
            yield return new WaitForSeconds(1);
            roundTimer--;
        }
        Debug.Log("Round " + roundNumber + " is over!");
        // Player.instance.ClearWalls(); 
        if (!AreEnemiesRemaining())
            StartNewRound();
    }

    private void StartNewRound()
    {
        Debug.Log("Starting Round " + roundNumber);
        // Hier kannst du die Logik implementieren, um den Spielern Coins zuzuweisen
        // Zum Beispiel könntest du auf die Spielerinstanz zugreifen und die Methode zum Erhalten von Coins aufrufen
        if (Player.instance != null)
        {
            roundTimer = 30 * roundNumber;
            //    Player.instance.ReceiveCoins(coinsPerRound * roundNumber);
            roundNumber++;
            GameObject shield = GameObject.FindGameObjectWithTag("Shield");
            shield.SetActive(true);
            Player.instance.shieldHealth = 100;
        }

    }

    private bool AreEnemiesRemaining()
    {
        // Suche nach allen GameObjects mit dem Tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Wenn keine Gegner gefunden wurden, gib false zurück
        if (enemies.Length == 0)
        {
            return false;
        }
        // Ansonsten gib true zurück
        return true;
    }

}