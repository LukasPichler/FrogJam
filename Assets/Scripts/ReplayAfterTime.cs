using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReplayAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilReplay=10f;

    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private int _numberOfDucks = 4;

    private float _clock = 0f;

    public UnityEvent _restartGame;


    private int _frogsInGoal = 0;

    private void Awake()
    {
        PlayerInputAction _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Restart.performed += ReloadSceneDeleteSaves;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_restartGame == null)
        {
            _restartGame = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

        _clock += Time.deltaTime;



    }

    private void FixedUpdate()
    {
        int currentTime = Mathf.Max(0, (int)(_timeUntilReplay - _clock));
        _textMesh.text = "Time:" + currentTime + "s";
        if(currentTime == 0)
        {
            ReloadSceneNoSave();
        }
        
    }


    public void ReloadSceneSaved()
    {
        if (_restartGame != null)
        {
            _restartGame.Invoke();
        }
        SaveFile.currentPlayer++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadSceneNoSave()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FrogInGoal()
    {
        _frogsInGoal++;

        if(_frogsInGoal >= _numberOfDucks)
        {
            Debug.Log("Win");
        }
        if(_frogsInGoal > SaveFile.currentPlayer)
        {
            ReloadSceneSaved();
        }
    }

    public void ReloadSceneDeleteSaves(InputAction.CallbackContext context)
    {
        SaveFile.DeleteInput();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
