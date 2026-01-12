
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class Gacha : MonoBehaviour
    {
        [SerializeField] Canvas canvas;

        [SerializeField] private Button ClickButton;

        private ItemFactory factory = new();
        private bool IsSpawnd;

        private void Start()
        {
            ClickButton.onClick.AddListener(Click);
        }

        private void Click()
        {
            if (IsSpawnd)
                return;



            IsSpawnd = true;
            Item currentItem = factory.Create();
            currentItem.transform.SetParent(canvas.transform, false);
            currentItem.Init();
            currentItem.Show();

            currentItem.OnItemDestroyed += () => IsSpawnd = false;
        }



    }
}