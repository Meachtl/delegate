using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using NumberGenerator.Logic;

namespace NumberGenerator.Test
{
    [TestClass]
    public class ObserverTests
    {
        private const int SEED = 125;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NumberGenerator_AttachWithNullAsObserver_ShouldThrowArgumentNullException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();

            //Act
            numberGenerator.Attach(null);


            //Assert
            Assert.Fail("ArgumentNullException was expected!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NumberGenerator_DetachWithNullAsObserver_ShouldThrowArgumentNullException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();

            //Act
            numberGenerator.Detach(null);


            //Assert
            Assert.Fail("ArgumentNullException was expected!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NumberGenerator_ExplicitCallOfAttachASecondTime_ShouldThrowInvalidOperationException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 5);

            //Act
            numberGenerator.Attach(baseObserver);

            //Assert
            Assert.Fail("InvalidOperationException was expected!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NumberGenerator_DetachCalledTwiceWithIdenticalObserver_ShouldThrowInvalidOperationException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 5);

            //Act
            numberGenerator.Detach(baseObserver);
            numberGenerator.Detach(baseObserver);

            //Assert
            Assert.Fail("InvalidOperationException was expected!");
        }

        [TestMethod]
        public void NumberGenerator_CallDetach_ShouldWork()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 5);

            //Act
            try
            {
                numberGenerator.Detach(baseObserver);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BaseObserver_ConstructorWithNegativeCountOfNumbersToWaitFor_ShouldThrowArgumentException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);


            //Act
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, -45);

            //Assert
            Assert.Fail("ArgumentException was expected!");
        }

        [TestMethod]
        public void StatisticsObserver_EvaluateMinWith50Numbers_ShouldBe30()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 50);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualMin = statisticsObserver.Min;

            //Assert
            Assert.AreEqual(30, actualMin);
        }

        [TestMethod]
        public void StatisticsObserver_EvaluateMaxWith50Numbers_ShouldBe950()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 50);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualMax = statisticsObserver.Max;

            //Assert
            Assert.AreEqual(950, actualMax);
        }

        [TestMethod]
        public void StatisticsObserver_EvaluateSumWith5000Numbers_ShouldBe30()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 5000);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualSum = statisticsObserver.Sum;

            //Assert
            Assert.AreEqual(2486436, actualSum);
        }

        [TestMethod]
        public void StatisticsObserver_EvaluateAvgWith5000Numbers_ShouldBe497()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 5000);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualAvg = statisticsObserver.Avg;

            //Assert
            Assert.AreEqual(497, actualAvg);
        }

        [TestMethod]
        public void StatisticsObserver_CallToStringAfter5000Numbers_ShouldBeCorrectOutput()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 5000);
            numberGenerator.StartNumberGeneration();

            //Act
            string actualToStringOutput = statisticsObserver.ToString();

            //Assert
            Assert.AreEqual("BaseObserver [CountOfNumbersReceived='5000', CountOfNumbersToWaitFor='5000'] => StatisticsObserver [Min='1', Max='999', Sum='2486436', Avg='497']", actualToStringOutput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RangeObserver_ConstructorWithBiggerLowerRangeThanHigherRange_ShouldThrowArgumentException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);


            //Act
            RangeObserver statisticsObserver = new RangeObserver(numberGenerator, 5, 15, 10);

            //Assert
            Assert.Fail("ArgumentException was expected!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RangeObserver_ConstructorNegativeNumberOfHitsToWaitFor_ShouldThrowArgumentException()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);


            //Act
            RangeObserver statisticsObserver = new RangeObserver(numberGenerator, -2, 15, 10);

            //Assert
            Assert.Fail("ArgumentException was expected!");
        }

        [TestMethod]
        public void RangeObserver_EvaluateCountOfNumbersReceivedToGet50HitsWithin200And300_ShouldBe69()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            RangeObserver rangeObserver = new RangeObserver(numberGenerator, 5, 200, 300);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualCountOfNumbersReceived = rangeObserver.CountOfNumbersReceived;

            //Assert
            Assert.AreEqual(69, actualCountOfNumbersReceived);
        }

        [TestMethod]
        public void QuickTippObserver_CountOfNumbersToGetAQuickTipp_ShouldBe186()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);
            numberGenerator.StartNumberGeneration();

            //Act
            int actualCountOfNumbersReceived = quickTippObserver.CountOfNumbersReceived;

            //Assert
            Assert.AreEqual(186, actualCountOfNumbersReceived);
        }

        [TestMethod]
        public void QuickTippObserver_CalculateQuickTipp_ShouldBeCorrectNumbers()
        {
            //Arrange
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(0, SEED);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);
            numberGenerator.StartNumberGeneration();

            //Act
            List<int> actualQuickTippNumbers = quickTippObserver.QuickTippNumbers;

            //Assert
            CollectionAssert.AreEqual(new List<int>() { 1, 5, 23, 30, 33, 34 }, actualQuickTippNumbers.OrderBy(_=>_).ToArray());
        }
    }
}

