using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore;
using PizzaStore.Data;
using PizzaStore.Models;

//? 애플리케이션 인스턴스 만들기 -> app
//? builder 에는 Services 속성이 있음.
var builder = WebApplication.CreateBuilder(args);

// Sqlite
var connectionString = builder.Configuration.GetConnectionString("PizzasConnection");
builder.Services.AddSqlite<SqliteDbContext>(connectionString);

builder.Services.AddCors(option => { });



// In Memory
builder.Services.AddDbContext<PizzaDbContext>(x => x.UseInMemoryDatabase("items"));

//! AddSwaggerGen() -> API 에 헤더정보(이름, 버전번호 등)을 설정 함.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Description = "Keep track of your tasks",
        Version = "v1"
    });
});

//? app 인스턴스를 사용하여 라우팅을 설정할 수 있음.
//--> app : 미들웨어를 추가할 수 있음.

/* (미들웨어)
- 요청을 가로채서 인증을 확인
- 클라이언트가 CORS 에 따라 연산을 수행할 수 있는지 확인하는 코드.
- 미들웨 실행이 완료되면 실제 요청이 수행됨
- 데이터가 저장소에서 읽히거나 쓰여지고 호출 클라이언트로 응답이 다시 전송됨
 */

var app = builder.Build();

//! UseSqagger() 및 UseSwaggerUI()를 추가함.
//--> API Project 에 Swagger를 사용하도록 지시하고,
//--> swagger.json file 위치를 알려줌.
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

List<Member> data = [
    new Member(1, "Viv"),
    new Member(2, "Kr"),
    new Member(3, "Hello")
];

// GET
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());

// POST
app.MapPost("/pizzas", (PizzaRecord pizza) => PizzaDB.CreatePizza(pizza));

// PUT : 리소스 업데이트
app.MapPut("/pizzas", (PizzaRecord pizza) => PizzaDB.UpdatePizza(pizza));

// DELETE : 리소스 제거
app.MapDelete("/pizzas", (int id) => PizzaDB.RemovePizza(id));

#region database
// In Memory Database
app.MapGet("/get-pizzas", async (PizzaDbContext db) => await db.Pizzas.ToListAsync());

app.MapGet("/get-pizzas/{id}", async (PizzaDbContext db, int id) => await db.Pizzas.FindAsync(id));

app.MapPost("/post-pizzas", async (PizzaDbContext db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/post-pizzas/{pizza.Id}", pizza);
});

app.MapPut("/put-pizzas/{id}", async (PizzaDbContext db, Pizza update, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
        return Results.NotFound();
    pizza.Name = update.Name;
    pizza.Description = update.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/delete-pizza", async (PizzaDbContext db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
        return Results.NotFound();

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

#endregion

// API를 시작하고 클라이언트의 요청을 수신 대기하도록 함.
// `$ dotnet run` 으로 프로젝트를 시작함.
// 사용포트 : 5000 ~ 5300
app.Run();


//-->      [ Note ]       <---//

// $ dotnet add package Microsoft.EntityFrameworkCore.Sqlite
// $ dotnet tool install --global dotnet-ef
// $ dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0

// API 에서 Swagger 및 Swagger UI 사용
// $ dotnet add package Swashbuckle.AspNetCore
// $ dotnet add package Microsoft.EntityFrameworkCore.InMemory


/* HTTP 동사

GET : 데이터를 반환
POST : 리소스를 만드는 데이터를 전송
PUT : 리소스를 업데이트하는 데이터를 보냄
Delete : 리소스 제거

- 경로만

- 경로 매개 변수 사용 :

 */
