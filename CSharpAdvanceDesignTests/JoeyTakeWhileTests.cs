using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyTakeWhileTests
    {
        [Test]
        public void take_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
            };

            var actual = JoeyTakeWhile(cards,
                card => card.Kind == CardKind.Normal);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        // 重點在你的if條件 必須符合 takewhile語意, 不能用負面, 不然抽出來會壞掉
        // 比較跟where 與 take的差別
        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards, Func<Card, bool> predicate)
        {
            var enumerator = cards.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var card = enumerator.Current;
                if (predicate(card))
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }
            }

        }
    }
}