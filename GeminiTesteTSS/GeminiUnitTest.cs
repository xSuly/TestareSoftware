using ProiectTSS;
using System.Text;

namespace TesteTSS
{
    [TestClass]
    public class GeminiUnitTest
    {
        private JocVideo joc_ai;

        [TestInitialize]
        public void Setup()
        {
            joc_ai = new JocVideo("Witcher 3", "PC", 2015);
        }

        [TestMethod]
        public void ShouldAddValidFaza()
        {
            joc_ai.AdaugaFaza("Intro");
            Assert.AreEqual(1, joc_ai.Faze.Count);
        }

        [TestMethod]
        public void ShouldNotAddEmptyFaza()
        {
            joc_ai.AdaugaFaza("");
            Assert.AreEqual(0, joc_ai.Faze.Count);
        }

        [TestMethod]
        public void ShouldThrowForNullFaza()
        {
            Assert.ThrowsException<ArgumentNullException>(() => joc_ai.AdaugaFaza(null));
        }

        [TestMethod]
        public void ShouldAddPositiveScor()
        {
            joc_ai.AdaugaScor(100);
            Assert.AreEqual(1, joc_ai.Scoruri.Count);
        }

        [TestMethod]
        public void ShouldAddZeroScor()
        {
            joc_ai.AdaugaScor(0);
            Assert.AreEqual(1, joc_ai.Scoruri.Count);
        }

        [TestMethod]
        public void ShouldThrowForNegativeScor()
        {
            Assert.ThrowsException<ArgumentException>(() => joc_ai.AdaugaScor(-10));
        }

        [TestMethod]
        public void ShouldPrintNoFazeForEmptyList()
        {
            var consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOut));
            joc_ai.AfisareFaze();
            StringAssert.Contains(consoleOut.ToString(), "Fazele joc_aiului Witcher 3:");
            StringAssert.DoesNotMatch(consoleOut.ToString(), new System.Text.RegularExpressions.Regex("Faza \\d+:"));
        }

        [TestMethod]
        public void ShouldPrintFazeNames()
        {
            joc_ai.AdaugaFaza("Intro");
            joc_ai.AdaugaFaza("Level 1");
            var consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOut));
            joc_ai.AfisareFaze();
            StringAssert.Contains(consoleOut.ToString(), "Faza 1: Intro");
            StringAssert.Contains(consoleOut.ToString(), "Faza 2: Level 1");
        }

        [TestMethod]
        public void ShouldIndicateCompatiblePlatform()
        {
            var consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOut));
            joc_ai.VerificaCompatibilitate("pc");
            StringAssert.Contains(consoleOut.ToString(), "Witcher 3 este disponibil pe pc.");
        }

        [TestMethod]
        public void ShouldIndicateIncompatiblePlatform()
        {
            var consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(consoleOut));
            joc_ai.VerificaCompatibilitate("Playstation");
            StringAssert.Contains(consoleOut.ToString(), "Witcher 3 nu este disponibil pe Playstation.");
        }

        [TestMethod]
        public void ShouldThrowForNullPlatform()
        {
            Assert.ThrowsException<ArgumentNullException>(() => joc_ai.VerificaCompatibilitate(null));
        }
    }
}
