using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<PlatformController> platforms = new List<PlatformController>();

    public int target;
    public int streak;
    public int value;

    public bool stop;

    int once = 0;

    public float time;

    public Text TextTarget;
    public Text TextChrono;
    public Text TextValue;
    public Text TextScore;

    // Start is called before the first frame update
    void Start()
    {
        streak = 0;
        time = 25f;
        value = 0;
        stop = false;

        target = Random.Range(3, 10) + 1;

        TextTarget.text = "Target: " + target.ToString();
        startPlatforms();
    }

    // Update is called once per frame
    void Update()
    {
        timeController();
        updateChrono();
        checkTarget();
        checkLose();
    }

    void startPlatforms()
    {
        int total;

        for(int i = 0; i < platforms.Count; ++i)
        {
            int number = Random.Range(0 - target, target + 1);
                
            platforms[i].valuePlatform = number;
            platforms[i].id = i;
        }

        EventLauncher.addStep += updatePlatform;
    }

    public void updatePlatform(int platform)
    {
        int preValue = platforms[platform].valuePlatform;
        
        value += preValue;

        platforms[platform].valuePlatform = Random.Range(0 - target, target + 1);

        TextValue.text = value.ToString();
    }

    void updateChrono()
    {
        int minutes = (int)time / 60;
        int seconds = (int)Mathf.Floor(time) % 60;
        TextChrono.text = string.Format("{0,2:00}:{1,2:00}", minutes, seconds);
    }

    void checkTarget()
    {
        if (value == target)
        {
            updateTarget();
        }
    }

    void updateTarget() {
        value = 0;
        //launch effect
        int aux = target;
        do
        {
            target = Random.Range(-20, 20) + 1;
        } while (target != aux);

        time += 25f;

        TextValue.text = value.ToString();
        TextTarget.text = "Target: " + target.ToString();
        TextScore.text = "Score: " + (streak * 100).ToString();
        streak += 1;
    }

    void checkLose()
    {
        if(time <= 0.1 && once == 0)
        {
            stopTime();

            SceneManager.LoadScene("Dialog", LoadSceneMode.Additive);

            PlayerPrefs.SetString("score", (streak * 100).ToString());
            PlayerPrefs.SetString("name", "Alexis");
            PlayerPrefs.Save();
            //SceneManager.LoadScene("Scores");
           
        }
        
    }

    void timeController()
    {
        if (!stop)
        {
            time -= 1 * Time.deltaTime;
           
        }
       // if (time == 0)
        //{
          //  SceneManager.LoadScene("Scores");
        //}

    }
    
    void stopTime()
    {
        stop = true;
    }
}
