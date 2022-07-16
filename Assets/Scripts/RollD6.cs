using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollD6 : MonoBehaviour
{

    public int rollTime = 0;
    public float throwForce = 25f;
    [SerializeField] List<Sprite> faces;
    public int currentFace;
    private SpriteRenderer _spriteRenderer;
    private int _fastRollSpeed = 5;
    private int _slowRollSpeed = 25;
    private int _slowestRollSpeed = 50;
    private int _initalRollTime;

    public float _initalVelocity;
    public float _currentVelocity;
    private Rigidbody2D _rig;
    private bool _isRolling = true;
    public bool _isDoneRolling = false;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        _initalRollTime = rollTime;
        RollTheDice();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRolling){
            _currentVelocity = _rig.velocity.magnitude;
            if (_currentVelocity > 0)
            {
                rollTime++;
                if (_currentVelocity > (_initalVelocity * 0.50) && rollTime % _fastRollSpeed == 0) DrawNextFace();
                if (_currentVelocity > (_initalVelocity * 0.10) && _currentVelocity < (_initalVelocity * 0.50) && rollTime % _slowRollSpeed == 0) DrawNextFace();
                if (_currentVelocity < (_initalVelocity * 0.10) && rollTime % _slowestRollSpeed == 0) DrawNextFace();
            }

            if (_currentVelocity < 1) _rig.drag = 5f;
            
            if (_currentVelocity < 0.01f) _rig.drag = 25f;

            if(_currentVelocity == 0) _isRolling = false;
        }
        else {

            if (Input.GetKeyDown(KeyCode.Space)) RollTheDice();

            Vector3 targetPosition = new Vector3(0,0.46f,0);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (12.5f * Time.deltaTime));

            float angle = Mathf.Atan2(0 - transform.position.y, 1 - transform.position.x ) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500f * Time.deltaTime);

            if (Vectors3CloseEnough(transform.position , targetPosition, 0.5f)) _isDoneRolling = true;
        }
    }

    void RollTheDice()
    {
        _rig.velocity = new Vector2(0,0);
        _rig.drag = 0.5f;
        currentFace = Random.Range(1,6);
        transform.position = new Vector3(0, 0, 0);
        _rig.AddForce(RandomVector2() * throwForce, ForceMode2D.Impulse);
        _initalVelocity = _rig.velocity.magnitude;
        _isRolling = true;
        _isDoneRolling = false;
    }

    void DrawNextFace()
    {
        if (currentFace < 6) currentFace++;
        else currentFace = 1;
        
        _spriteRenderer.sprite = faces[currentFace - 1];
    }

    Vector2 RandomVector2()
    {
        return new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    private bool Vectors3CloseEnough(Vector3 vectorA, Vector3 vectorB, float closeEnough)
    {
        if(vectorA.x - vectorB.x < closeEnough &&
        vectorA.y - vectorB.y < closeEnough &&
        vectorA.z - vectorB.z < closeEnough) return true;

        return false;
    }

    public bool DoneRolling() => _isDoneRolling;
}
