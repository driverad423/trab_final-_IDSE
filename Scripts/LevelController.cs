using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<PlatformController> platforms = new List<PlatformController>();

    private AudioSource audio;

    public int maxTarget;
    public int minTarget;

    private int target;
    private int streak;
    private int value;

    public bool stop;

    int once = 0;

    public float time;

    public Text TextTarget;
    public Text TextChrono;
    public Text TextValue;
    public Text TextScore;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam.clearFlags = CameraClearFlags.SolidColor;

        audio = GetComponent<AudioSource>();

        streak = 0;
        time = 30f;
        value = 0;
        stop = false;

        maxTarget = 10;
        minTarget = -8;

        do
        {
            target = Random.Range(minTarget, maxTarget);
        } while (target == 0);

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
            int number = Random.Range(minTarget, maxTarget);
                
            platforms[i].valuePlatform = number;
            platforms[i].id = i;
        }

        EventLauncher.addStep += updatePlatform;
    }

    public void updatePlatform(int platform)
    {
        int preValue = platforms[platform].valuePlatform;
        
        value += preValue;

        platforms[platform].valuePlatform = Random.Range(minTarget, maxTarget);

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

        audio.Play();

        value = 0;
        streak += 1;
        time += 25f;
        maxTarget = maxTarget + ((int)Mathf.Floor(streak * 1.3f));
        minTarget = minTarget - ((int)Mathf.Floor(streak * 1.3f));

        //launch effect

        do
        {
            target = Random.Range(minTarget, maxTarget);
        } while (target == 0);


        TextValue.text = value.ToString();
        TextTarget.text = "Target: " + target.ToString();
        TextScore.text = "Score: " + (streak * 100).ToString();
    }

    void checkLose()
    {
        if(time <= 0.1 && once == 0)
        {
            stopTime();

            SceneManager.LoadScene("Dialog");

            PlayerPrefs.SetString("score", (streak * 100).ToString());
            PlayerPrefs.SetString("name", "Alexis");
            PlayerPrefs.Save();
        }
    }

    void timeController()
    {
        if (!stop)
        {
            time -= 1 * Time.deltaTime;
        }
        if(time <= 15)
        {
            
            float f = Mathf.PingPong(Time.time, 3.0f) / 3.0f;
            cam.backgroundColor = Color.Lerp(new Color(0.9f, 0.3f, 0.3f, 0.7f), new Color(0.9f, 0.3f, 0.3f, 0.1f), f);
            //TextChrono.color = new Color(212, 0, 0);
        }else
        {
            cam.backgroundColor = Color.white;
        }
    }
    
    void stopTime()
    {
        stop = true;
    }
}
