using System;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

using Microsoft.FSharp;
using Example.Models;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

namespace Example.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1_1_StructuralEquality()
        {
            var c1 = new Cat("Molly", "8", 9); //Constructor of a Record Type takes all fields
            var c2 = new Cat("Molly", "8", 9);

            Assert.Equal(c1,c2);
        }

        [Fact]
        public void Test1_2_MutableField()
        {
            var d1 = new Dog("Billy","apple");
            var d2 = new Dog("Billy", "bacon");

            Assert.NotEqual(d1,d2);
            d1.LastThingEaten = "bacon";
            //d1.Name = "bob"; //note that this will not compile because it lacks a setter
            Assert.Equal(d1,d2);
        }

        [Fact]
        public void Test1_3_CliMutable_DefaultConstructor()
        {
            var b1 = new Bird("Bob", "9 years");
            var b2 = new Bird(); //Empty constructor is included

            Assert.NotEqual(b1, b2);
            b2.Name = "Bob";
            b2.Age = "9 years";
            Assert.Equal(b1, b2); //structural equality
        }

        [Fact]
        public void Test2_1_DiscriminatedUnion()
        {
            var b1 = SparrowSpecies.AmericanTree; //uses member to create a SparrowSpecies
            SparrowSpecies b2 = SparrowSpecies.Chipping;

            Assert.NotEqual(b1, b2);
            Assert.Equal(b1,SparrowSpecies.AmericanTree);
        }

        [Fact]
        public void Test2_2_DiscriminatedUnion()
        {
            var s1 = Shape.Point;
            var s2 = Shape.NewCircle(1.2);
            var s3 = Shape.NewRentengle(2.9, 1.1);

            var s4 = s2 as Shape.Circle;
            Assert.Equal(1.2,s4.Item); //access unnamed member

            var s5 = s3 as Shape.Rentengle;
            Assert.Equal(2.9, s5.length); //access named member

            Assert.Equal(Shape.NewCircle(1.2),s2); //structural equality

            Assert.True(s1.IsPoint); //check case

            //using switch to process cases
            //Note: cannot switch on cases without members because they are not compiled as classes. 
            double Area(Shape shape)
            {
                switch (shape)
                {
                    case Shape.Circle cir:
                        return Math.PI * cir.Item * cir.Item;
                    case Shape.Rentengle rect:
                        return rect.length * rect.width;
                    //case Shape.Point p: //Invalid
                    default:
                        return 0;
                }
            }
            
        }

        [Fact]
        public void Test3_1_Tuples()
        {
            var b1 = new Tuple<Double,Double>(0,0);

            var b2 = new DrawBox<Tuple<Double,Double>>(new Tuple<double, double, double>(0,0,0), new Tuple<double, double>(0,0));

            Assert.Equal(b1, b2.Shape); //structural equality
        }

        [Fact]
        public void Test4_1_Options()
        {
            var b1 = new PollResult(FSharpOption<int>.Some(1),FSharpOption<int>.None );

            var b2 = new PollResult(FSharpOption<int>.Some(1), FSharpOption<int>.Some(1));

            //The OptionModule is a static class providing static methods for interacting with Option types

            if (OptionModule.IsSome(b1.Yes)) //guarding access 
            {
                Assert.Equal(1,b1.Yes.Value); //safe to access Value
            }

            var n1 = OptionModule.ToNullable(b1.No); //convert to more idiomatic C#
            Assert.Null(n1);
            var n2 = OptionModule.ToNullable(b1.Yes);
            Assert.Equal(1,n2);

            Assert.NotEqual(b1, b2);
            
        }

        [Fact]
        public void Test5_1_Collections()
        {
            var m1 = new MegaType(
                new[] {0, 1},
                new FSharpMap<string, int>(new List<Tuple<string, int>> {new Tuple<string, int>("key", 10)}),
                new FSharpSet<int>(new[] {1}),
                new FSharpList<FSharpOption<int>>(new FSharpOption<int>(22), FSharpList<FSharpOption<int>>.Empty),
                new[] {new[] {0, 1}},
                new int[,] { }
            );

            Assert.True(MapModule.ContainsKey("key", m1.Maps));

            var newSet = SetModule.Add(2, m1.Sets); //does not mutate set
            Assert.NotEqual(newSet,m1.Sets);

            if (OptionModule.IsSome(ListModule.Head(m1.Lists)))
                Assert.Equal(22, ListModule.Head(m1.Lists).Value);

            //Lists are linked lists, immutable and have structural equality

            Assert.Equal(m1.Arrays,m1.ArrayofArrays[0]); //Structural equality

            m1.Arrays[0] = 1;
            Assert.Equal(m1.Arrays, new[] {1, 1}); //Arrays are mutable
        }
        // If you begin to use these types please explore the associated modules more on your own.
    }
}
