using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;

    private bool _isEnemyLaser=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser==false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y > 7.1f)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void MoveDown()
    {
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
        if (transform.position.y < -7.1f)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public void DeleteEnemyLaser()
    {
        _isEnemyLaser = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" && _isEnemyLaser==true)
        {
            Player player = other.GetComponent<Player>();
            if(player!=null)
                player.Damage();
        }
    }
}
