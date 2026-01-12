using System;
using Unity.VisualScripting;
using UnityEditor;

namespace Factory
{
    public class Compass : Item
    {
        public override void Init()
        {
            nameString = "나침반";
            descString = "항해 시에 필요한 물건";
        }



    }
}