# gRPC

## 장점

1. `이진 데이터 형식`으로 메시지를 직렬화.
2. `페이로드가 매우 작게 최적화` 되어 있음. (모바일 앱과 같이 제한된 환경에 특화)
3. `HTTP/2` 용으로 설계
4. 단일 TCP 연결이 아닌 `멀티`..
5. .proto 파일을 중심으로 서비스 및 메시지 인터페이스, 클라이언트 동시 관장
6. 서버와 클라이언간에 .proto 파일을 중심으로 상호 Endpoints 를 생성 공유.
7. 별도의 클라이언트를 작성하지 않아도 됨으로 `개발 시간이 절감`됨.

(요약)
1. 구글이 개발한 오픈 소스 RPC 프레임워크.
2. 마이크로서비스 아키텍처에서 서비스 간의 고성능, 저지연 통신을 제공하도록 설계되었습니다.
3. gRPC는 프로토콜 버퍼를 데이터 직렬화 형식으로 사용하며 C#, C++, Java, Python, Go 등을 포함한 다양한 프로그래밍 언어를 지원합니다.

## SignalR vs gRPC (ref 4.)

>- `통신 프로토콜`           웹소켓, HTTP/2, 서버 전송 이벤트 (SSE)                   HTTP/2, TCP, UDP
>- `데이터 직렬화`           JSON, 메시지팩                                        프로토콜 버퍼, JSON
>- `성능`                  gRPC보다 느린                                          SignalR보다 빠르다
>- `언어 지원`	           .NET , 자바스크립트, 자바, 파이썬, 스위프트 등               C#, C++, Go, Ruby 등을 포함하여 SignalR보다 많음.
>- `확장성`                연결 기반 접근 방식으로 인한 제한된 수평 스케일링              요청 기반 접근 방식으로 인한 더 나은 수평 스케일링
>- `보안`	               제한된 보안 옵션                                        SSL/TLS 및 인증을 포함한 여러 보안 옵션
>- `기술통합`              ASP.NET Core에 대한 내장 지원                            쿠버네티스와 이스티오를 포함한 다양한 기술과 통합될 수 있습니다.
>- `사용 사례`             실시간 웹 애플리케이션, 채팅 애플리케이션                     마이크로서비스, 원격 절차 호출, 분산 시스템
>- `사용 편의성`           사용하기 쉽다. NET 개발자                                 일부 개발자에게는 더 가파른 학습 곡선이 있을 수 있다.
>- `미래의 발전`           닷넷의 적극적인 지원                                      Google Cloud Platform 등의 업데이트를 통한 적극적인 지원

## Start gRPC BootCamp

>- Create Solution & Project and Insall Packages

```bash

    # 솔루션 생성
    # VS Code ...
    dotnet new sln -o GrpcCamp
    cd GrpcCamp
    code .  #-> 솔루션 루트에서 VS Code 실행

    # 솔루션 내부에 gRPC 학습용 프로젝트 하나 생성
    $ dotnet new web -o ToDo
    dotnet build
    $ cd ToDo # 패키지 설치를 위하여 해당 프로젝트로 이동...
    $ dotnet dev-certs https --trust # https 프로토콜 신뢰 기반 작성

    # 아래 ref (1) 에 따라서 기본 테스트 코드 작성 (-> 꼭 vscode 용으로 진행)
    # ref 에서 gRPC 클라이언트 만들기는 참고 만 하고 본 프로젝트 참조 하여 작성

    # install tool and packages
    dotnet tool install --global dotnet-ef # EntityFramework Cli Tool

    # ( grpc packages )
    dotnet add GrpcGreeterClient.csproj package Grpc.Net.Client
    dotnet add GrpcGreeterClient.csproj package Google.Protobuf
    dotnet add GrpcGreeterClient.csproj package Grpc.Tools

    # ( Entity Framework packages )
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite  # 또는 SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer # 동시 사용 가능, MSSQL == SQL Server

    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.Tools

    # ( Database migration, 코드를 기반으로 데이터베이스 및 테이블 만들기 )
    dotnet ef migrations add InitialCreate --output-dir Data/Migrations
    dotnet ef database update


    # 솔루션 루트로 다시 이동하기
    $ cd ..

    # 실행하기
    $ dotnet watch run --project ToDo/ --launch-profile https

```

## ref (1). Micorsoft : [Start gRPC](https://learn.microsoft.com/ko-kr/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio-code)

## ref (2). YouTube : [Les jackson](https://youtu.be/Rqz9XiSqH3E?si=tXrFJRc5M6pNh1n3)

## ref (3). With Blazor : [Chat App](https://learn.microsoft.com/ko-kr/azure/azure-signalr/signalr-tutorial-build-blazor-server-chat-app)

## ref (4). 비교 [SignalR vs gRPC](https://www.frontendmag.com/insights/signalr-vs-grpc/)
