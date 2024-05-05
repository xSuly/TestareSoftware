using ProiectTSS;

public class Meniu
{
    private List<JocVideo> jocuri = new List<JocVideo>();

    public void AfisareMeniu()
    {
        while (true)
        {
            Console.WriteLine("1. Afiseaza toate jocurile");
            Console.WriteLine("2. Adauga un joc nou");
            Console.WriteLine("3. Sterge un joc");
            Console.WriteLine("4. Afiseaza fazele unui joc");
            Console.WriteLine("5. Iesire");
            Console.Write("Alege o optiune: ");
            int optiune = Convert.ToInt32(Console.ReadLine());

            switch (optiune)
            {
                case 1:
                    AfisareJocuri();
                    break;
                case 2:
                    AdaugaJoc();
                    break;
                case 3:
                    StergeJoc();
                    break;
                case 4:
                    AfisareFazeJoc();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Optiune invalida! Incercati din nou.");
                    break;
            }
        }
    }

    private void AdaugaJoc()
    {
        Console.Write("Introdu numele jocului: ");
        string nume = Console.ReadLine();
        Console.Write("Introdu platforma: ");
        string platforma = Console.ReadLine();
        Console.Write("Introdu anul lansarii: ");
        int anulLansarii = Convert.ToInt32(Console.ReadLine());

        JocVideo joc = new JocVideo(nume, platforma, anulLansarii);

        Console.Write("Cate faze doresti sa adaugi? ");
        int numarFaze = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < numarFaze; i++)
        {
            Console.Write($"Introdu numele fazei {i + 1}: ");
            string numeFaza = Console.ReadLine();
            joc.AdaugaFaza(numeFaza);
        }

        jocuri.Add(joc);
        Console.WriteLine("Joc adaugat cu succes!");

        Console.Write("Doresti sa verifici compatibilitatea jocului cu o platforma? (da/nu) ");
        if (Console.ReadLine().ToLower() == "da")
        {
            Console.Write("Introdu platforma pentru verificare: ");
            string platformaVerificare = Console.ReadLine();
            joc.VerificaCompatibilitate(platformaVerificare);
        }
    }

    private void StergeJoc()
    {
        Console.Write("Introdu numele jocului de sters: ");
        string nume = Console.ReadLine();
        JocVideo jocDeSters = jocuri.Find(j => j.Nume == nume);
        if (jocDeSters != null)
        {
            jocuri.Remove(jocDeSters);
            Console.WriteLine("Joc sters cu succes!");
        }
        else
        {
            Console.WriteLine("Jocul nu a fost gasit!");
        }
    }

    private void AfisareJocuri()
    {
        foreach (var joc in jocuri)
        {
            Console.WriteLine(joc);
        }
    }


    private void AfisareFazeJoc()
    {
        Console.Write("Introdu numele jocului pentru care vrei sa vezi fazele: ");
        string numeJoc = Console.ReadLine();
        JocVideo joc = jocuri.Find(j => j.Nume.ToLower() == numeJoc.ToLower());
        if (joc != null)
        {
            joc.AfisareFaze();
        }
        else
        {
            Console.WriteLine("Jocul nu a fost gasit!");
        }
    }

    // Restul metodelor rămân neschimbate...
}
