# DenpendencyInjection

## Packages
* add       : `$ dotnet add package Microsoft.Extensions.Hosting`
* list      : `$ dotnet list package` 
* remove    : `$ dotnet remove package <PACKAGE_NAME>`

## 종속성 주입 
* 인터페이스 또는 기본 클래스를 사용하여 종속성 구현을 추상화 함
* 서비스 컨테이너에 종속성 등록
* .NET 에서 서비스 컨테이너인 IServiceProvider 를 기본 제공함
* `IServiceCollection` 서비스는 일반적으로 앱의 시작에 등록되고 추가됨
* 모든 서비스가 추가되면 `BuildServiceProvider` 를 사용하여 서비스 컨테이너를 만듬
* 서비스가 사용되는 클래스의 생성자에 주입됨
* 프레임워크가 종속성의 인스턴스를 생성 및 삭제작업을 담당함


## Example

1. IMessageWriter 인터페이스로 Write 메서드를 정의함
2. MessageWriter 구현
3. AddSignleton 앱 수명을 사용하여 서비스 등록
