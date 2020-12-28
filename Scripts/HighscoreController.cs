using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{

    private Transform containerT;
    private Transform template;
    private List<Transform> highscoresTransformList;
    public Button back;
    
    private void Awake()
    {
        back.onClick.AddListener(Menu);

        containerT = transform.Find("Container");
        template = containerT.Find("Entry");
        template.gameObject.SetActive(false);

        string some = "a";

        if(!(PlayerPrefs.GetString("name") == "none"))
        {
            AddHighScore(int.Parse(PlayerPrefs.GetString("score")), PlayerPrefs.GetString("name"));
            PlayerPrefs.SetString("name", "none");
            PlayerPrefs.Save();
        }

        highscoresTransformList = new List<Transform>();

        //load
        string jsonString = PlayerPrefs.GetString("highscores");
        HighScores highscores =  JsonUtility.FromJson<HighScores>(jsonString);

        //sort
        for(int i = 0; i < highscores.highscoresList.Count; ++i)
        {
            for(int j = i + 1; j < highscores.highscoresList.Count; ++j)
            {
                if(highscores.highscoresList[j].score > highscores.highscoresList[i].score)
                {
                    HighScore tmp = highscores.highscoresList[i];
                    highscores.highscoresList[i] = highscores.highscoresList[j];
                    highscores.highscoresList[j] = tmp;
                }
            }
        }
        

        foreach(HighScore h in highscores.highscoresList)
        {
            CreateEntry(h, containerT, highscoresTransformList);
        }
         

    }

    private void CreateEntry(HighScore scoreEntry, Transform container, List<Transform> transformList)
    {

        float templateHeight = 50f;
        
        Transform entryTransform = Instantiate(template, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("pos").GetComponent<Text>().text = (transformList.Count + 1).ToString();

        int score = scoreEntry.score;
        entryTransform.Find("score").GetComponent<Text>().text = score.ToString();


        entryTransform.Find("name").GetComponent<Text>().text = scoreEntry.name;

        transformList.Add(entryTransform);
    }

    private void AddHighScore(int score, string name)
    {
        HighScore h = new HighScore { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscores");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        highscores.highscoresList.Add(h);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscores", json);
        PlayerPrefs.Save();

    }

    private void ResetList()
    {
        string jsonString = PlayerPrefs.GetString("highscores");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        highscores.highscoresList.Clear();
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscores", json);
        PlayerPrefs.Save();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

public class HighScores
{
    public List<HighScore> highscoresList;
}

[System.Serializable]
public class HighScore
{
    public int score;
    public string name;
}
