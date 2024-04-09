using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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
                instance.StartCoroutine(instance.GetRequest());
            }
            return instance;
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        //   Debug.Log("Added " + amount + " exp. Total exp: " + exp);
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

    IEnumerator GetRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://42baiw2cuduacmidi63rqxpjaq0vmvbc.lambda-url.eu-north-1.on.aws/user/highscore?username=Max Muster"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received: " + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                ParseJSON(json);
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        } // The using block ensures www.Dispose() is called when this block is exited
    }

    void ParseJSON(string json)
    {
        // Parse the JSON response to extract the score value
        HighscoreResponse response = JsonUtility.FromJson<HighscoreResponse>(json);

        // Extract the score value
        int score = response.message[0].score;

        // Assign the score value to the exp variable
        exp = score;

        // Now you can use the exp variable as needed in your game
        Debug.Log("Exp: " + exp);
    }

    [System.Serializable]
    public class HighscoreResponse
    {
        public Message[] message;
        public string status;
    }

    [System.Serializable]
    public class Message
    {
        public string timestamp_created;
        public int score;
        public long sk;
    }
}
