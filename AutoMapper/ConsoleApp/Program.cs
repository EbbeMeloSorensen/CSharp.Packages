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

            var animals = new List<Animal>
            {
                new Animal{ Legs = 2 },
                new Cat{ Legs = 4, Name = "Garfield"} 
            };

            animals.ForEach(a =>
            {
                var mapped = mapper.Map(a, a.GetType(), typeof(AnimalDto));
                Console.WriteLine(mapped.ToString());
            });
        }
    }
}