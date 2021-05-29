using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _powerUpSound;
 

    // Update is called once per frame
    void Update()
    {
        // movement of power up
        float restrict_pos_position_y = 7.69f;
        float restrict_neg_position_y = -5.45f;

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y>restrict_pos_position_y)
        {
            transform.position = new Vector3(Random.Range(-9.15f,9.15f), restrict_pos_position_y, 0);
        }
        else if(transform.position.y<restrict_neg_position_y)
        {
            Destroy(this.gameObject);
        }    
    }

    // after collecting power up
    private void OnTriggerEnter2D(Collider2D other)
    {
        // player 1 power ups
        if (other.tag =="Player")
        {
            AudioSource.PlayClipAtPoint(_powerUpSound,transform.position);
            Player player = other.transform.GetComponent<Player>();
            if(player!=null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TrippleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("NO POWERUP");
                        break;
                }

            }
            
            Destroy(this.gameObject);
        }


    }

}
