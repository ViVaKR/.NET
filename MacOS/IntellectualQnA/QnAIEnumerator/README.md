# IEnumbertor

```csharp
interface IEnumerable
{
    IEnumerator GetEnumerator();
}

interface IEnumerator
{
    bool MoveNext();
    void Reset();
    object Current
    {
        get;
    }
}
```
--> yield return

           // yield :
           // Caller 에게 컬렉션 데이타를 하나씩 리턴할 때 사용함.
           // Enumerator 는 데이타 요소를 하나씩 리턴하는 기능을 하는 것
           // IEnumerator 인터페이스는 3개의 멤버로 구성되어 있음.
           // 1. Current : Property
           // 2. MoveNext() : Method
           // 3. Reset() : Method
           
           // 즉, Enumertator 가 되기 위해서는 Current 와 MoveNext() 를 반듯이 구현해야 함
           // 일반 클래스를 컬렉션 기능 클래스의 기능을 제공하기 위해서는
           // Enumeration 이 가능한 클래스로 만들어야 하는데, 필요한 것이 
           // IEnumerable 인터페이스를 구현하는 것임
           // 

- 만약 데이타의 양이 커서 모든 데이타를 한꺼번에 리턴하는 것하는 것 보다 조금씩 리턴하는 것이 더 효율적일 경우.
- 예를 들어, 어떤 검색에서 1만 개의 자료가 존재하는데, UI에서 10개씩만 On Demand로 표시해 주는게수도 있다. i
- 즉, 사용자가 20개를 원할 지, 1000개를 원할 지 모르기 때문에, 일종의 지연 실행(Lazy Operation)을 수행하는 것이 나을 수 있다.
- 어떤 메서드가 무제한의 데이타를 리턴할 경우. i
- 예를 들어, 랜덤 숫자를 무제한 계속 리턴하는 함수는 결국 전체 리스트를 리턴할 수 없기 때문에 yield 를 사용해서 구현하게 된다.
- 모든 데이타를 미리 계산하면 속도가 느려서 그때 그때 On Demand로 처리하는 것이 좋은 경우.
- 예를 들어 소수(Prime Number)를 계속 리턴하는 함수의 경우,
- 소수 전체를 구하면 (물론 무제한의 데이타를 리턴하는 경우이기도 하지만) 시간상 많은 계산 시간이 소요되므로
- 다음 소수만 리턴하는 함수를 만들어 소요 시간을 분산하는 지연 계산(Lazy Calculation)을 구현할 수 있다.
*
*
*
질문1. 어찌됐건 GetEnumerator()메서드에서는 yield return문을 통해
시퀀스를 하나하나 반환하니 열거자의 기능을 하고 있는거라 보고 main메서드에서
foreach구문을 IEnumerable과 관계없이 구현가능하다고 보면 되죠?

1. 네 맞습니다 GetEnumerator() 메서드는 yield return을 통해 시퀀스를 하나씩 변환 하므로

열거자의 기능을 수행하고 있죠

그러므로 Main 메서ㄷ에서 foreach 구문은 IEnumerable과 관계없이 사용가능합니다.

​
//질문2. IEnumerable<int>를 구현하게 되면 IEnumerator IEnumerable.GetEnumerator()는 무조건
구현해줘야 하는 메서드인가요? 그냥 이 형태를 외우면 되나요? IEnumerable<int>를 구현할 때 한 세트로 구현해주면 되는 부분인지 아니면 다르게 변경이 가능한 부분인지 궁금해요..

2. IEnmerable<T> 인터페이스는 IEnumerable 인터페이스를 상속하므로

IEnumerable의 GetEnumerator() 메서드도 구현해야 합니다 이 메서드는 비제네릭 버전의

IEnumerator를 반환하게 됩니다 이렇게 하는 이유는 기존 코드와의 호환성 유지를 위해서입니다

아무래도 이 형태를 외우는게 좋겠죠

