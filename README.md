# Unity Strategy Pattern 예제 (무기 교체)

![Image](https://github.com/user-attachments/assets/c350b71a-f44c-44d6-9be3-456c52415f85)

이 프로젝트 안에는 **Unity에서 전략 패턴(Strategy Pattern)** 을 활용하여  
총기의 발사 방식을 유연하게 교체할 수 있도록 설계한 예제를 추가했다.

핸드건, 샷건처럼 **동작은 다르지만 동일한 인터페이스를 사용하는 구조**를 목표로 한다.

## 예제 핵심 구조

- `BaseGun` : 발사 전략의 공통 추상 클래스
- `Handgun`, `Shotgun` : 서로 다른 발사 전략 구현체
- `GunController` : 구체적인 발사 방식에 의존하지 않는 컨트롤러

## 1. 전략 패턴이란?

**전략 패턴(Strategy Pattern)** 은 알고리즘(동작)을 객체로 캡슐화하고,
런타임에 해당 알고리즘을 교체할 수 있도록 하는 패턴이다.

즉,

- 어떤 일을 **어떻게 수행할지**를 분리하고
- 사용하는 쪽에서는 **구현 세부를 알 필요가 없도록** 만든다.

이 프로젝트에서는 **총이 발사되는 방식(Fire 로직)** 이 스트래티지에 해당한다.

### 코드에서의 적용 구조

```csharp
public abstract class BaseGun : MonoBehaviour
{
   // 공통 변수들 선언

    public virtual void Shoot(Transform shotPoint)
    {
      
    }

    public abstract void Fire(Transform shotPoint);
}
```

```csharp
public class Handgun : BaseGun
{
    public override void Fire(Transform shotPoint)
    {
       // 단일 발사
    }
}
```

```csharp
public class Shotgun : BaseGun
{
    public override void Fire(Transform shotPoint)
    {
      //산탄 형식으로 발사
    }
}
```
## 2. 스트래티지 패턴이 필요한 이유

총기 시스템처럼 **동작이 다양해질 가능성이 높은 구조**에서는 스트래티지 패턴이 특히 효과적이다.

### 패턴을 사용하지 않았을 경우
발사 방식이 총 종류에 따라 분기된다면, 코드는 빠르게 복잡해지고,<br>
<mark>새로운 총을 추가 하거나 제거해야 하는 경우</mark> 관련된 모든 클래스를 수정하는 번거로움이 생길 것이다.<br>
아래의 예시를 보면..

```csharp
if (gunType == GunType.Handgun)
{
    // 단일 발사
}
else if (gunType == GunType.Shotgun)
{
    // 산탄 발사
}
else if (gunType == GunType.Rifle)
{
    // 연사
}

```
이 방식의 문제점은 다음과 같다.

- 총 종류가 늘어날수록 조건문이 계속 증가한다
- 기존 코드 수정이 잦아진다
- 테스트와 유지보수가 어려워진다
- 실수로 코드 수정을 누락했을 경우 그 부분을 찾으려고 수 없이 코드를 돌아다녀야 하는 최악의 경우가 존재 할 수 있다.

### 현재 구조의 장점

이 프로젝트에서는 **발사 방식을 `BaseGun`을 상속한 클래스 단위로 분리**하여  
위에서 언급한 문제를 해결한다.

### 1. 발사 방식 변경이 교체로 끝난다

```csharp
public class GunConroller : MonoBehaviour
{
    private void ChangeGun(BaseGun newGun)
    {
        CurrentGun = newGun;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurrentGun.Shoot(ShotPoint);
        }
    }
}

```
GunController는 현재 총이 핸드건인지, 샷건인지 알지 못한다 단지 BaseGun 타입으로만 접근한다
**동작(전략)과 호출(Context)이 분리된 구조**

---

### 2. 새로운 총 추가가 쉽다

새로운 총을 추가하려면  
기존 코드를 수정하지 않고 새로운 전략 클래스만 추가하면 된다.

```csharp
public class Rifle : BaseGun
{
    public override void Fire(Transform shotPoint)
    {
        // 연사 로직 구현
    }
}
```
기존 코드 수정 ❌ <br>
클래스 추가 ⭕<br>
확장에는 열려 있고, 변경에는 닫힌 구조

### 3️. 공통 발사 로직을 부모클래스가 관리

```csharp
public virtual void Shoot(Transform shotPoint)
{
    if (!canShoot)
        return;

    Fire(shotPoint);
    StartCoroutine(ShootWithDelay());
}
```
- 발사 딜레이 처리
- 연속 입력 방지
- 발사 흐름 통합 관리
하위 클래스는 **발사 방식 구현에만 집중**하면 된다

<br>
<br>
<br>
<br>

# Unity Observer Pattern 예제 (두더지 잡기)

![Image](https://github.com/user-attachments/assets/b0d08d0a-2139-4ade-b823-4de688a4b53b)

이 프로젝트 안에 Unity 환경에서 옵저버 패턴(Observer Pattern)을 실제 게임 로직에 자연스럽게 적용한 간단한 예제이다. <br>
몬스터의 상태 변화(사망, 도주)를 이벤트로 분리하고, 점수 시스템이 그 변화를 독립적으로 반응하도록 구성했다.

---

## 예제 핵심 구조

- O_Monster : 상태 변화를 알리는 Subject
- O_Score : 점수 계산을 담당하는 Observer
- O_SceneManager : Subject와 Observer를 연결하는 중재자
- O_SpawnPoint : 스폰 위치 점유 상태 관리 (게임 로직)

---

## 1. 옵저버 패턴이란?

**옵저버 패턴(Observer Pattern)** 은
어떤 객체의 상태가 변경되었을 때,
그 객체를 구독하고 있는 다른 객체들에게
자동으로 알림을 전달하는 패턴이다.

즉,

- 상태 변화는 발생한 객체가 책임지고 알린다
- 그 변화에 어떻게 반응할지는 관찰자의 책임이다

상태를 알리는 객체는
누가 그 알림을 받는지 알 필요가 없다.

---

## 2. 옵저버 패턴을 사용하면 좋은 이유
게임에서는 하나의 이벤트가
여러 시스템에 영향을 주는 경우가 많다.

### 패턴을 사용하지 않았을 경우
몬스터가 사망할 때마다
점수, UI, 스폰 로직을 직접 호출한다면
몬스터는 너무 많은 책임을 가지게 된다.

```csharp
// Monster 내부
score.Add(2);
ui.UpdateScore();
sceneManager.OnMonsterDead();
```
이 구조의 문제점은 다음과 같다.
- 몬스터가 다른 시스템을 직접 알고 있어야 한다
- 시스템이 추가될 때마다 몬스터 코드가 수정된다
- 테스트와 확장이 어려워진다

### 현재 구조의 장점
이 프로젝트에서는
몬스터가 이벤트만 발생시키고,
외부 시스템이 이를 구독하도록 설계했다.
```csharp
public event Action OnDeath;
public event Action OnRun;
```
- 몬스터는 자신의 상태 변화만 책임진다
- 점수, UI, 스폰 로직은 독립적으로 반응한다
- 서로 강하게 의존하지 않는 구조가 된다

---

## 3. 오리지널 옵저버 패턴과 Unity에서의 적용

**오리지널 옵저버 패턴**

아래는 전통적인 옵저버 패턴 구조를
플레이어가 피해를 받아 체력이 감소하는 상황으로 단순화한 예시다.
```csharp
public interface IObserver
{
    void OnNotified(int damage);
}

public class PlayerUI : IObserver
{
    public void OnNotified(int damage)
    {
        // 체력 UI 갱신
    }
}

public class Player
{
    private List<IObserver> observers = new();
    private int hp = 100;

    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        foreach (var o in observers)
            o.OnNotified(damage);
    }
}
```
- Player : Subject (상태 변화 발생)
- PlayerUI : Observer (변화에 반응)

**유니티에서의 옵저버 패턴 (C#)**

이 예제는 위 구조를 Unity에 맞게 event 기반으로 단순화했다
```csharp
// Monster.cs
public event Action OnDeath;
public event Action OnRun;
```
```csharp
// Score.cs
public void OnMonsterKilled() { }
public void OnMonsterEscaped() { }
```
```csharp
//SceneManager.cs
monster.OnDeath += () => score.OnMonsterKilled();
monster.OnRun   += () => score.OnMonsterEscaped();
```
옵저버 패턴의 핵심은 **상태 변화를 발생시키는 객체와, 그 변화에 반응하는 로직을 분리**하는 것이다.<br>
오리지널 옵저버 패턴은 인터페이스와 Notify() 구조로 이 관계를 명확히 표현한다. <br>
Unity에서는 이를 event와 Action으로 단순화해, 개념은 유지하면서 구현 부담을 줄인다.<br>
이 예제의 구조 역시 몬스터는 상태만 알리고, 점수 시스템은 그 알림에 반응하도록 설계되어 있다.






