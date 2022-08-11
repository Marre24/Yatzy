using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yatzy.Outcomes;

namespace YatzyTests.UnitTests
{
    [TestClass]
    public class OutcomeTests
    {
        [TestMethod]
        public void ThreePairGetValue_DiceContainsThreePair_ReturnsGreaterThanZero()
        {
            ThreePairs tp = new ThreePairs();
            List<int> dice = new List<int>() {4,4,6,6,3,3};

            int value = tp.GetValue(dice);

             
            Assert.IsTrue(value > 0);
        }

        [TestMethod]
        public void TripsGetValue_DiceContainsTrips_ReturnsGreaterThanZero()
        {
            Trips tp = new Trips();
            List<int> dice = new List<int>() { 4, 4, 4, 3, 5, 6 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 12);
        }

        [TestMethod]
        public void FourOfAKindGetValue_DiceContainsFourOfAKind_ReturnsGreaterThanZero()
        {
            FourOfAKind tp = new FourOfAKind();
            List<int> dice = new List<int>() { 4, 4, 4, 4, 5, 6 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 16);
        }

        [TestMethod]
        public void SmallStraightGetValue_DiceContainsSmallStraight_ReturnsGreaterThanZero()
        {
            SmallStraight tp = new SmallStraight();
            List<int> dice = new List<int>() { 1, 1, 2, 3, 4, 5 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 15);
        }

        [TestMethod]
        public void LargeStraightGetValue_DiceContainsLargeStraight_ReturnsGreaterThanZero()
        {
            LargeStraight tp = new LargeStraight();
            List<int> dice = new List<int>() { 1, 2, 3, 4, 5, 6 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 20);
        }

        [TestMethod]
        public void VillaGetValue_DiceContainsvilla_ReturnsGreaterThanZero()
        {
            Villa tp = new Villa();
            List<int> dice = new List<int>() {6,6,6,6,6,6 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 36);
        }

        [TestMethod]
        public void TowerGetValue_DiceContainstower_ReturnsGreaterThanZero()
        {
            Tower tp = new Tower();
            List<int> dice = new List<int>() { 6, 6, 5, 5, 5, 5 };

            int value = tp.GetValue(dice);


            Assert.IsTrue(value == 32);
        }
    }
}
