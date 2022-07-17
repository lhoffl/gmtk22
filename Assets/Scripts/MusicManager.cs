using System.Collections;
using UnityEngine;
public class MusicManager : MonoBehaviour {

    [SerializeField]
    AudioClip _loop;
    AudioSource _source;

    bool _loopMusic = true;

    public static MusicManager Instance { get; private set; }
    
    void Start() {

        if (Instance == null)
            Instance = this;
        else {
            Destroy(this);
        }
        
        _source = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);

        _source.volume = 0.45f;
        StartMusic();
    }

    void Update() {
        if(_loopMusic)
            LoopMusic();
    }

    public void StartMusic() {
        _source.clip = _loop;
    }

    public void EndMusic() {
        _loopMusic = false;
        _source.Stop();
    }

    public void LoopMusic() {
        if(_source.isPlaying)
            return;
        
        _source.Play();
    }

    public IEnumerator ReduceVolumeForSeconds(float amount, float seconds) {
        _source.volume = amount;
        yield return new WaitForSeconds(seconds);
        _source.volume = 1.0f;
    }
}
