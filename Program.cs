//1 Facade
// Подсистема бронирования номеров
/*public class RoomBookingSystem
{
    public void BookRoom(int roomId)
    {
        Console.WriteLine($"Room {roomId} has been booked.");
    }

    public void CancelRoomBooking(int roomId)
    {
        Console.WriteLine($"Room booking for room {roomId} has been canceled.");
    }

    public bool IsRoomAvailable(int roomId)
    {
        // Проверка доступности комнаты
        return true; // Допустим, комната доступна
    }
}

// Подсистема ресторана
public class RestaurantSystem
{
    public void BookTable(int tableId)
    {
        Console.WriteLine($"Table {tableId} has been booked.");
    }

    public void OrderFood(string food)
    {
        Console.WriteLine($"Food '{food}' has been ordered.");
    }

    public void OrderTaxi()
    {
        Console.WriteLine("Taxi has been ordered.");
    }
}

// Подсистема управления мероприятиями
public class EventManagementSystem
{
    public void BookConferenceRoom(int roomId)
    {
        Console.WriteLine($"Conference room {roomId} has been booked.");
    }

    public void OrderEquipment(string equipment)
    {
        Console.WriteLine($"Equipment '{equipment}' has been ordered.");
    }
}

// Подсистема уборки
public class CleaningService
{
    public void ScheduleCleaning(int roomId)
    {
        Console.WriteLine($"Cleaning has been scheduled for room {roomId}.");
    }

    public void RequestCleaning(int roomId)
    {
        Console.WriteLine($"Cleaning has been requested for room {roomId}.");
    }
}
public class HotelFacade
{
    private RoomBookingSystem _roomBookingSystem;
    private RestaurantSystem _restaurantSystem;
    private EventManagementSystem _eventManagementSystem;
    private CleaningService _cleaningService;

    public HotelFacade()
    {
        _roomBookingSystem = new RoomBookingSystem();
        _restaurantSystem = new RestaurantSystem();
        _eventManagementSystem = new EventManagementSystem();
        _cleaningService = new CleaningService();
    }

    // Бронирование номера с заказом еды и услугами уборки
    public void BookRoomWithServices(int roomId, string food)
    {
        if (_roomBookingSystem.IsRoomAvailable(roomId))
        {
            _roomBookingSystem.BookRoom(roomId);
            _restaurantSystem.OrderFood(food);
            _cleaningService.ScheduleCleaning(roomId);
        }
    }

    // Организация мероприятия с бронированием номеров и заказом оборудования
    public void OrganizeEvent(int conferenceRoomId, int[] roomIds, string equipment)
    {
        _eventManagementSystem.BookConferenceRoom(conferenceRoomId);
        _eventManagementSystem.OrderEquipment(equipment);
        foreach (var roomId in roomIds)
        {
            _roomBookingSystem.BookRoom(roomId);
        }
    }

    // Бронирование стола с вызовом такси
    public void BookTableWithTaxi(int tableId)
    {
        _restaurantSystem.BookTable(tableId);
        _restaurantSystem.OrderTaxi();
    }

    // Отмена бронирования комнаты
    public void CancelRoomBooking(int roomId)
    {
        _roomBookingSystem.CancelRoomBooking(roomId);
    }

    // Организация уборки по запросу
    public void RequestCleaning(int roomId)
    {
        _cleaningService.RequestCleaning(roomId);
    }
}
class Program
{
    static void Main(string[] args)
    {
        HotelFacade hotel = new HotelFacade();

        // 1. Бронирование номера с услугами ресторана и уборки
        hotel.BookRoomWithServices(101, "Pizza");

        // 2. Организация мероприятия с бронированием оборудования и номеров
        hotel.OrganizeEvent(1, new int[] { 101, 102 }, "Projector");

        // 3. Бронирование стола в ресторане с вызовом такси
        hotel.BookTableWithTaxi(5);

        // 4. Отмена бронирования номера
        hotel.CancelRoomBooking(101);

        // 5. Запрос на уборку номера
        hotel.RequestCleaning(102);
    }
}*/

//2 Composite
public abstract class OrganizationComponent
{
    protected string name;

    public OrganizationComponent(string name)
    {
        this.name = name;
    }

    public abstract void DisplayStructure(int indent = 0);
    public abstract double GetBudget();
    public abstract int GetEmployeeCount();
}
public class Employee : OrganizationComponent
{
    private string position;
    private double salary;

    public Employee(string name, string position, double salary) : base(name)
    {
        this.position = position;
        this.salary = salary;
    }

    public override void DisplayStructure(int indent = 0)
    {
        Console.WriteLine(new String(' ', indent) + $"{name} - {position} (${salary})");
    }

    public override double GetBudget()
    {
        return salary;
    }

    public override int GetEmployeeCount()
    {
        return 1;
    }
}
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> components = new List<OrganizationComponent>();

    public Department(string name) : base(name) { }

    public void Add(OrganizationComponent component)
    {
        components.Add(component);
    }

    public void Remove(OrganizationComponent component)
    {
        components.Remove(component);
    }

    public override void DisplayStructure(int indent = 0)
    {
        Console.WriteLine(new String(' ', indent) + $"Department: {name}");
        foreach (var component in components)
        {
            component.DisplayStructure(indent + 2);
        }
    }

    public override double GetBudget()
    {
        return components.Sum(c => c.GetBudget());
    }

    public override int GetEmployeeCount()
    {
        return components.Sum(c => c.GetEmployeeCount());
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Создание сотрудников
        var employee1 = new Employee("John Doe", "Developer", 50000);
        var employee2 = new Employee("Jane Smith", "Manager", 80000);
        var employee3 = new Employee("Jack Black", "Analyst", 60000);

        // Создание департаментов
        var developmentDept = new Department("Development");
        var hrDept = new Department("HR");

        // Добавление сотрудников в департаменты
        developmentDept.Add(employee1);
        developmentDept.Add(employee3);
        hrDept.Add(employee2);

        // Создание головного офиса
        var headOffice = new Department("Head Office");
        headOffice.Add(developmentDept);
        headOffice.Add(hrDept);

        // Отображение структуры организации
        headOffice.DisplayStructure();

        // Расчет общего бюджета и количества сотрудников
        Console.WriteLine($"Total budget: ${headOffice.GetBudget()}");
        Console.WriteLine($"Total employees: {headOffice.GetEmployeeCount()}");
    }
}
