using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Factory
{
    public enum ItemTypes
    {
        None,
        Normal,
        Rare,
        Unique,

        Legendary,
    }


    public abstract class Item : MonoBehaviour
    {
        public ItemTypes Type;
        protected string nameString;
        protected string descString;

        [SerializeField] private TextMeshProUGUI descText;

        public event Action OnItemDestroyed;

        private void Start()
        {
            StartCoroutine(DestroyTime());
        }

        public abstract void Init();

        public void Show()
        {
            descText.SetText(nameString + "\n" + descString);
        }

        IEnumerator DestroyTime()
        {
            yield return new WaitForSeconds(2f);
            OnItemDestroyed?.Invoke();
            Destroy(this.gameObject);
        }

    }
}

