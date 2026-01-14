using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Command
{
    public class ReplayController : MonoBehaviour
    {
        [SerializeField] private CommandInvoker invoker;
        [SerializeField] private Image background;

        private Recorder recorder;
        public event Action OnReplayStarted;
        public event Action OnReplayFinished;

        public bool IsReplay;


        [SerializeField] private Button replayButton;


        public void SetRecorder(Recorder commandRecorder)
        {
            recorder = commandRecorder;
        }

        private void Start()
        {
            replayButton.onClick.AddListener(() => Replay());
        }

        public void Replay()
        {
            StartCoroutine(ReplayRoutine());
        }

        private IEnumerator ReplayRoutine()
        {
            IsReplay = true;
            background.enabled = true;

            OnReplayStarted?.Invoke();

            recorder.ResetCharacterPos();

            float startTime = Time.time;
            int index = 0;
            var records = recorder.Records;

            while (index < records.Count)
            {
                if (Time.time - startTime >= records[index].time)
                {
                    invoker.Execute(records[index].command);
                    index++;
                }
                yield return null;
            }

            IsReplay = false;
            background.enabled = false;
            OnReplayFinished?.Invoke();
        }
    }
}

