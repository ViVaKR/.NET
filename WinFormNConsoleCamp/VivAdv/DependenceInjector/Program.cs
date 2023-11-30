
using DependenceInjector;

// Injection (1)
var cs = new ConstructorInjection(new VvFormat());
cs.Print();


// Injection (2)
var writer = new EventLogWriter();
var simple = new Simple();

simple.Notify(writer, "to logger");
// Injection (3)
var client = new Client();
client.Run(new Service());

