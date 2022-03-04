using System;
using System.Collections.Generic;
using RevitCommon.Extensions;
using RevitCommon.ML;
using RevitCommon.ML.Datasets;
using RevitCommon.ML.ModelSelection;
using RevitCommon.ML.Neighbors;
using RevitCommon.Numerical.Matrix;


namespace ConsoleTest
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            List<Dog> ls = new List<Dog>();
            ls.Add(new Dog(1));
            ls.Add(new Dog(6));
            ls.Add(new Dog(3));
            ls.Add(new Dog(2));
            ls.Add(new Dog(8));

            Dog a = ls.MaxBy(n => n.Age,null);

            Console.WriteLine(a.Age);
        } 

        class Dog
        {
            public int Age;
            public Dog(int age)
            {
                Age = age;
            }
        }
        
    }
}
