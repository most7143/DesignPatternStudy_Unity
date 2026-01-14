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

<br> <br> <br> <br>
# Unity Singleton Pattern 예제 (스테이지 & 점수 관리)
![Image](https://github.com/user-attachments/assets/81580d89-b897-4eea-9690-cd51e370dc4d)

이 프로젝트 안에는 Unity에서 싱글톤 패턴(Singleton Pattern) 을 활용하여
씬 전환이 발생해도 게임 진행 상태를 유지할 수 있도록 구성한 예제가 포함되어 있다.

스테이지 번호, 점수처럼
여러 씬에서 공통으로 참조되지만 단 하나만 존재해야 하는 데이터를
어떻게 관리하는지에 초점을 맞춘 구조이다.

## 예제 핵심 구조
- S_GameManager : 싱글톤으로 유지되는 전역 게임 상태 관리자
- S_Score : 점수 데이터 관리 클래스
- S_GameController : 씬 단위의 게임 로직 제어
- S_HUD : UI 표시 담당

## 1. 싱글톤 패턴이란?

싱글톤 패턴(Singleton Pattern) 은 **프로그램 전체에서 단 하나의 인스턴스만 존재하도록 보장하는 패턴**이다.

즉,

어디서든 동일한 객체에 접근할 수 있고 객체가 중복 생성되는 것을 방지한다<br>
Unity에서는 주로 씬이 바뀌어도 유지되어야 하는 시스템을 구현할 때 사용한다.

이 예제에서는 GameManager가 싱글톤 역할을 담당한다.

## 2. 싱글톤이 필요한 이유 (이 프로젝트 기준)

이 게임은 스테이지가 바뀌어도 현재 스테이지 번호, 누적 점수를 유지해야 한다. <br>
만약 싱글톤을 사용하지 않는다면 씬마다 점수와 스테이지를 새로 계산하거나, <br>
데이터를 별도로 전달해야 하는 구조가 된다.

## 3. 코드에서의 적용 구조
싱글톤 GameManager 구현
```csharp
public class S_GameManager : MonoBehaviour
{
    public static S_GameManager Instance { get; private set; }

    public int StageLevel { get; set; } = 1;
    public S_Score ScoreManager { get; private set; } = new S_Score();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
```

### 핵심 포인트
- static Instance 를 통해 전역 접근 가능 
- DontDestroyOnLoad 로 씬 전환 시에도 유지
- 중복 생성 시 자동 제거

## 4. 싱글톤을 사용하지 않았을 경우의 문제점

씬마다 GameManager가 새로 생성된다면
- 점수가 초기화된다
- 스테이지 진행 정보가 끊긴다
- 데이터를 전달하기 위한 코드가 늘어난다

결과적으로 게임 상태 관리 로직이 분산되고 복잡해진다.

## 5. 현재 구조의 장점
### 씬이 바뀌어도 상태가 유지된다
```csharp
Hud.UpdateScore(S_GameManager.Instance.ScoreManager.Score);
Hud.UpdateStage(S_GameManager.Instance.StageLevel);
```
어느 씬에서든 동일한 점수와 스테이지를 참조, 데이터 흐름이 단순해진다

### 전역 데이터와 씬 로직의 분리
S_GameController는 게임 플레이 흐름만 담당하고 전역 데이터는 S_GameManager에 위임한다.
```csharp
S_GameManager.Instance.NextStage();
```
전역 상태 관리와 게임 로직이 분리된 구조

<br> <br> <br> <br>

# Unity Factory Pattern 예제 (가챠 아이템 생성)
![Image](https://github.com/user-attachments/assets/b14e230e-2502-4489-94ac-711af6f4ddc9)

Unity에서 팩토리 패턴(Factory Pattern) 을 활용하여
아이템 생성 로직을 한 곳에 모은 간단한 가챠 예제를 추가하였다.

아이템의 종류와 확률은 다양하지만, 아이템을 사용하는 쪽에서는 “무엇이 생성되는지”를 알 필요가 없는 구조를 목표로 한다.

## 예제 핵심 구조
- Item : 아이템의 공통 추상 클래스
- PaperMap, Sword 등 : 아이템 구현체
- ItemFactory : 아이템 생성 책임을 담당
- Gacha : 아이템을 요청하고 사용하는 쪽

## 1. 팩토리 패턴이란?
팩토리 패턴(Factory Pattern)은 객체 생성을 담당하는 로직을 별도의 클래스로 분리하여, <br>
어떤 객체를 생성할지에 대한 판단과 객체를 사용하는 로직을 분리하는 패턴이다.

이 구조를 통해 사용하는 쪽은 생성 방식과 구체 타입에 의존하지 않게 된다.

## 2. 코드에서의 적용 구조
**Item (공통 인터페이스 역할)**
```csharp
public abstract class Item : MonoBehaviour
{
    protected string nameString;
    protected string descString;

    public abstract void Init();
}
```

모든 아이템은 Item을 상속. 사용하는 쪽은 Item 타입으로만 접근한다

**ItemFactory (핵심)**
```csharp
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
        }

        return null;
    }
}
```
아이템 생성 판단과 생성 로직이 팩토리에 집중해 아이템 추가·확률 변경 시 수정 지점이 명확하다

**Gacha (사용하는 쪽)**
```csharp
Item item = factory.Create();
item.Init();
```
가챠 시스템은 구체적인 아이템 타입을 알지 못한다. “아이템을 하나 생성해 달라”는 요청만 한다

## 3. 이 구조의 장점

- 아이템 생성 로직이 한 곳에 모인다.
- 새로운 아이템 추가 시 기존 사용 코드 수정이 필요 없다
- 객체 생성과 사용 책임이 명확히 분리된다

## 정리
이 예제는 Unity 환경에서 팩토리 패턴의 핵심 목적 **객체 생성 책임 분리** 를 가장 단순하게 보여주는 구조다. <br>
아이템을 사용하는 쪽은 생성 방식이나 구체 타입에 의존하지 않고, 팩토리는 생성 로직만 책임진다.


<br><br><br><br>
# Unity Command Pattern 예제 (캐릭터 이동&점프, 리플레이)

이 프로젝트 안에는 Unity에서 커맨드 패턴(Command Pattern) 을 활용하여
플레이어 입력을 객체로 분리하고, 입력 기록과 리플레이까지 확장한 예제가 포함되어 있다.

이동, 점프 같은 입력을 즉시 실행하지 않고
명령(Command) 객체로 캡슐화하여 실행·저장·재실행이 가능한 구조를 목표로 한다.

## 예제 핵심 구조

- ICommand : 모든 명령의 공통 인터페이스
- CommandMove, CommandJump : 실제 행동을 수행하는 명령 객체
- CommandInvoker : 명령을 실행하는 호출자
- Recorder : 실행된 명령과 시간을 기록
- ReplayController : 기록된 명령을 다시 실행
- Character : 실제 행동의 수신자(Receiver)

## 1. 커맨드 패턴이란?

커맨드 패턴(Command Pattern)은 요청(행동)을 객체로 캡슐화하여, 요청을 보내는 쪽과 실제 동작을 수행하는 쪽을 분리하는 패턴이다.

즉,

무엇을 할지(Command) / 실행할지(Invoker) / 실제로 어떻게 동작하는지(Receiver) 를 분리한다.

이 프로젝트에서는 플레이어 입력(이동, 점프) 이 커맨드에 해당한다.

## 2. 패턴을 사용하지 않았을 경우

일반적인 입력 처리 방식에서는 입력 → 캐릭터 동작이 직접 연결된다.

```csharp
if (Input.GetKey(KeyCode.A))
{
    character.Move(Vector2.left);
}

if (Input.GetKeyDown(KeyCode.Space))
{
    character.Jump();
}
```

이 방식의 문제점은 다음과 같다.
- 입력과 캐릭터 로직이 강하게 결합된다
- 입력을 저장하거나 되돌리기 어렵다
- 리플레이, AI 입력, 네트워크 입력으로 확장하기 까다롭다
  
“입력 그 자체”를 하나의 데이터로 다룰 수 없다

## 3. 커맨드 패턴 적용 구조
```csharp
ICommand (명령의 공통 인터페이스)
public interface ICommand
{
    void Execute();
}
```
모든 입력은 “실행 가능한 하나의 명령” 으로 정의된다.
```csharp
CommandMove / CommandJump (구체 명령)

public class CommandMove : ICommand
{
    private Character character;
    private Vector2 direction;

    public CommandMove(Character character, Vector2 dir)
    {
        this.character = character;
        direction = dir;
    }

    public void Execute()
    {
        character.Move(direction);
    }
}

public class CommandJump : ICommand
{
    private Character character;

    public CommandJump(Character character)
    {
        this.character = character;
    }

    public void Execute()
    {
        character.Jump();
    }
}
```

입력은 Command 객체 생성으로 변환된다. 명령은 자신이 무엇을 실행할지만 알고 있다<br>
실제 동작은 Character(Receiver)에게 위임된다
```csharp
CommandInvoker (호출자)

public class CommandInvoker : MonoBehaviour
{
    public event Action<ICommand> OnExecuted;

    public void Execute(ICommand command)
    {
        command.Execute();
        OnExecuted?.Invoke(command);
    }
}
```
Invoker는 명령을 실행만 할 뿐 그 명령이 무엇을 의미하는지는 알 필요가 없다<br>
또한 실행된 명령을 외부에 이벤트로 알린다. → 이 지점이 Recorder와 연결된다.
## 4. 입력 기록 (Recorder)
이 예제의 핵심은 커맨드 패턴을 입력 리플레이로 확장한 부분이다.
```csharp
public struct CommandRecord
{
    public float time;
    public ICommand command;
}

invoker.OnExecuted += Record;
```
실행된 명령과 그 시점의 경과 시간(Time.time 기준)을 함께 저장한다.
```csharp
시작 시 캐릭터 스냅샷 저장
public struct CharacterSnapshot
{
    public Vector3 position;
    public Quaternion rotation;
}
```

리플레이 시 항상 동일한 상태에서 재생되도록 위치·회전 스냅샷을 함께 보관한다.

## 5. 리플레이 시스템 (ReplayController)
```csharp
private IEnumerator ReplayRoutine()
{
    recorder.ResetCharacterPos();

    float startTime = Time.time;
    int index = 0;

    while (index < records.Count)
    {
        if (Time.time - startTime >= records[index].time)
        {
            invoker.Execute(records[index].command);
            index++;
        }
        yield return null;
    }
}
```
기록된 시간에 맞춰 동일한 명령 객체를 다시 실행한다. <br>
입력 코드, 캐릭터 코드 수정 없이 명령만 재실행하는 구조가 완성된다.

## 6. 이 구조의 장점
### 1. 입력과 동작이 분리된다
입력은 Command 생성<br>
실행은 Invoker<br>
실제 동작은 Character<br>

각각의 책임이 명확하다.

### 2. 리플레이 / AI / 네트워크 확장에 유리
Recorder → 리플레이<br>
AI → Command 생성<br>
네트워크 → Command 전송

모두 같은 구조를 사용할 수 있다.

### 3. 실행 흐름을 데이터로 다룰 수 있다
입력은 더 이상 “순간 이벤트”가 아니라 저장·재생 가능한 데이터가 된다.




