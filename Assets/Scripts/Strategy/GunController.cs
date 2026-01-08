using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunConroller : MonoBehaviour
{
    public BaseGun CurrentGun;

    public Transform Pivot;

    public Transform ShotPoint;

    public SpriteRenderer GunSprite;


    public TextMeshProUGUI GunInfoText;

    private List<BaseGun> availableGuns;

    private void Awake()
    {
        BaseGun[] guns = GetComponentsInChildren<BaseGun>(true);
        availableGuns = new List<BaseGun>(guns);
    }

    private void Start()
    {
        ChangeGun(CurrentGun);
    }

    private void Update()
    {
        RefreshGunDirection();

        HandleWeaponInput();

        if (Input.GetMouseButtonDown(0))
        {
            CurrentGun.Shoot(ShotPoint);
        }
    }

    // 총기 변경 입력 처리
    private void HandleWeaponInput()
    {
        for (int i = 0; i < availableGuns.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ChangeGun(availableGuns[i]);
            }
        }
    }

    private void ChangeGun(BaseGun newGun)
    {
        CurrentGun = newGun;
        GunSprite.sprite = newGun.GunSprite;

        GunInfoText.text = $"Gun: {newGun.GetType().Name}\n" +
            $"Damage: {newGun.Damage}\n" +
            $"Speed: {newGun.Speed}\n" +
            $"Delay: {newGun.ShotDelay:F1}Seconds\n";
    }


    // 총구 방향을 마우스 위치로 갱신
    private void RefreshGunDirection()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - Pivot.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Pivot.rotation = Quaternion.Euler(0f, 0f, angle);

    }
}