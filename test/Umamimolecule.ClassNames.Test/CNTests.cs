using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Umamimolecule.ClassNames.Test
{
    public class CNTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData(null, "", " ", "\t", "\n")]
        public void IgnoreNullOrWhitespaceValues(params object[] values)
        {
            CN.Create(values).ShouldBe(String.Empty);
        }

        [Theory]
        [InlineData(new string[] { "apple" }, "apple")]
        [InlineData(new string[] { "apple", "banana" }, "apple banana")]
        public void HandleStrings(object[] values, string expected)
        {
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleTuple()
        {
            var values = ("apple", true);
            var expected = "apple";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleTuples()
        {
            var values = new object[]
            {
                ("apple", true),
                ("banana", false),
                ("cherry", true)
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleTupleWithFunction()
        {
            var values = ("apple", new Func<bool>(() => true));
            var expected = "apple";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleTuplesWithFunctions()
        {
            var falseFunc = new Func<bool>(() => false);
            var trueFunc = new Func<bool>(() => true);

            var values = new object[]
            {
                ("apple", trueFunc),
                ("banana", falseFunc),
                ("cherry", trueFunc)
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleKeyValuePair()
        {
            var values = new KeyValuePair<string, bool>("apple", true);
            var expected = "apple";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleKeyValuePairs()
        {
            var values = new object[]
            {
                new KeyValuePair<string, bool>("apple", true),
                new KeyValuePair<string, bool>("banana", false),
                new KeyValuePair<string, bool>("cherry", true)
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleKeyValuePairWithFunction()
        {
            var values = new KeyValuePair<string, Func<bool>>("apple", new Func<bool>(() => true));
            var expected = "apple";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleKeyValuePairsWithFunctions()
        {
            var falseFunc = new Func<bool>(() => false);
            var trueFunc = new Func<bool>(() => true);

            var values = new object[]
            {
                new KeyValuePair<string, Func<bool>>("apple", trueFunc),
                new KeyValuePair<string, Func<bool>>("banana", falseFunc),
                new KeyValuePair<string, Func<bool>>("cherry", trueFunc)
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleDictionary()
        {
            var values = new Dictionary<string, bool>()
            {
                { "apple", true },
                { "banana", false },
                { "cherry", true },
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleDictionaryWithFunction()
        {
            var falseFunc = new Func<bool>(() => false);
            var trueFunc = new Func<bool>(() => true);

            var values = new Dictionary<string, Func<bool>>()
            {
                { "apple", trueFunc },
                { "banana", falseFunc },
                { "cherry", trueFunc },
            };
            var expected = "apple cherry";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleFunction()
        {
            var values = new Func<string>(() => "apple");
            var expected = "apple";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleFunctions()
        {
            var values = new Func<string>[]
            {
                () => "apple",
                () => null,
                () => "banana",
            };
            var expected = "apple banana";
            CN.Create(values).ShouldBe(expected);
        }

        [Fact]
        public void HandleAllCombinations()
        {
            var falseFunc = new Func<bool>(() => false);
            var trueFunc = new Func<bool>(() => true);

            var values = new object[]
            {
                "apple",
                null,
                new string[] { "banana", "cherry" },
                " ",
                ("durian", true),
                ("ignore", false),
                ("elderberry", trueFunc),
                ("ignore", falseFunc),
                "\t",
                new Dictionary<string, bool>()
                {
                    { "fig", true },
                    { "ignore", false }
                },
                new Dictionary<string, Func<bool>>()
                {
                    { "grape", trueFunc },
                    { "ignore", falseFunc }
                },
                new KeyValuePair<string, bool>("hackberry", true),
                new KeyValuePair<string, bool>("ignore", false),
                new KeyValuePair<string, Func<bool>>("imbe", trueFunc),
                new KeyValuePair<string, Func<bool>>("ignore", falseFunc),
            };
            var expected = "apple banana cherry durian elderberry fig grape hackberry imbe";
            CN.Create(values).ShouldBe(expected);
        }
    }
}
