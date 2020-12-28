using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogController : MonoBehaviour
{
    public Button acept;
    public Button cancel;
    public InputField name;

    void Awake()
    {
        acept.onClick.AddListener(SaveRecord);
        cancel.onClick.AddListener(CancelRecord);
    }

    void SaveRecord()
    {
        
        PlayerPrefs.SetString("name", name.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Scores");
        //launch scoresScene;
    }

    void CancelRecord()
    {
        PlayerPrefs.SetString("name", "none");
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }
}
