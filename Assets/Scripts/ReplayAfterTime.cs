using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReplayAfterTime : MonoBehaviour
{
    [SerializeField]
    private bool _inTutorial=false;

    [SerializeField]
    private float _timeUntilReplay=10f;

    public float TimeUntilReplay
    {
        get { return _timeUntilReplay; }
    }

    public float Clock
    {
        get { return _clock; }
    }

    [SerializeField]
    private Transform _fire;

    public Transform Fire
    {
        get { return _fire; }
    }

    [SerializeField]
    private Transform _moveFireFrom;

    [SerializeField]
    private Transform _moveFireTo;

    [SerializeField]
    private Image _sprite;
    
    private Material _desolver;

    [SerializeField]
    private float _maxDesolv;

    [SerializeField]
    private float _minDesolv;
    
    [SerializeField]
    private int _numberOfDucks = 4;

    private float _clock = 0f;

    public UnityEvent _restartGame;

    public UnityEvent _wonGame;

    public UnityEvent _reloadUnsaved;

    private int _frogsInGoal = 0;

    private bool _calcTime = true;

    private PlayerInputAction _playerInputAction;

    private bool _callReloadOnce = true;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _desolver = _sprite.material;
        _playerInputAction.Player.Enable();
        _playerInputAction.Player.Restart.started += ReloadSceneDeleteSaves;
    }

    private void OnDisable()
    {
        _playerInputAction.Player.Restart.started -= ReloadSceneDeleteSaves;

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
        if (_calcTime)
        {
            _clock += Time.deltaTime;
        }

        float desolvValue = Mathf.Lerp(_maxDesolv,_minDesolv, _clock / _timeUntilReplay);
        
        _desolver.SetFloat("_Fade",desolvValue);

        if(_clock / _timeUntilReplay < 1f)
        {

            _fire.position = Vector3.Lerp(_moveFireFrom.position, _moveFireTo.position, _clock / _timeUntilReplay);
        }



    }

    private void FixedUpdate()
    {
        int currentTime = Mathf.Max(0, (int)(_timeUntilReplay - _clock));
        if(currentTime == 0 && _callReloadOnce)
        {
            _callReloadOnce = false;
            ReloadSceneNoSave();
        }
        
    }


    public void ReloadSceneSaved()
    {
        StartCoroutine(WaitBeforeReloadSaved());
    }

    private IEnumerator WaitBeforeReloadSaved()
    {
        if (_restartGame != null)
        {
            _restartGame.Invoke();
        }
        SaveFile.currentPlayer++;
        yield return new WaitForSeconds(1f);

        Loader.LoadRewind(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadSceneNoSave()
    {
        if (!_inTutorial)
        {
            CalculateScore.Death();
        }

        StartCoroutine(ReloadSceneNoSaveWait());
    }

    private IEnumerator ReloadSceneNoSaveWait()
    {
        if (_reloadUnsaved != null && Mathf.Max(0, (int)(_timeUntilReplay - _clock))==0)
        {
            _reloadUnsaved.Invoke();
        }
        yield return new WaitForSeconds(1f);
        Loader.LoadFast(SceneManager.GetActiveScene().buildIndex);
    }

    public void FrogInGoal()
    {
        _frogsInGoal++;

        if(_frogsInGoal >= _numberOfDucks)
        {
            StartCoroutine(LoadNextLevel());
        }else
        if(_frogsInGoal > SaveFile.currentPlayer)
        {
            ReloadSceneSaved();
        }
    }

    private IEnumerator LoadNextLevel()
    {
        _calcTime = false;
        _playerInputAction.Player.Restart.started -= ReloadSceneDeleteSaves;
        SaveFile.DeleteInput();
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        if (_wonGame != null)
        {
            _wonGame.Invoke();
        }
        if (!_inTutorial)
        {
            CalculateScore.CalculateNewScore((_timeUntilReplay - _clock));
            PlayerPrefs.SetInt("LVL", scene);
        }
        yield return new WaitForSeconds(1f);
        Loader.Load(scene);
    }

    public void ReloadSceneDeleteSaves(InputAction.CallbackContext context)
    {
        SaveFile.DeleteInput();
        Loader.LoadFast(SceneManager.GetActiveScene().buildIndex);
    }

    public void SubscribeToWon(UnityAction call)
    {
        if (_wonGame == null)
        {
            _wonGame = new UnityEvent();
        }
        _wonGame.AddListener(call);
    }
    public void DeSubscribeToWon(UnityAction call)
    {

        if (_wonGame == null)
        {
            _wonGame = new UnityEvent();
        }
        _wonGame.RemoveListener(call);
    }


    public void SubscribeToReloadUnsaved(UnityAction call)
    {
        if (_reloadUnsaved == null)
        {
            _reloadUnsaved = new UnityEvent();
        }
        _reloadUnsaved.AddListener(call);
    }

    public void DeSubscribeToReloadUnsaved(UnityAction call)
    {
        if (_reloadUnsaved == null)
        {
            _reloadUnsaved = new UnityEvent();
        }
        _reloadUnsaved.RemoveListener(call);
    }
}
