using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCatPaw : MonoBehaviour
{
    public GameObject leg;
    private float[] _yBounds = {0.2f, 3.75f};
    private float[] _xBounds = {-3, -0.5f};
    // private SpriteRenderer _legSpriteRenderer;
    // private SpriteRenderer _shadowSpriteRenderer;
    private bool _shouldMove = true;
    public bool _isSmashing = false;
    public float _moveSpeed = 0.003f;
    private Transform _yOffsetTransform;
    public int pauseTimeBeforeSmash = 2;
    public int smashTime = 3;
    private int _smashCooldownFramesLowerBound = 1000;
    private int _smashCooldownFramesUpperBound = 3000;
    public int smashCooldown = 0;

    public bool _isGoingUp = false;
    public bool _isGoingLeft = false;

    public bool isRightPaw = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isRightPaw) {
            float[] tempBounds = {(_xBounds[1] * -1), (_xBounds[0] * -1)};
            _xBounds = tempBounds;
        }
        
        smashCooldown = Random.Range(_smashCooldownFramesLowerBound, _smashCooldownFramesUpperBound);
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldMove) Wander();
        if (!_isSmashing)
        {
            if (smashCooldown > 0) smashCooldown--;
            else Smash();
        }
        
    }

    void Wander()
    {
        if (transform.position.x - _xBounds[0] <= 0.1f) _isGoingLeft = false;
        if (_xBounds[1] - transform.position.x <= 0.1f) _isGoingLeft = true;
        if (transform.position.y - _yBounds[0] <= 0.1f) _isGoingUp = true;
        if (_yBounds[1] - transform.position.y <= 0.1f) _isGoingUp = false;

        transform.position = new Vector3(
            transform.position.x + (_isGoingLeft ? -_moveSpeed : _moveSpeed),
            transform.position.y + (_isGoingUp ? _moveSpeed : -_moveSpeed),
            0
        );
    }

    void Smash()
    {
        StartCoroutine(SmashCoroutine());
    }

    IEnumerator SmashCoroutine()
    {
        _shouldMove = false;
        _isSmashing = true;
        yield return new WaitForSecondsRealtime(pauseTimeBeforeSmash);
        CatLegActive(true);
        yield return new WaitForSecondsRealtime(smashTime);
        CatLegActive(false);
        smashCooldown = Random.Range(_smashCooldownFramesLowerBound, _smashCooldownFramesUpperBound);
        _shouldMove = true;
        _isSmashing = false;
        
    }

    void CatLegActive(bool isActive)
    {
        leg.SetActive(isActive);
    }
}
