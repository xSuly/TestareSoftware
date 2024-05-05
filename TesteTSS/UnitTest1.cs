using ProiectTSS;

namespace TesteTSS
{
    [TestClass]
    public class JocVideoTests
    {
        private JocVideo joc;

        [TestInitialize]
        public void Setup()
        {
            joc = new JocVideo("Witcher 3", "PC", 2015);
        }

        [TestMethod]
        public void Constructor_Test()
        {
            Assert.AreEqual("Witcher 3", joc.Nume);
            Assert.AreEqual("PC", joc.Platforma);
            Assert.AreEqual(2015, joc.AnulLansarii);
        }

        [TestMethod]
        public void AdaugaFaza_Test()
        {
            joc.AdaugaFaza("Tutorial");
            Assert.AreEqual(1, joc.Faze.Count);
            Assert.AreEqual("Tutorial", joc.Faze[0]);
        }

        [TestMethod]
        public void AfisareFaze_Test()
        {
            joc.AdaugaFaza("Tutorial");
            joc.AdaugaFaza("Act 1");
            Assert.AreEqual(2, joc.Faze.Count);

            using (var consoleOutput = new ConsoleOutput())
            {
                joc.AfisareFaze();
                StringAssert.Contains(consoleOutput.GetOutput(), "Tutorial");
                StringAssert.Contains(consoleOutput.GetOutput(), "Act 1");
            }
        }

        [TestMethod]
        public void VerificaCompatibilitate_TestCompatibil()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.VerificaCompatibilitate("PC");
                StringAssert.Contains(consoleOutput.GetOutput(), "Witcher 3 este disponibil pe PC.");
            }
        }

        [TestMethod]
        public void VerificaCompatibilitate_TestIncompatibil()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.VerificaCompatibilitate("Xbox");
                StringAssert.Contains(consoleOutput.GetOutput(), "Witcher 3 nu este disponibil pe Xbox.");
            }
        }

        //teste pt equivalence partition
        [TestMethod]
        public void VerificaCompatibilitate_Compatibil_CazulSensitiv_Test()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.VerificaCompatibilitate("pc");
                StringAssert.Contains(consoleOutput.GetOutput(), "Witcher 3 este disponibil pe pc.");
            }
        }

        [TestMethod]
        public void VerificaCompatibilitate_Incompatibil_Test()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.VerificaCompatibilitate("PlayStation");
                StringAssert.Contains(consoleOutput.GetOutput(), "Witcher 3 nu este disponibil pe PlayStation.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerificaCompatibilitate_Null_Test()
        {
            joc.VerificaCompatibilitate(null); // Așteptăm o excepție de tip ArgumentNullException
        }

        [TestMethod]
        public void AdaugaScor_ScorZero_Test()
        {
            joc.AdaugaScor(0);
            Assert.AreEqual(0, joc.Scoruri[0]);
        }

        [TestMethod]
        public void AdaugaScor_ScorMaxim_Test()
        {
            joc.AdaugaScor(int.MaxValue);
            Assert.AreEqual(int.MaxValue, joc.Scoruri[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AdaugaScor_ScorNegativ_Test()
        {
            joc.AdaugaScor(-1);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var joc = new JocVideo("Cyberpunk 2077", "PC", 2020);

            // Act
            var result = joc.ToString();

            // Assert
            var expected = "Nume: Cyberpunk 2077, Platforma: PC, Anul Lansarii: 2020";
            Assert.AreEqual(expected, result, "Metoda ToString nu returnează formatul corect.");
        }

        // TESTE STRUCTURALE

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AdaugaFaza_NullInput_ThrowsException()
        {
            joc.AdaugaFaza(null);  // Așteptăm o excepție de tip ArgumentNullException
        }

        [TestMethod]
        public void AfisareFaze_ListaGoala_ContineDoarTitlul_Test()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.AfisareFaze();
                var output = consoleOutput.GetOutput().Trim();  // Elimină spațiile albe de la începutul și sfârșitul string-ului
                var expectedOutput = $"Fazele jocului {joc.Nume}:";

                Assert.AreEqual(expectedOutput, output, "Output-ul ar trebui să conțină doar titlul când lista de faze este goală.");
            }
        }

        [TestMethod]
        public void AdaugaScor_MultipleValidScores_Test()
        {
            joc.AdaugaScor(10);
            joc.AdaugaScor(20);
            joc.AdaugaScor(30);
            Assert.AreEqual(3, joc.Scoruri.Count);
            Assert.AreEqual(10, joc.Scoruri[0]);
            Assert.AreEqual(20, joc.Scoruri[1]);
            Assert.AreEqual(30, joc.Scoruri[2]);
        }

        [TestMethod]
        public void VerificaCompatibilitate_CaseInsensitive_Test()
        {
            using (var consoleOutput = new ConsoleOutput())
            {
                joc.VerificaCompatibilitate("pC");  // Testare cu caz mixt
                StringAssert.Contains(consoleOutput.GetOutput(), "Witcher 3 este disponibil pe pC.");
            }
        }

        [TestMethod]
        public void ToString_ExtremeValues_Test()
        {
            var jocExtrem = new JocVideo("", "", -1);
            var result = jocExtrem.ToString();
            var expected = "Nume: , Platforma: , Anul Lansarii: -1";
            Assert.AreEqual(expected, result, "Metoda ToString ar trebui să gestioneze și valorile extreme corect.");
        }
    }

    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