​
//질문3. main 메서드에서 items가 PowerOfTwo객체의 주소를 가리키고 있고 어찌됐건 yield return문을 통해 시퀀스를 하나씩 반환하고 있으니 결국 MoveNext, Current, Reset등의 IEnumerator의 멤버들을 구현하고 있는 셈이니까 IEnumerable<int> 인터페이스를 구현하지 않아도 되는지 궁금합니다. 즉, Case2랑 Case3이 IEnumerable을 구현하냐 안 하냐 차이인데 둘 다 되니 좀 헷갈려서 질문드리는 겁니다.

3. yield return을 사용하는 것은 IEnumerator의 MoveNext, Current, Reset 메서드를 직접 구현하는게 아닙니다.

yield return은 컴파일러에 의해 이러한 메서드를 포함한 클래스를 자동적으로 생성하게 됩니다

따라서 IEnumerable<T> 인터페이스를 구현하지 않으면 foreach문에서 해당 클래스를 사용할 수 없습니다

Case2와 3의 차이는 Case3에서는 IEnumerable<T>구현하지 않았으므로 foreach문에서 사용할 수 없다는 것입니다.

​
//질문4. IEnumerable<T>와 IEnumerable의 차이점, 그리고 IEnumerator<T>와 IEnumerator의 차이점이 궁금해요

4. IEnumerable<T> 와 IEnumerable의 차이점은 IEnumerable<T>가 제네릭 버전이라는 점입니다

즉 IEnumerable<T>는 특정 타입의 시퀀스를 열거하는 열거자를 반환하게 됩니다.

반면 IEnumerable는 모든 타입의 시퀀스를 열거하는 열거자를 반환합니다

​

IEnumerator<T>와 IEnumerator의 차이점도 비슷하죠
IEnumerator<T>는 Current 속성이 T 타입을 반환하지만,
IEnumerator의 Current 속성은 object 타입을 반환합니다

IEnumerator<T> 와 IEnumerable<T> 는 타입의 안정성을 제공하기에 가능하면 이걸 사용하는것이 좋습니다.
*/

/*
*
*
- 모든 컬렉션은 IEnumerable, IEnumerator 를 상속 받아 구현하므로 List, Array 같은 컬렉션 객체들은 foreach 문을 사용할 수 있음.
- IEnumerator : 데이터를 리턴(Getter)하는 열거자
- IEnumerable : 열거자를 리턴하는 Getter 의 Gettre


## IEnumerator (열거자) ##

- 열거자를 구현하는데 필요한 인터페이스


```csharp

public interface IEnumerator 
{
    // 현재 위치의 데이터를 object 타입으로 반환함
    object Current {get;}
    
    // 다음 위치에 데이터가 있으면 true, 없으면 false
    bool MoveNext();
    
    // 인덱스를 초기 상태 위치로 리셋
    void Reset();
}
    
```

## IEnumerable ##

- IEnumerator 를 Get 하는데 필요한 인터페이스
- 클래스 내부의 컬렉션에 대해 반복할 수 있도록, IEnumerator 를 반환한다.
- 열거할 우 있는 제너릭이 아닌 모든 컬렉션에 대한 기본 인터 페이스

```csharp
public interface IEnuerable
{
    IEnumerator GetEnumerator();
}
```

- 사용자 정의 객체를 foreach 문을 돌리기 위해서는 그 객체 타입이 IEnumerable 을 상속 받은 클래스여야함.
- IEnumerable 을 상속 받아 GetEnumerator() 를 구현한 클래스여야 foreach 로 내부 데이터를 열거할 수 있게 됨
- 람다 함수에 사용할 수 없음
- GetEnumerator 
  - 컬렉션을 반복하는 데 사용할 수 있는 IEnumerator를 리턴해야 함
  - IEnumerator 의 3가지 함수를 통해 컬렉션을 반복 할 수 있다.





















