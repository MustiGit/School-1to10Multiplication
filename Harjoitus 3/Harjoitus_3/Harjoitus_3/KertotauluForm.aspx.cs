// Ohjelma, jolla voi harjoitella matematiikan 1-10 kertolaskuja.
// Tekijä: Sami Mustonen

using System;

namespace Kertotaulu
{
    public partial class KertotauluForm : System.Web.UI.Page
    {
        // Asetetaan muuttujat kerrottaville numeroille ja oikein/väärin -laskureille
        private int oikeinTulos;
        private int vaarinTulos;
        private int numero1;
        private int numero2;
        private int vanhaNumero1;
        private int vanhaNumero2;

        // Staattinen sessionin ulkopuolinen satunnaislukugeneraattori
        private static Random r = new Random();

        // Sessiokohtaisen muuttujan määrittely väärin menneiden vastauksien laskurille
        public int VaarinTulos
        {
            get
            {
                if (Session["VaarinTulos"] != null)
                {
                    return (int)Session["VaarinTulos"];
                }
                else
                {
                    return vaarinTulos;
                }
            }

            set
            {
                vaarinTulos = value;
                Session.Add("VaarinTulos", value);
            }
        }

        // Sessiokohtaisen muuttujan määrittely oikein menneiden vastauksien laskurille
        public int OikeinTulos
        {
            get
            {
                if (Session["OikeinTulos"] != null)
                {
                    return (int)Session["OikeinTulos"];
                }
                else
                {
                    return oikeinTulos;
                }
            }

            set
            {
                oikeinTulos = value;
                Session.Add("OikeinTulos", value);
            }
        }

        // Sessiokohtaisen muuttujan määrittely ensimmäiselle satunnaisluvulle
        public int Numero1
        {
            get
            {
                if (Session["Numero1"] != null)
                {
                    return (int)Session["Numero1"];
                }
                else
                {
                    return numero1;
                }
            }

            set
            {
                numero1 = value;
                Session.Add("Numero1", value);
            }
        }

        // Sessiokohtaisen muuttujan määrittely toiselle satunnaisluvulle
        public int Numero2
        {
            get
            {
                if (Session["Numero2"] != null)
                {
                    return (int)Session["Numero2"];
                }
                else
                {
                    return numero2;
                }
            }

            set
            {
                numero2 = value;
                Session.Add("Numero2", value);
            }
        }

        // Sessiokohtaisen muuttujan määrittely ensimmäiselle käytetylle luvulle
        public int VanhaNumero1
        {
            get
            {
                if (Session["VanhaNumero1"] != null)
                {
                    return (int)Session["VanhaNumero1"];
                }
                else
                {
                    return vanhaNumero1;
                }
            }

            set
            {
                vanhaNumero1 = value;
                Session.Add("VanhaNumero1", value);
            }
        }

        // Sessiokohtaisen muuttujan määrittely toiselle käytetylle luvulle
        public int VanhaNumero2
        {
            get
            {
                if (Session["VanhaNumero2"] != null)
                {
                    return (int)Session["VanhaNumero2"];
                }
                else
                {
                    return vanhaNumero2;
                }
            }

            set
            {
                vanhaNumero2 = value;
                Session.Add("VanhaNumero2", value);
            }
        }

        // uusiKysymys-metodi arpoo uudet luvut ja esittää käyttäjälle kysymyksen
        private void uusiKysymys()
        {
            // Määritellään kerrottavat numerot random-generaattorilla
            Numero1 = r.Next(1, 11);
            Numero2 = r.Next(1, 11);

            /* Varmistetaan, että samaa kertolaskua ei tule kaksi kertaa peräkkäin
                vertaamalla vanhoja numeroita uusiin */
            while ((VanhaNumero1 == Numero1) && (VanhaNumero2 == Numero2))
            {
                // Haetaan Numero1:lle uusi arvo
                Numero1 = r.Next(1, 11);
            }
            // Asetetaan kysymys käyttäjälle, lukuina edellä määritellyt numerot
            kysymysLabel.Text = "Paljonko on " + Numero1 + " * " + Numero2 + " ?";

            // Varmistetaan, että kursori on keskitetty vastauskenttään
            vastausKentta.Focus();

            // Otetaan kertaalleen käytetyt numerot talteen
            VanhaNumero1 = Numero1;
            VanhaNumero2 = Numero2;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {   // Kutsutaan uusiKysymys metodia, kun sivua ladataan ensimmäisen kerran.
                uusiKysymys();
            }
        }

