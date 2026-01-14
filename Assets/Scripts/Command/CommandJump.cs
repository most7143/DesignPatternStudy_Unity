
using UnityEngine;

namespace Command
{
    public class CommandJump : ICommand
    {
        private Character character;

        public CommandJump(Character character)
        {
            this.character = character;
        }


        public void Execute()
        {
            character.Jump();
        }


    }

}
