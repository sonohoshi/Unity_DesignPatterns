# State Pattern

상태 패턴은 주로 오브젝트가 특정 조건에 따라 행동이 달라지거나, 혹은 상태에 따라 다른 행동을 할 때, 즉 오브젝트의 상태를 정의할 수 있으며 그에 따라 동작이 달라질 때 이용하는 디자인 패턴이다. 게임 개발에서 특히 알아보기 쉽게 배울 수 있는데, 몬스터가 맵을 방황하는 상태, 플레이어를 쫓아오는 상태 등으로 나뉘어진다고 말하면 이해가 쉬울 것 같다. 실제로 몬스터 등을 구현할 때 이용할 수 있다.  

본문에서는 상태 패턴을 응용한 것 중 하나인 FSM, Finite State Machine (유한 상태 기계)에 대해 알아볼 것이다.

## FSM 이란?

서론에서도 이야기 했듯이, 영문으로는 `Finite State Machine`, 우리말로는 유한 상태 기계라 한다. 이는 상태 패턴에서 파생된 것 중 하나인데, 하나의 오브젝트는 `동시에 여러 상태를 가지지 않고` 하나의 상태만 가지고 있는 것을 말한다. 예시를 들어보겠다.

```cs
일반적인 상태 패턴

각 객체는 상태를 '동시에' 여러개 가질 수 있다.
만약 플레이어 객체가 있다면, "전투 중" 상태와 함께 "건강함" 상태를 가질 수 있는 것이다.
```
```cs
FSM

각 객체는 상태를 한 시점에 여러개를 가질 수 없다.
만약 플레이어 객체가 있다면, "탐험 중" 상태와 "전투 중" 상태를 함께 가질 수 없다는 것이다.
```

이정도의 예시로 이해가 됐길 바란다.

## 장점

1. 각 객체가 상태에 따라 어떻게 작동하는지 알기 쉽게 구현을 할 수 있다.  
코드만 봐도 어떤 상태에서는 어떤 동작을 하는지 쉽게 알 수 있기 때문에, 개발하는 입장에서 관리가 상당히 쉽다.

2. 불필요한 조건문을 줄일 수 있다.  
보통 유한 상태 기계를 구현할 때, 하나의 메소드를 만들고 그 상태에 따라 해당 메소드를 오버라이드한 것을 구현 후 이용하게 된다. 예시를 들어보겠다.

```cs
휴대폰의 화면이 켜져있을 때, 전원 버튼을 누르면 화면이 꺼진다.
휴대폰의 화면이 꺼져있을 때, 전원 버튼을 누르면 화면이 켜진다.

여기서 "전원 버튼을 누르는 작업" 을 하나의 메소드로 정의한다.
virtual void PushButton() { }
그리고 각 상태마다 PushButton() 메소드를 오버라이드 한다.

// ScreenTurnedOnState class...
override void PushButton() 
{
    TurnOffScreen();
    state = new ScreenTurnedOffState();
}

// ScreenTurnedOffState class...
override void PushButton() 
{
    TurnOnScreen();
    state = new ScreenTurnedOnState();
}

이런 식으로 구현하는 것이다.
```

## 단점

1. 유한 상태 머신은 상태를 단 하나만 갖기 때문에, 여러 상태를 가져야 하는 경우에는 알맞지 않다.
`ex) 전투 중, 여러 버프나 디버프를 받고 있을 경우`

2. 상태가 너무 많으면 각 상태를 바꾸는 코드를 적는데에도 조건문이 많이 들어가게 될 수 있다.  