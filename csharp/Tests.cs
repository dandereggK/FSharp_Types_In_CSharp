using System;
using Xunit;

using Microsoft.FSharp;
using Example.Models;

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
            var b2 = new Bird();

            Assert.NotEqual(b1, b2);
            b2.Name = "Bob";
            b2.Age = "9 years";
            Assert.Equal(b1, b2);
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

    }
}
