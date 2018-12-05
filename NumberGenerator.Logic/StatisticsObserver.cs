using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields
        private int _countRandomNumbers = 0;
        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg
        {
            get
            {
                return Sum / CountOfNumbersReceived;
            }
        }

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {
            Min = int.MaxValue;
            Max = int.MinValue;
            Sum = 0;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            // BaseObserver [CountOfNumbersReceived='5000', CountOfNumbersToWaitFor='5000'] => StatisticsObserver [Min='1', Max='999', Sum='2486436', Avg='497']
            return string.Format($"BaseObserver [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}'] => " +
                $"StatisticsObserver [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']");
        }

        public override void OnNextNumber(int number)
        {
            if (number < Min)
            {
                Min = number;
            }
            if (number > Max)
            {
                Max = number;
            }
            Sum += number;

            base.OnNextNumber(number);
        }

        #endregion
    }
}
