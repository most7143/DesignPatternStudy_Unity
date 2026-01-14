using UnityEngine;

namespace Command
{


    public class CommandMove : ICommand
    {
        private Character character;
        private Vector2 direction;

        public CommandMove(Character currentCharacter, Vector2 dir)
        {
            character = currentCharacter;
            SetDirection(dir);
        }
        public void Execute()
        {
            character.Move(direction);
        }


        public void SetDirection(Vector2 dir)
        {
            direction = dir;
        }


    }

}
