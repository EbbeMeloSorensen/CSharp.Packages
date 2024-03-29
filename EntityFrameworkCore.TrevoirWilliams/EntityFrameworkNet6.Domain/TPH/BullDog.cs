﻿namespace EntityFrameworkNet6.Domain.TPH;

public class BullDog : Dog
{
    public int Height { get; set; }

    public override string ToString()
    {
        return $"{Id} Bulldog, Legs: {Legs}, Height: {Height}";
    }
}