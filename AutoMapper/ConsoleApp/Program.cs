using AutoMapper;
using System;
using ConsoleApp.Basic;
using ConsoleApp.Polymorphic;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fun with AutoMapper");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Animal, AnimalDto>()
                    .Include<Cat, CatDto>();

                cfg.CreateMap<Cat, CatDto>();
            });

            var mapper = config.CreateMapper();

            var order = new Order {Name = "Bamse"};
            var dtoOrder = mapper.Map<OrderDto>(order);
            Console.WriteLine($"{dtoOrder.Name} (property from automapped object)");

            var animal = new Animal { Legs = 2 };
            var dtoAnimal = mapper.Map<AnimalDto>(animal);
            Console.WriteLine($"{dtoAnimal.Legs} (property from automapped object)");

            var cat = new Cat { Legs = 4, Name = "Garfield" };
            var dtoCat = mapper.Map<CatDto>(cat);
            Console.WriteLine($"{dtoCat.Legs}, {dtoCat.Name} (property from automapped object)");
        }
    }
}