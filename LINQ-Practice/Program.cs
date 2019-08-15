using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//******************************************************************************
// LINQ: stands for Language Integradted Query
//
// Purpose: Write valid queries against many targets. Targets include databases
// in-memory objects, XML.
//
// LINQ is similar to SQL, but it can work with data aside from databases.
//
// What I learned: How to initialize variables using LINQ with objects, how to
// convert an array to a list and list to an array, How to make a new collection
// by only taking certain attributes using select, Joins, Group Joins, Where,
// Orderby, select new.
//
//******************************************************************************
namespace LINQ_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryStringArray();

            QueryIntArray();

            QueryArrayList();

            QueryCollection();

            QueryAnimalData();
        }

        //**********************************************************************
        //QueryStringArray()
        //**********************************************************************

        // Dog Names with Spaces Query // String LINQ practice
        static void QueryStringArray()
        {
            string[] dogs = { "Brian Griffin", "Scooby Doo",
            "Sky Negron", "Old Yeller", "Lassie", "Snoopy",
            "Charlie B. Barkin" };

            // LINQ
            var dogNamesWithSpace = from dog in dogs
                            where dog.Contains(" ") // Only want Dogs names that contain a space
                            orderby dog descending // Reverse alphabetical order
                            select dog; // variable to be checked by (where) condition

            foreach (var dogName in dogNamesWithSpace)
            {
                Console.WriteLine(dogName);
            }

            Console.WriteLine();
        }

        //**********************************************************************
        //QueryIntArray()
        //**********************************************************************

        // Numbers Query // Integer LINQ practice
        static int[] QueryIntArray()
        {
            int[] numbers = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };

            // LINQ
            var greaterThan20 = from num in numbers
                                where num > 20
                                orderby num
                                select num;

            foreach (var num in greaterThan20)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine();

            // LINQ.OrderedEnumerable
            Console.WriteLine(greaterThan20.GetType());

            Console.WriteLine();

            // Converting array to list
            var listGreaterThan20 = greaterThan20.ToList<int>();

            // Converting list to array
            var arrayGreaterThan20 = listGreaterThan20.ToArray();

            // LINQ adding other integers, changing data, makes the query update
            numbers[0] = 100; // Query updates automatically

            // Show LINQ adding other integers
            foreach (var number in greaterThan20)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();


            return arrayGreaterThan20;
        }


        //**********************************************************************
        //QueryArrayList()
        //**********************************************************************

        static void QueryArrayList()
        {
            ArrayList Animals = new ArrayList()
            {
                new Animal
                {
                    Name = "Shrek",
                    Height = 60,
                    Weight = 330
                },

                new Animal
                {
                    Name = "Tiger",
                    Height = 50,
                    Weight = 150
                },

                new Animal
                {
                    Name = "Ms. Piggy",
                    Height = 40,
                    Weight = 200
                },

                new Animal
                {
                    Name = "Kong",
                    Height = 100,
                    Weight = 500
                }
            };

            // Have to convert array list into Enumerable for LINQ (or will get build error)
            var AnimalsEnum = Animals.OfType<Animal>();

            // LINQ
            var smallAnimals = from smAnimals in AnimalsEnum
                               where smAnimals.Height <= 50
                               orderby smAnimals.Name
                               select smAnimals;

            foreach(var smAnimal in smallAnimals)
            {
                Console.WriteLine(smAnimal);
            }

            Console.WriteLine();

        }

        //**********************************************************************
        //QueryCollection()
        //**********************************************************************

        // Big dogs
        static void QueryCollection()
        {
            var animalList = new List<Animal>()
            {
                new Animal
                {
                    Name = "Husky",
                    Height = 25,
                    Weight = 70
                },

                new Animal
                {
                    Name = "Australian Shepard",
                    Height = 26,
                    Weight = 65
                },

                new Animal
                {
                    Name = "Pomeranian",
                    Height = 7,
                    Weight = 7
                },
            };

            // LINQ
            var bigDogs = from dog in animalList
                          where (dog.Weight > 60) &&
                          (dog.Height >= 25)
                          orderby dog.Name
                          select dog;

            foreach (var dog in bigDogs)
            {
                Console.WriteLine(dog);
            }

            Console.WriteLine();
        }

        //**********************************************************************
        //QueryAnimalData()
        //**********************************************************************

        static void QueryAnimalData()
        {
            Animal[] animals =
            {
                  new Animal
                {
                    Name = "Husky",
                    Height = 25,
                    Weight = 70,
                    AnimalID = 1
                },

                new Animal
                {
                    Name = "Australian Shepard",
                    Height = 26,
                    Weight = 65,
                    AnimalID = 2
                },

                new Animal
                {
                    Name = "Pomeranian",
                    Height = 7,
                    Weight = 7,
                    AnimalID = 3
                }
            };

            Owner[] owners =
            {
                new Owner
                {
                    Name = "Elena Negron",
                    OwnerID = 1
                },

                new Owner
                {
                    Name = "Timothy Mendez",
                    OwnerID = 2
                },

                new Owner
                {
                    Name = "Andrew Yang",
                    OwnerID = 3
                }

            };

            // LINQ
            // Take only the name and height from Animals
            // in other words make new collection with only name and height
            var nameHeight = from a in animals
                             select new
                             {
                                 a.Name,
                                 a.Height
                             };

            Array arrNameHeight = nameHeight.ToArray();

            foreach(var nH in arrNameHeight)
            {
                Console.WriteLine(nH.ToString());
            }
            Console.WriteLine();





            // LINQ // Inner Join // Join base off equality of Animal ID and Owner ID
            var innerJoin = from animal in animals 
                            join owner in owners
                            on animal.AnimalID equals owner.OwnerID
                            select new
                            {
                                OwnerName = owner.Name, // for i.OwnerName returns name
                                AnimalName = animal.Name // Gives access to name
                            };

            foreach (var i in innerJoin)
            {
                Console.WriteLine("{0} owns {1}", i.OwnerName, i.AnimalName);
            }

            Console.WriteLine();






            // LINQ // Group Join // Store ownergroup in groupJoin
            var groupJoin = from owner in owners
                            orderby owner.OwnerID

                            join animal in animals
                            on owner.OwnerID equals animal.AnimalID

                            into ownerGroup

                            select new
                            {
                                Owner = owner.Name,

                                Animals = from owner2 in ownerGroup
                                          orderby owner2.Name
                                          select owner2
                            };

            foreach(var ownerGroup in groupJoin)
            {
                Console.WriteLine(ownerGroup.Owner); // Print Owner Name

                foreach(var animal in ownerGroup.Animals)
                {
                    Console.WriteLine("* " + animal.Name); // Print Animal
                }
            }



        }
    }
}
