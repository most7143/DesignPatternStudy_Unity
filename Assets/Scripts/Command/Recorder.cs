using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Command
{

    [System.Serializable]
    public struct CommandRecord
    {
        public float time;
        public ICommand command;

        public CommandRecord(float time, ICommand command)
        {
            this.time = time;
            this.command = command;
        }
    }
    [System.Serializable]
    public struct CharacterSnapshot
    {
        public Vector3 position;
        public Quaternion rotation;

        public CharacterSnapshot(Character character)
        {
            position = character.transform.position;
            rotation = character.transform.rotation;
        }
    }

    public class Recorder : MonoBehaviour
    {
        private readonly List<CommandRecord> records = new();
        private CharacterSnapshot startSnapshot;

        [SerializeField] private CommandInvoker invoker;
        [SerializeField] private ReplayController replayController;
        [SerializeField] private Character character;

        private float startTime;

        public bool IsRecording => isRecording;
        private bool isRecording;


        private void Start()
        {
            replayController.SetRecorder(this);

            invoker.OnExecuted += Record;

            replayController.OnReplayStarted += () =>
           {
               StopRecording();
           };

            replayController.OnReplayFinished += () =>
            {
                StartRecording();
            };




            StartRecording();
        }

        public void ResetCharacterPos()
        {
            character.transform.position = startSnapshot.position;
            character.transform.rotation = startSnapshot.rotation;
        }

        public void StartRecording()
        {
            records.Clear();
            startSnapshot = new CharacterSnapshot(character);
            startTime = Time.time;
            isRecording = true;
        }

        public void StopRecording()
        {
            isRecording = false;
        }

        public void Record(ICommand command)
        {
            if (!isRecording)
                return;

            float time = Time.time - startTime;
            records.Add(new CommandRecord(time, command));
        }

        public IReadOnlyList<CommandRecord> Records => records;
    }

}

