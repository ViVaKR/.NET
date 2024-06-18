# Template

## Create, Install & Uninstall

```bash

    # (템플릿 구성)
    # root > working > extensions > `StringExtensions.cs`
    # root > working > extensions > .template.config > `template.json`

    # (템플릿 설치)
    # Windows
    $ dotnet new install .\
    # Linux 또는 macOS
    $ dotnet new install ./
    $ dotnet new list # 목록 확인

    # (템플릿 테스트)
    $ dotnet new console
    $ dotnet new stringext
    $ dotnet new stringext -ClassName MyExts # 값을 지정하면 파일 이름이 MyExts.cs로 바뀌고 클래스 이름이 MyExts로 바뀝니다.

    # Console.WriteLine("Hello, World!".Reverse());
    $ dotnet run # -> !dlroW ,olleH

    # (템플릿 제거)
    # Windows
    dotnet new uninstall .\
    # Linux 또는 macOS
    dotnet new uninstall ./

```

## Create Solution and Project with Template `template` 

```bash
    $ dotnet new sln -o solution-folder
    $ cd solution-folder 
    $ dotnet new template -o project-folder
    $ dotnet sln add **/*.csproj
    $ dotnet build
    $ dotnet run --project project-folder/project-name.csproj
```
