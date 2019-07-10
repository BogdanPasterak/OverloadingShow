using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OverloadingShow
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 Enum, own data type, intelisense, readable, maintainable, typing mistake, dictionary<key, value>
            foreach (string species in Enum.GetNames(typeof(Species)))
            {
                //Console.WriteLine(species);
            }

            // 2 Overloading method supply, compile time, method signature, parameter list, order, number, type,
            Zoo zooDublin = new Zoo();
            zooDublin.Supply(new Animal() { Name = "Tom", Type = Species.Lion });
            zooDublin.Supply(new Animal[] {
                new Animal() { Name = "Bob", Type = Species.Tiger },
                new Animal() { Name = "Lee", Type = Species.Lion }
            });

            // 3 Overloading constructor Animal
            Animal FoxDodger = new Animal("Dodger", Species.Fox);

            // 4 Overloading operators '+'
            zooDublin += FoxDodger;

            // 5 Overloading index
            Console.WriteLine("Animal with ID 1 : " + zooDublin[1]);
            zooDublin[1] = FoxDodger;

            // 6 ? ternary conditional operator, int? nullable value, ?? null-coalescing operator
            Console.WriteLine("Dodger : " + (zooDublin["Dodger"] ?? FoxDodger));

            Console.WriteLine("Animals in Zoo");
            Console.WriteLine(zooDublin);

            
            Console.ReadKey();

        }

        enum Species { Unknown, Cat, Dog, Lion, Tiger, Wolf, Fox }


        class Zoo : List<Animal>
        {
            // constructor
            public Zoo()
            {
                Add(new Animal() { Name = "Leonardo", Type = Species.Lion });
            }

            // supply animal
            public void Supply(Animal animal)
            {
                Add(animal);
            }

            // supply animals (overloading) , 
            public void Supply(params Animal[] animals)
            {
                foreach (Animal animal in animals)
                {
                    Add(animal);
                }
            }

            // overload operator +
            public static Zoo operator +(Zoo zoo, Animal animal)
            {
                zoo.Add(animal);
                return zoo;
            }

            // overload index
            new public Animal this[int id]
            {
                get { return this.FirstOrDefault(a => a.ID == id); }
                set
                {
                    Animal changed = this.FirstOrDefault(a => a.ID == id);
                    int place = this.ToList().IndexOf(changed);
                    Remove(changed);
                    Insert(place, value);
                }
            }
            // overload index by name
            public Animal this[string name]
            {
                get { return this.FirstOrDefault(a => a.Name == name); }
                set
                {
                    Animal changed = this.FirstOrDefault(a => a.Name == name);
                    int place = this.ToList().IndexOf(changed);
                    Remove(changed);
                    Insert(place, value);
                }
            }

            // override ToString
            public override string ToString()
            {
                string answer = String.Empty;
                foreach (Animal animal in this.ToList())
                {
                    answer += animal + "\r\n";
                }
                return answer;
            }

        }


        class Animal
        {
            // counter to increment ID
            private static int nextId;
            // Property of Animal ( ID is auto incrementer , readonly)
            public int ID { get; private set; }
            public string Name { get; set; }
            public Species Type { get; set; }

            // constructor default with auto incremented ID
            public Animal()
            {
                ID = Interlocked.Increment(ref nextId);
            }
            // overloading constructor
            public Animal(string name, Species species) : this()
            {
                Name = name;
                Type = species;
            }

            public override string ToString()
            {
                return "Animal [ ID = " + ID + ", Name = " + Name + ", Species = " + Type + " ]";
            }
        }

    }
}
