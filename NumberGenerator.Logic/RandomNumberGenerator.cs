using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Implementiert einen Nummern-Generator, welcher Zufallszahlen generiert.
    /// Es können sich Beobachter registrieren, welche über eine neu generierte Zufallszahl benachrichtigt werden.
    /// Zwischen der Generierung der einzelnen Zufallsnzahlen erfolgt jeweils eine Pause.
    /// Die Generierung erfolgt so lange, solange Beobachter registriert sind.
    /// </summary>
    public class RandomNumberGenerator
    {
        #region Constants

        private static readonly int DEFAULT_SEED = DateTime.Now.Millisecond;
        private const int DEFAULT_DELAY = 500;

        private const int RANDOM_MIN_VALUE = 1;
        private const int RANDOM_MAX_VALUE = 1000;

        #endregion

        #region Fields

        List<IObserver> _observers;

        Random rnd;

        public int Delay { get; }
        public int Seed { get; }

        public int RandomNumber { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        public RandomNumberGenerator() : this(DEFAULT_DELAY, DEFAULT_SEED)
        {
            //_observers = new List<IObserver>();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        public RandomNumberGenerator(int delay) : this(delay, DEFAULT_SEED)
        {           
        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        /// <param name="seed">Enthält die Initialisierung der Zufallszahlengenerierung.</param>
        public RandomNumberGenerator(int delay, int seed)
        {
            _observers = new List<IObserver>();

            Delay = delay;
            Seed = seed;

            rnd = new Random(seed);
        }

        #endregion

        #region Methods

        #region IObservable Members

        /// <summary>
        /// Fügt einen Beobachter hinzu.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher benachricht werden möchte.</param>
        public void Attach(IObserver observer)
        {

            if (observer != null)
            {
                if (_observers.Contains(observer))
                {
                    throw new InvalidOperationException("Observer kann nicht zwei Mal hinzugefügt werden!");
                }
                _observers.Add(observer);
            }
            else
            {
                throw new ArgumentNullException("Es können keine null-Werte übergeben werden!");
            }
        }

        /// <summary>
        /// Entfernt einen Beobachter.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher nicht mehr benachrichtigt werden möchte</param>
        public void Detach(IObserver observer)
        {
            if (observer != null)
            {
                if (_observers.Contains(observer))
                {
                    _observers.Remove(observer);
                }
                else
                {
                    throw new InvalidOperationException("Observer kann nicht zwei Mal gelöscht werden!");
                }
                
            }
            else
            {
                throw new ArgumentNullException("Es können keine null-Werte von der Liste entfernt werden!");
            }
        }

        /// <summary>
        /// Benachrichtigt die registrierten Beobachter, dass eine neue Zahl generiert wurde.
        /// </summary>
        /// <param name="number">Die generierte Zahl.</param>
        public void NotifyObservers(int number)
        {
            foreach (IObserver observer in _observers.ToList())
            {
                observer.OnNextNumber(number);
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format($">> NumberGenerator: Number generated: {RandomNumber}");
        }

        /// <summary>
        /// Started the Nummer-Generierung.
        /// Diese läuft so lange, solange interessierte Beobachter registriert sind (=>Attach()).
        /// </summary>
        public void StartNumberGeneration()
        {
            while (_observers.Count > 0)
            {
                RandomNumber = rnd.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE);
                Console.WriteLine(ToString());
                NotifyObservers(RandomNumber);
            }
        }

        #endregion
    }

}
