using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver : IObserver
    {
        #region Fields

        private IObservable _numberGenerator;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(IObservable numberGenerator)
        {
            if (numberGenerator == null)
            {
                throw new ArgumentNullException(nameof(numberGenerator));
            }

            _numberGenerator = numberGenerator;

            QuickTippNumbers = new List<int>(6);

            // Beim NumberGenerator als Beobachter registrieren
            _numberGenerator.Attach(this);
        }

        #endregion

        #region Methods

        public void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            if ((1 <= number && number <= 45)
                && !QuickTippNumbers.Contains(number))
            {
                QuickTippNumbers.Add(number);
            }

            if (QuickTippNumbers.Count >= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got a full Quick-Tipp => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();

                DetachFromNumberGenerator();
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} => QuickTippObserver [{nameof(QuickTippNumbers)}='{String.Join(", ", QuickTippNumbers.OrderBy(_ => _))}']";
        }

        private void DetachFromNumberGenerator()
        {
            _numberGenerator.Detach(this);
        }

        #endregion
    }
}
