using TMPro;
using UnityEngine;
public class LevelTextUI : MonoBehaviour {
    [SerializeField] TMP_Text _text;

    void Awake() {
        _text.text = "Level " + GameManager.Instance._level;
    }
}
