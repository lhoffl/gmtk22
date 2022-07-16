using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;
    // Start is called before the first frame update
    void Start()
    {
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
        if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (_spriteRenderer) _spriteRenderer.sprite = _sprites[1];
        
    }

    void OnMouseUp()
    {
        if (_spriteRenderer) _spriteRenderer.sprite = _sprites[0];
        //go to new scene
        _gameManager.GoToScene(false, "MainMenu");
    }
}
