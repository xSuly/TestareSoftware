using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectTSS
{
    public class JocVideo
    {
        public string Nume { get; set; }
        public string Platforma { get; set; }
        public int AnulLansarii { get; set; }
        public List<string> Faze { get; set; } // Lista de faze ale jocului
        public List<int> Scoruri { get; private set; }

        public JocVideo(string nume, string platforma, int anulLansarii)
        {
            Nume = nume;
            Platforma = platforma;
            AnulLansarii = anulLansarii;
            Faze = new List<string>(); // Inițializare listă faze
            Scoruri = new List<int>();
        }

        public void AdaugaFaza(string faza)
        {
            Faze.Add(faza);
        }

        public void AdaugaScor(int scor)
        {
            if (scor < 0)
                throw new ArgumentException("Scorul nu poate fi negativ.");

            Scoruri.Add(scor);
        }

        // Funcția cu for - AfisareFaze()
        public void AfisareFaze()
        {
            Console.WriteLine($"Fazele jocului {Nume}:");
            for (int i = 0; i < Faze.Count; i++)
            {
                Console.WriteLine($"Faza {i + 1}: {Faze[i]}");
            }
        }

        // Funcția cu if-else - VerificaCompatibilitate()
        public void VerificaCompatibilitate(string platformaCautata)
        {
            if (platformaCautata == null)
            {
                throw new ArgumentNullException(nameof(platformaCautata), "Platforma cautata nu poate fi null.");
            }

            if (Platforma.ToLower() == platformaCautata.ToLower())
            {
                Console.WriteLine($"{Nume} este disponibil pe {platformaCautata}.");
            }
            else
            {
                Console.WriteLine($"{Nume} nu este disponibil pe {platformaCautata}.");
            }
        }

        public override string ToString()
        {
            return $"Nume: {Nume}, Platforma: {Platforma}, Anul Lansarii: {AnulLansarii}";
        }
    }

}
