using UnityEngine;
using UnityEngine.UI;
public class DashUI : MonoBehaviour {
    [SerializeField] Sprite[] _dashUI;

    Image _image;
    Dash _dash;

    bool _shouldUpdate;
    float timer;
    
    void Start() {
        _dash = PlayerController.Instance.GetComponent<Dash>();
        _image = GetComponent<Image>();
        if (_dash == null) return;
        _dash.OnDashStarted += UpdateUI;
        timer = _dash.Cooldown;
    }

    void Update() {
        if (!_shouldUpdate) return;
        
        float parts = _dash.Cooldown / _dashUI.Length;
        timer -= Time.deltaTime;
        
        if (timer < _dash.Cooldown && timer > _dash.Cooldown - parts)
            _image.sprite = _dashUI [6];
        else if (timer <= _dash.Cooldown - parts && timer > _dash.Cooldown - parts * 2)
            _image.sprite = _dashUI [5];
        else if (timer <= _dash.Cooldown - parts * 2 && timer > _dash.Cooldown - parts * 3)
            _image.sprite = _dashUI [4];
        else if (timer <= _dash.Cooldown - parts * 3 && timer > _dash.Cooldown - parts * 4)
            _image.sprite = _dashUI [3];
        else if (timer <= _dash.Cooldown - parts * 4 && timer > _dash.Cooldown - parts * 5)
            _image.sprite = _dashUI [2];
        else if (timer <= _dash.Cooldown - parts * 5 && timer > 0)
            _image.sprite = _dashUI [1];
        else {
            _image.sprite = _dashUI [0];
            _shouldUpdate = false;
            timer = _dash.Cooldown;
        }
    }

    void UpdateUI() {
        _shouldUpdate = true;
    }
    }
