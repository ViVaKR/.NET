# Entity Framework

```bash

    $ dotnet new console -o EFCamp
    $ cd EfCamp

    # plugins
    $ dotnet tool install --global dotnet-ef

    # -- 토큰은 뒤 나타나는 모든 항목을 인수로 처리하고 이를 옵션으로 구문 분석하지 않도록 지시
    # dotnet ef 에서 사용하지 않는 추가 인수는 앱으로 전달됨
    # 프로젝트에 대한 환경 지정
    $ dotnet ef database update -- --environment Production

    $ dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    $ dotnet add package Microsoft.EntityFrameworkCore.Tools
    $ dotnet add package Microsoft.EntityFrameworkCore.Design

    # Create 3 Folers ( Contexts, Db, Models ) and 3 Class files
    $ dotnet new class -n BloggingContext -o Contexts
    $ dotnet new class -n Blog -o Models/
    $ dotnet new class -n Post -o Models/

```

* Microsoft.EntityFrameworkCore.Tools Enables Commands
    * Add-Migration
    * Bundle-Migration
    * Drop-Database
    * Get-DbContext
    * Get-Migration
    * Optimize-DbContext
    * Remove-Migration
    * Scaffold-DbContext
    * Script-Migration
    * Update-Database
