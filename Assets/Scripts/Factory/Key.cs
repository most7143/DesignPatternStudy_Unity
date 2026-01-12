using System;

namespace Factory
{
    public class Key : Item
    {
        public override void Init()
        {
            nameString = "열쇠";
            descString = "어떠한 상자를 열기 위한 열쇠";
        }

    }
}