        protected void vastaaButton_Click(object sender, EventArgs e)
        {
            // Varmistetaan, että kursori on keskitetty vastauskenttään
            vastausKentta.Focus();

            // Jos käyttäjän antama syöte ei ole tyhjä:
            if (!string.IsNullOrWhiteSpace(vastausKentta.Text))
            {
                // Luetaan vastausKenttä ja muutetaan parsella annettu luku int-muotoon
                String syote = vastausKentta.Text;
                int annettuLuku = Int32.Parse(syote);

                // Tehdään laskutoimitus ja jos vastaus meni oikein:
                if ((annettuLuku) == (Numero1 * Numero2))
                {
                    // Kerrotaan käyttäjälle, että vastaus meni oikein ja tyhjennetään vastauskenttä
                    viestiLabel.Text = "Oikein!";
                    vastausKentta.Text = "";

                    // Lisätään OikeinTulos laskuriin + 1 ja päivitetään pisteLabel
                    OikeinTulos++;
                    pisteLabel.Text = "Oikeita vastauksia: " + OikeinTulos.ToString() + "/" + (OikeinTulos + VaarinTulos).ToString();

                    // Kutsutaan uusiKysymys-metodia antamaan seuraavat kerrottavat numerot
                    uusiKysymys();

                }
                // Jos vastaus meni väärin:
                else
                {
                    // Kerrotaan käyttäjälle, että vastaus meni väärin ja tyhjennetään vastauskenttä
                    viestiLabel.Text = "Väärin, yritä uudestaan.";
                    vastausKentta.Text = "";

                    // Lisätään VaarinTulos laskuriin + 1 ja päivitetään pisteLabel
                    VaarinTulos++;
                    pisteLabel.Text = "Oikeita vastauksia: " + OikeinTulos.ToString() + "/" + (OikeinTulos + VaarinTulos).ToString();

                    // Varmistetaan, että kursori on keskitetty vastauskenttään
                    vastausKentta.Focus();
                }
            }
        }

        protected void seuraavaButton_Click(object sender, EventArgs e)
        {
            // Tehdään laskutoimitus ja kerrotaan käyttäjälle oikea vastaus
            viestiLabel.Text = "Oikea vastaus oli: " + (Numero1 * Numero2) + ". ";

            // Päivitetään VaarinTulos laskuria + 1 ja päivitetään pisteLabel
            VaarinTulos++;
            pisteLabel.Text = "Oikeita vastauksia: " + OikeinTulos.ToString() + "/" + (OikeinTulos + VaarinTulos).ToString();

            // Kutsutaan uusiKysymys-metodia antamaan seuraavat kerrottavat numerot
            uusiKysymys();
        }

        protected void naytaPisteetButton_Click(object sender, EventArgs e)
        {
            // Asetetaan pisteLabel näkyväksi ja varmistetaan, että pistetiedot ovat ajantasalla
            pisteLabel.Visible = true;
            pisteLabel.Text = "Oikeita vastauksia: " + OikeinTulos.ToString() + "/" + (OikeinTulos + VaarinTulos).ToString();

            // Piilotetaan 'Näytä pisteet' -näppäin ja tuodaan esiin 'Piilota pisteet' -näppäin.
            naytaPisteetButton.Visible = false;
            piilotaPisteetButton.Visible = true;

            // Varmistetaan, että kursori on keskitetty vastauskenttään
            vastausKentta.Focus();
        }

        protected void alustaButton_Click(object sender, EventArgs e)
        {
            // Tyhjennetään viestiLabel ja vastausKentta
            viestiLabel.Text = "";
            vastausKentta.Text = "";

            // Piilotetaan 'Piilota pisteet' näppäin ja pisteLabel, sekä tuodaan esiin 'Näytä pisteet' -näppäin
            piilotaPisteetButton.Visible = false;
            naytaPisteetButton.Visible = true;
            pisteLabel.Visible = false;

            // Nollataan OikeinTulos ja VaarinTulos laskurit
            OikeinTulos = 0;
            VaarinTulos = 0;

            // Kutsutaan uusiKysymys-metodia antamaan seuraavat kerrottavat numerot
            uusiKysymys();
        }

        protected void piilotaPisteetButton_Click(object sender, EventArgs e)
        {
            // Asetetaan pisteLabel näkymättömäksi
            pisteLabel.Visible = false;

            // Piilotetaan 'Piilota pisteet' -näppäin ja tuodaan esiin 'Näytä pisteet' -näppäin.
            piilotaPisteetButton.Visible = false;
            naytaPisteetButton.Visible = true;

            // Varmistetaan, että kursori on keskitetty vastauskenttään
            vastausKentta.Focus();
        }
    }
}