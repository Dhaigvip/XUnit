using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

//https://github.com/xunit/samples.xunit

namespace XUnitTestProject1
{

    public class Person : IEquatable<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(Person other)
        {
            if (other == null) return false;
            return other.Id == this.Id && other.FirstName == this.FirstName && other.LastName == this.LastName;
        }
    }
    public static class DummyData
    {
        public static IEnumerable<Person> GetPersonList(string identifier)
        {
            List<Person> list = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Person() { Id = i, FirstName = $"{identifier}_FirstName", LastName = $"{identifier}LastName" });
            }
            return list;
        }
    }

    [Collection("Group1")] // Tests in same collection does not run parallel
    public class UnitTest1
    {
        private MyAssemblyFixture _fixture;
        public UnitTest1()
        {
        }
        

        [Fact]
        public void CompareList()
        {
            IEnumerable<Person> listOne = DummyData.GetPersonList("One");
            IEnumerable<Person> listTwo = DummyData.GetPersonList("Two");

            Assert.Equal(listOne, listTwo);
        }
        [Fact]
        public void CompareList2()
        {
            IEnumerable<Person> listOne = DummyData.GetPersonList("One");
            IEnumerable<Person> listTwo = DummyData.GetPersonList("One");

            //This test pass because person implements equal to compare data and not the object reference.
            Assert.Equal(listOne, listTwo);
        }
        [Fact]
        public void CollectionEquality()
        {
            List<int> left = new List<int>(new int[] { 4, 12, 16, 27 });
            List<int> right = new List<int>(new int[] { 4, 12, 16, 27 });

            Assert.Equal(left, right, new CollectionEquivalenceComparer<int>());
        }
        [Fact]
        public void EqualStringIgnoreCase()
        {
            string expected = "TestString";
            string actual = "teststring";

            Assert.False(actual == expected);
            Assert.NotEqual(expected, actual);
            Assert.Equal(expected, actual, StringComparer.CurrentCultureIgnoreCase);
        }
        [Fact]
        public void DateShouldBeEqualEvenThoughTimesAreDifferent()
        {
            DateTime firstTime = DateTime.Now.Date;
            DateTime later = firstTime.AddMinutes(90);

            Assert.NotEqual(firstTime, later);
            Assert.Equal(firstTime, later, new DateComparer());
        }
        /*
         * THEORY
         * Theories are tests which are only true for a particular set of data.
         */
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int num)
        {
            Assert.True(IsOdd(num));
        }

        private bool IsOdd(int num)
        {
            return num % 2 == 1;
        }



    }
}
