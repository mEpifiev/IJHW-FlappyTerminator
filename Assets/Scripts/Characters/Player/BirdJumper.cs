using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]
public class BirdJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _maxRotationZ;

    private Vector2 _startPosition;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    private float _rotationThreshold = 0.1f;
    private bool _isRotatingToMin = true;

    private Rigidbody2D _rigidbody2D;
    private InputReader _inputReader;

    private void OnValidate()
    {
        if(_minRotationZ > _maxRotationZ)
            _minRotationZ = _maxRotationZ - 1;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();

        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
    }

    private void OnEnable()
    {
        _inputReader.Jumped += Jump;
    }

    private void OnDisable()
    {
        _inputReader.Jumped += Jump;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_isRotatingToMin)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, _minRotation) < _rotationThreshold)
            {
                transform.rotation = _minRotation;
                _isRotatingToMin = false;
            }
        }
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.velocity = Vector3.zero;
    } 

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_speed, _jumpForce);
        transform.rotation = _maxRotation;
        _isRotatingToMin = true;
    }
}
