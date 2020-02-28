using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeySkipWhileTests
    {
        [Test]
        public void skip_cards_is_normal()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
                new Card {Kind = CardKind.Separate},
            };

            // 讓func語意 同測試名稱
            var actual = cards.JoeySkip(card => card.Kind == CardKind.Normal);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
                new Card {Kind = CardKind.Separate},
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }
    }
}