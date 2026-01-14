using System;
using Factory;
using UnityEngine;


namespace Command
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Character character;

        [SerializeField] private CommandInvoker invoker;

        [SerializeField] private ReplayController replayController;

        private bool isReplay => replayController.IsReplay;


        void Update()
        {
            if (isReplay)
                return;

            Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

            invoker.Execute(new CommandMove(character, move));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                invoker.Execute(new CommandJump(character));

            }



        }
    }

}
