using HotelChainApp.HotelChainApp_Domain;
using HotelChainApp_AccessDataBase;
using HotelChainApp_Domain;
using HotelChainApp_Domain.ForCreation;
using Microsoft.EntityFrameworkCore;

// طباعة رسالة للتأكد من بدء تشغيل البرنامج.
Console.WriteLine("huseen");

// إنشاء سياق قاعدة البيانات، وسيتم التخلص منه تلقائياً بعد انتهاء الكتلة.
using (HotelChainAppContext context = new HotelChainAppContext())
{
    // إنشاء فندق جديد بالبيانات الأساسية الخاصة به.
    Hotel h1 = new Hotel()
    {
        Name = "Foo",
        Address = "Allepo , Alforqan",
        Email = "huseenalialhsan2001@gmail.com",
        Phone = 0994285944,
    };

    // إضافة الفندق إلى قاعدة البيانات والتحقق من نجاح العملية.
    if (context.AddHotel(h1) == 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء موظف جديد ليتم ربطه بالفندق لاحقاً.
    Employee employee = new Employee()
    {
        FirstNmae = "ali",
        LastNmae = "alhsan",
        Title = "employee estqbale",
        HireDate = DateTime.Now,
        DateOfBirth = DateTime.Now,
        Email = "huseenalialhsan2002@gmail.com",
    };

    // إضافة الموظف إلى الفندق الذي يحمل الاسم Foo.
    if (context.AddEmployeeToHotelByName("Foo", employee) == 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء نوع غرفة جديد وتحديد سعر الإقامة وعدد الأسرّة.
    RoomType roomType = new RoomType()
    {
        TypeName = "vip",
        NightlyRate = 100,
        BedsCount = 100,
    };

    // إضافة نوع الغرفة ثم حفظ التغييرات في قاعدة البيانات.
    context.RoomTypes.Add(roomType);
    if (context.SaveChanges() == 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء غرفة جديدة وتحديد رقمها والطابق والوصف الخاص بها.
    Room room = new Room()
    {
        Description = "vip",
        RoomNumber = 105,
        Floor = 2,
    };

    // ربط الغرفة بنوع الغرفة vip والفندق Foo.
    if (context.AddRoom("vip", "Foo", room) == 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء نزيل جديد مع إضافة حجز ودفعة مالية مرتبطة به.
    Guest guest = new Guest()
    {
        FirstNmae = "Ahmad",
        LastNmae = "Abo Hmed",
        PassPortNumber = "A225#66FFF33300S",
        Phone = 0994285944,
        reservations = new List<Reservation>()
        {
            new Reservation()
            {
                CheckInDate = DateTime.UtcNow,
                CheckOutDate = null,
                EmployeeId = employee.Id,
                RoomId = room.Id,
                TotalPrice = 2000,
                payments = new List<Payment>()
                {
                    new Payment()
                    {
                        CreateDate = DateTime.UtcNow,
                        Amount = 2000,
                    }
                }
            }
        },
        DateOfBirth = DateTime.Now,
        Email = "mostafa@gmail.com",
    };

    // حفظ بيانات النزيل والحجز والدفعة المرتبطة به في قاعدة البيانات.
    if (context.AddGuest(guest) >= 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء حجز إضافي يبدأ من وقت التشغيل الحالي.
    Reservation reservation = new Reservation()
    {
        CheckInDate = DateTime.UtcNow,
    };

    // إضافة الحجز إلى الغرفة باستخدام رقم جواز سفر النزيل ورقم الغرفة.
    if (context.AddReservation("A225#66FFF33300S", 105, reservation) >= 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }

    // إنشاء دفعة مالية جديدة مع تسجيل تاريخ إنشائها.
    Payment payment = new Payment()
    {
        Amount = 2000,
        CreateDate = DateTime.UtcNow,
    };

    // إضافة الدفعة إلى الحجز المرتبط بالغرفة رقم 105.
    if (context.AddPayment(105, payment) == 1)
    {
        Console.WriteLine("Success");
    }
    else
    {
        Console.WriteLine("error");
    }
}


