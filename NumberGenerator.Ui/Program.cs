/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 4ABIF
 *--------------------------------------------------------------
 *              Martin PECHAK 
 *--------------------------------------------------------------
 * Description: Visuelle Ausgabe
 *--------------------------------------------------------------
*/
using NumberGenerator.Logic;
using System;

namespace NumberGenerator.Ui
{
    class Program
    {
        static void Main()
        {
            // Zufallszahlengenerator erstelltn
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(250);

            // Beobachter erstellen
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 10);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 20);
            RangeObserver rangeObserver = new RangeObserver(numberGenerator, 5, 200, 300);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);

            // Nummerngenerierung starten
            numberGenerator.StartNumberGeneration();
            
            // Quick-Tipp sortieren
            quickTippObserver.QuickTippNumbers.Sort();

            // Resultat ausgeben
            Console.WriteLine();
            PrintResult(statisticsObserver, rangeObserver, quickTippObserver);            
        }
        /// <summary>
        /// Print result line
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="range"></param>
        /// <param name="quick"></param>
        private static void PrintResult(StatisticsObserver stat, RangeObserver range, QuickTippObserver quick)
        {
            Console.WriteLine("----------------------------------------  Result  -------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($">> StatisticsObserver: Received {stat.CountOfNumbersReceived:D5} numbers ===> Min='{stat.Min}', Max='{stat.Max}', Sum='{stat.Sum}', Avg='{stat.Avg}'.");
            Console.WriteLine($">> RangeObserver:      Received {range.CountOfNumbersReceived:D5} numbers ===> There where '{range.NumbersInRange}' numbers between '{range.LowerRange}' and '{range.UpperRange}'.");
            Console.WriteLine($">> QuickTippObserver:  Received {quick.CountOfNumbersReceived:D5} numbers ===> Quick-Tipp is {quick.QuickTippNumbers[0]}, {quick.QuickTippNumbers[1]}, {quick.QuickTippNumbers[2]}, {quick.QuickTippNumbers[3]}, {quick.QuickTippNumbers[4]}, {quick.QuickTippNumbers[5]}.");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
        }
    }
}
