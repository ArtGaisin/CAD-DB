namespace SQLiteDB;

public class Program
{
    public static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {           
            User u = new() { Name = "Lox", Age = 10, Country = new()};
            db.Users.Add(u);
            db.SaveChanges();
            foreach (User user in db.Users)
                Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
        }
    }
}

/*
using (ApplicationContextOld db = new ApplicationContextOld())
{
    // создаем два объекта User
    UserOld tom = new UserOld { Name = "Tom", Age = 33 };
    UserOld alice = new UserOld { Name = "Alice", Age = 26 };

    // добавляем их в бд
    db.Users.Add(tom);
    db.Users.Add(alice);
    db.SaveChanges();
    Console.WriteLine("Объекты успешно сохранены");

    // получаем объекты из бд и выводим на консоль
    var users = db.Users.ToList();
    Console.WriteLine("Список объектов:");
    foreach (UserOld u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
    }
}*/

