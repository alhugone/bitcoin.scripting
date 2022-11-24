using System;

namespace Scripting.Faker
{
    public class Faker
    {
        private readonly Random _random = new();
        public int NextInt => _random.Next();
    }
}