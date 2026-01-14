using System;
using UnityEngine;

namespace Command
{

    public interface ICommand
    {
        public void Execute();

    }

    public class CommandInvoker : MonoBehaviour
    {
        public event Action<ICommand> OnExecuted;

        public void Execute(ICommand command)
        {
            // 실제 실행
            command.Execute();
            OnExecuted?.Invoke(command);


        }

    }
}


