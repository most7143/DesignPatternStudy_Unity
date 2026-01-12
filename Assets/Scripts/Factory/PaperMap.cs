using System;

namespace Factory
{
    public class PaperMap : Item
    {
        public override void Init()
        {
            nameString = "지도";
            descString = "길을 찾기 위해 반드시 가지고 가야할 물건";
        }

    }
}