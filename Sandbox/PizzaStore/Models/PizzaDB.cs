namespace PizzaStore;

public record PizzaRecord
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PizzaRecord(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class PizzaDB
{
    private static List<PizzaRecord> _pizzas =
    [
        new PizzaRecord(1, "Montemagno, Pizza shaped like a great mountain" ),
        new PizzaRecord(2, "The Galloway, Pizza shaped like a submarine, silent but deadly"),
        new PizzaRecord(3, "The Noring, Pizza shaped like a Viking helmet, where's the mead")
    ];

    public static List<PizzaRecord> GetPizzas()
    {
        return _pizzas;
    }

    public static PizzaRecord? GetPizza(int id)
    {
        return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
    }

    public static PizzaRecord CreatePizza(PizzaRecord pizza)
    {
        _pizzas.Add(pizza);
        return pizza;
    }

    public static PizzaRecord UpdatePizza(PizzaRecord update)
    {
        _pizzas = _pizzas.Select(x =>
        {
            if (x.Id == update.Id)
            {
                x.Name = update.Name;

            }
            return x;
        }).ToList();
        return update;
    }

    public static void RemovePizza(int id)
    {
        _pizzas = [.. _pizzas.FindAll(x => x.Id != id)];
    }
}
