using CampDeconstructor;

var person = new Person(1, "Viv", 33);
var (id, name, age) = person;
Console.WriteLine($"{id} {name} {age}");

/* 
    ! Deconstructor
        --> Use 1: 필드의 값들을 외부로 전달하는 역할을 한다.
        --> Use 2: 속성을 일일히 기재하지 않아도 되는 이점
 */
