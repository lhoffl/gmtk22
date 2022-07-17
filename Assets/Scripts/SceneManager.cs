using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public static int CurrentLevel;
    public static SceneManager Instance { get; private set; }

    void Awake() {
        CurrentLevel = 1;
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void ChangeScene(int scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ChangeScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
    }
    
    public void LoadLevel(int level) {
        StartCoroutine(Wait(level));
    }
    IEnumerator Wait(int level) {
        yield return new WaitForSecondsRealtime(0.25f);
        ChangeScene("Level" + level);
    }
}
