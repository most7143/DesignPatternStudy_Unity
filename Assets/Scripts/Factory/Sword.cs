using System;

namespace Factory
{
    public class Sword : Item
    {
        public override void Init()
        {
            nameString = "검";
            descString = "그냥 검이다";
        }

    }
}