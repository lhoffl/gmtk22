using UnityEngine;
using UnityEngine.UI;
public class PlayButton : MonoBehaviour {

    [SerializeField] Sprite[] _sprites;
    [SerializeField] float _frameLength = 0.1f;
    
    bool _startUpdate;

    float _currentTime;
    Image _image;
        
    int _currentIndex = 0;
    
    void Awake() {
        _image = GetComponent<Image>();
    }

    void Update() {
        if (!_startUpdate) return;
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0 && _currentIndex < _sprites.Length) {
            _image.sprite = _sprites [_currentIndex];
            _currentIndex++;
            _currentTime = _frameLength;
        }
    }

    public void StartGame() {
        GameManager.Instance.GoToScene(true, "Level1");
    }
    
    public void UpdateUI() {
        _startUpdate = true;
        _currentTime = _frameLength;
    }
}
