

using UnityEngine;

namespace Factory
{
    public class ItemFactory
    {
        public Item Create()
        {
            ItemTypes type = GetItemType();

            switch (type)
            {
                case ItemTypes.Normal:
                    return Object.Instantiate(Resources.Load<Item>("Factory/F_Sword"));

                case ItemTypes.Rare:
                    return Object.Instantiate(Resources.Load<Item>("Factory/F_Compass"));

                case ItemTypes.Unique:
                    return Object.Instantiate(Resources.Load<Item>("Factory/F_PaperMap"));

                case ItemTypes.Legendary:
                    return Object.Instantiate(Resources.Load<Item>("Factory/F_Key"));
                default:
                    return null;
            }
        }

        private ItemTypes GetItemType()
        {
            float chance = Random.Range(0, 1f);

            if (chance <= 0.05f)
                return ItemTypes.Legendary;
            else if (chance <= 0.2f)
                return ItemTypes.Unique;
            else if (chance <= 0.55f)
                return ItemTypes.Rare;
            else
                return ItemTypes.Normal;
        }
    }
}
