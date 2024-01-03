
using CampRecords;

var oneline = new OneLineRecord(2, "vivakr", 1);
var (id, nm, grade) = oneline;
Console.WriteLine($"{id} {nm} {grade}");

var person = new Person("Viv", 45);
var (name, age) = person;
Console.WriteLine($"{name} {age}");


var p1 = new Point(12.5, 13.2, 0.5);
var p2 = p1 with {Y = 35.6};
Console.WriteLine(p1.Y);
Console.WriteLine(p2.Y);
Console.WriteLine(p1.Equals(p2));
Console.WriteLine(p1 == p2);

var vector3 = new Vector3(12.5f, 45.2f, 17.3f) {Memo = "HelloWorld"};
var v3 = vector3 with { Y = 38.1f};
Console.WriteLine($"{v3.X} {v3.Y} {v3.Z} {v3.Memo}");
/* 
    ! Record
    # 레코드를 변경할 수 있지만 레코드의 기봅적인 의도는 변경할 수 없는 데이터 모델 지원
    # 값 비교
    # 간결한 구문
    # record class, record 는 참조형식을 선언 class 키워드는 선택사항으로 가독성을 위함
    # record struct 는 값 형식을 선언함

    # class 형식의 경우 두개체가 메모리에서 동일한 개체를 참조하면 두개체는 동일함
    # struct 형식의 경우 두개체가 동일한 형식을 갖고 동일한 값을 자장하면 두개체는 동일함

    # 비 파괴적 변경 : with 식을 사용하여 비파괴적 변형을 구현

 */
