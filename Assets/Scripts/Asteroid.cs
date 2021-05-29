using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 8f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    
    public void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Vector3 pos_explode = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_explosionPrefab, pos_explode, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);




        }
    }
}
   
