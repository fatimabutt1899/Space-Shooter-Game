using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player_1;
    private Player _player;
    // animator component
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private bool _boxColider;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _FireRate = 3.0f;
    private float _canFire = -1f;

    void Start()
    {
        //_player = GameObject.Find("Player").GetComponent<Player>();//-------------------------- change name
       
        _player = GameObject.Find("Player_1").GetComponent<Player>();//-------------------------- change name

        _audioSource =GetComponent<AudioSource>();
        if (_player == null)
            Debug.Log("Player 1 is null");
        // assign animator component
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.Log("Animator is null");
        if (_audioSource == null)
            Debug.Log("Audio Source on Enemy is null");
        else
            _audioSource.clip = _explosion;



    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time>_canFire)
        {
            _FireRate = Random.Range(3f,7f);
            _canFire = Time.time + _FireRate;
            GameObject enemyLaser= Instantiate(_laserPrefab,transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i =0;i<lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }

    }

    public void CalculateMovement()
    {
        float x_axis_random_spawn = Random.Range(-9.15f, 9.16f);
        float pos_Restrrict_Position_y = 7.1f;
        float neg_Restrrict_Position_y = -5.45f;

        if (transform.position.y > pos_Restrrict_Position_y)
        {
            transform.position = new Vector3(x_axis_random_spawn, 7.1f, 0);
        }
        else if (transform.position.y < neg_Restrrict_Position_y)
        {
            transform.position = new Vector3(x_axis_random_spawn, 7.1f, 0);
        }
        // move down 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        if(other.tag=="Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Player player = other.transform.GetComponent<Player>();
            if(player!=null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
            
        }
        if(other.tag=="Laser")
        {
           
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);




            if(_player!=null)
            {
                _player.AddToScore(10);
            }
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
           
            
        }
        
    }

   
}

    