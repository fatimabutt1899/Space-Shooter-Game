using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private GameObject _GameOverText;
    [SerializeField]
    private Text _RestartText;
    private GameManager _gameManager;

    void Start()
    {
        Player player = transform.GetComponent<Player>();
        
            _scoreText.text = "Score : " + 0;
            _GameOverText.gameObject.SetActive(false);
            _RestartText.gameObject.SetActive(false);
            _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            if (_gameManager == null)
                Debug.LogError("Game Manager is null");
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite=_liveSprites[currentLives];
        if(currentLives==0)
        {
            GameOverSequence();              
        }
    }
    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _GameOverText.gameObject.SetActive(true);
        _RestartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

    }
    IEnumerator GameOverFlickerRoutine()
    {
      
        while (true)
        {
            _GameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _GameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            if(Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }
}
