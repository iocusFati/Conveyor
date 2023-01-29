using GamePlay.QuestFolder;
using Infrastructure.States;
using TMPro;
using UI.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUD_Display : MonoBehaviour
    {
        [SerializeField] private Image _questContainer;
        [SerializeField] private Image _winContainer;
        [SerializeField] private TextMeshProUGUI _questTask;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _replayButton;
        
        private Quest _quest;
        private IGameStateMachine _gameStateMachine;
        private HUD_Animation _animation;

        public void Construct(IGameStateMachine gameStateMachine, UIStaticData uiStaticData)
        {
            _gameStateMachine = gameStateMachine;

            _animation = new HUD_Animation(uiStaticData, _score);
        }

        public void Initialize(Quest quest)
        {
            _quest = quest;
            
            _quest.OnProgressUpdate += UpdateProgress;
        }

        private void Start()
        {
            _replayButton.onClick.AddListener(() =>
                _gameStateMachine.Enter<LoadLevelState, string>(
                    SceneManager.GetActiveScene().name));
        }

        public void ShowTheQuest()
        {
            _questContainer.gameObject.SetActive(true);
            
            _questTask.text = _quest.ToString();
            UpdateProgress(0);
        }

        public void HideQuest() => 
            _questContainer.gameObject.SetActive(false);

        public void ShowWin() => 
            _winContainer.gameObject.SetActive(true);

        public void HideWin() => 
            _winContainer.gameObject.SetActive(false);

        private void UpdateProgress(int itemsPicked)
        {
            _score.text = new string($"{itemsPicked} / {_quest.TargetNum}");
            _animation.PingPongScaleScore();
            
            if (itemsPicked == _quest.TargetNum)
                _quest.OnProgressUpdate -= UpdateProgress;
        }
    }
}