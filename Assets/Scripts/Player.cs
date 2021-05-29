using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // not an error


public class Player : MonoBehaviour
{
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _FireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _SpawnManager;

    [SerializeField]
    private bool _isTrippleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSoundClip;
    
  




    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        // take current position of player = new position(0, 0, 0)
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
       

        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_SpawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL!");
        }
        if (_uiManager == null)
        {
            Debug.Log("THE UIMANAGER IS NULL");
        }
        if (_audioSource == null)
        {
            Debug.Log("THE AudioSource on the player IS NULL");
        }
        else {
            _audioSource.clip = _laserSoundClip;
        }
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
        if (_gameManager.isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // if playewr1 then handle this
        if (isPlayerOne == true)
        {
            PlayerOneCalculateMovement();
            if ((Input.GetKeyDown(KeyCode.Space)  && Time.time > _canFire))
            {
                FireLaser();
            }
        }
        //if player 2 then do this
        if (isPlayerTwo == true)
        {
            PlayerTwoCalculateMovement();
            if ((Input.GetKeyDown(KeyCode.RightShift) && Time.time > _canFire))
            {
                FireLaser();
            }
        }
        /*
#if UNITY_ANDROID
  if (((Input.GetKeyDown(KeyCode.Space)|| CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire))
        {
            FireLaser();
        }

#elif UNITY_IOS
if (((Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)) && Time.time > _canFire))
        {
            FireLaser();
        }

#else
        // if playewr1 then handle this
        if (((Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)) && Time.time > _canFire))
        {
            FireLaser();
        }
        //if player 2 then do this

#endif
        */
    }



    void PlayerOneCalculateMovement()
    {
        //float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); // not an error
        //float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); // not an error

        // for y axis
        float Pos_RestrictPosition_y = 5.9f;
        float Neg_RestrictPosition_y = -3.9f;

        // for x axis
        float Pos_RestrictPosition_X = 9.17f;
        float Neg_RestrictPosition_X = -9.17f;

        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        //restricting on y axis
        if (transform.position.y >= Pos_RestrictPosition_y)
        {
            transform.position = new Vector3(transform.position.x, Pos_RestrictPosition_y, 0);
        }
        else if (transform.position.y <= Neg_RestrictPosition_y)
        {
            transform.position = new Vector3(transform.position.x, Neg_RestrictPosition_y, 0);
        }

        // restricting on x axis
        if (transform.position.x >= Pos_RestrictPosition_X)
        {
            transform.position = new Vector3(Pos_RestrictPosition_X, transform.position.y, 0);
        }
        else if (transform.position.x <= Neg_RestrictPosition_X)
        {
            transform.position = new Vector3(Neg_RestrictPosition_X, transform.position.y, 0);
        }
    }

    void PlayerTwoCalculateMovement()
    {
        //float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); // not an error
        //float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); // not an error

        // for y axis
        float Pos_RestrictPosition_y = 5.9f;
        float Neg_RestrictPosition_y = -3.9f;

        // for x axis
        float Pos_RestrictPosition_X = 9.17f;
        float Neg_RestrictPosition_X = -9.17f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }

        //restricting on y axis
        if (transform.position.y >= Pos_RestrictPosition_y)
        {
            transform.position = new Vector3(transform.position.x, Pos_RestrictPosition_y, 0);
        }
        else if (transform.position.y <= Neg_RestrictPosition_y)
        {
            transform.position = new Vector3(transform.position.x, Neg_RestrictPosition_y, 0);
        } 

        // restricting on x axis
        if (transform.position.x >= Pos_RestrictPosition_X)
        {
            transform.position = new Vector3(Pos_RestrictPosition_X, transform.position.y, 0);
        }
        else if (transform.position.x <= Neg_RestrictPosition_X)
        {
            transform.position = new Vector3(Neg_RestrictPosition_X, transform.position.y, 0);
        }
    }

    // player 1
    void FireLaser()
    {
        _canFire = Time.time + _FireRate;
        if (_isTrippleShotActive == true) // tripple fire
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else // single fire
            Instantiate(_LaserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        _audioSource.Play();
    }
    // player 2
   

    public void Damage()
    {
        // if sheld is active
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            _lives -= 1;
            if (_lives == 2)
                _rightEngine.SetActive(true);
            else if (_lives == 1)
                _leftEngine.SetActive(true);


            _uiManager.UpdateLives(_lives);
            if (_lives < 1)
            {

                _SpawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }


    // player 1 tripple shot
    public void TrippleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }
    private IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTrippleShotActive = false;
    }
    /*
    // player 2 tripple shot
    public void TrippleShotActivePlayer2()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutinePlayer2());
    }
    private IEnumerator TrippleShotPowerDownRoutinePlayer2()
    {
        yield return new WaitForSeconds(5);
        _isTrippleShotActive = false;
    }
    */
    // player 1 speed boost
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }
    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    /*
    // player 2 speed boost
    public void SpeedBoostActivePlayer2()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutinePlayer2());

    }
    private IEnumerator SpeedBoostPowerDownRoutinePlayer2()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }*/

    // player 1 shield
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    private IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(10);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }
    /*
    // player 2 shield
    public void ShieldActivePlayer2()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutinePlayer2());
    }
    private IEnumerator ShieldPowerDownRoutinePlayer2()
    {
        yield return new WaitForSeconds(10);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }
    */
    // add to score.
    public void AddToScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);

    }

} 
