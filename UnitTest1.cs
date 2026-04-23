using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CechDobrodruhu.Testy
{
    // Cvičné enumy, aby nám to vůbec netrhalo ruce a šlo to spustit/zkompilovat
    public enum Zbran { Mec, Luk, Hul, Dyky }
    public enum Brneni { Latove, Kozene, Latkove }
    public enum TypObchodu { Lektvary, Zbrane, Brneni, Jidlo }

    [TestClass]
    public class ClenCechuTesty
    {
        [TestMethod]
        public void NovyClen_MaSpravneVychoziHodnoty()
        {
            var pepa = new ClenCechu("Pepa");
            Assert.AreEqual("Pepa", pepa.Jmeno);
            Assert.AreEqual(100, pepa.Zdravi);
            Assert.AreEqual(50, pepa.Energie);
            Assert.AreEqual(1, pepa.Uroven);
            Assert.IsTrue(pepa.JeAktivni);
        }

        [TestMethod]
        public void ZmenaJmena_NaPrazdne_SeNeulozi()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.Jmeno = ""; 
            Assert.AreEqual("Pepa", pepa.Jmeno); // Zůstane původní
        }

        [TestMethod]
        public void ZmenaJmena_NaMocDlouhe_SeNeulozi()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.Jmeno = "PepaZDepaKteryJeMocDlouhy"; 
            Assert.AreEqual("Pepa", pepa.Jmeno);
        }

        [TestMethod]
        public void Trenink_PridaEnergii()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.Trenuj(20);
            Assert.AreEqual(70, pepa.Energie); // 50 + 20
        }

        [TestMethod]
        public void Trenink_DoMinusu_NicNeudela()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.Trenuj(-10);
            Assert.AreEqual(50, pepa.Energie);
        }

        [TestMethod]
        public void Trenink_NejdePres100Energie()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.Trenuj(1000); // zkusíme ho přetrénovat
            Assert.AreEqual(100, pepa.Energie);
        }

        [TestMethod]
        public void Zraneni_UbiraZdravi()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.UtrzZraneni(30);
            Assert.AreEqual(70, pepa.Zdravi);
        }

        [TestMethod]
        public void Zraneni_PodNulu_UmreAJeNeaktivni()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.UtrzZraneni(150); // dostane pecku za 150
            Assert.AreEqual(0, pepa.Zdravi); // zdraví nesmí být pod 0
            Assert.IsFalse(pepa.JeAktivni);
        }

        [TestMethod]
        public void Odpocinek_DoplniZdraviAEnergii()
        {
            var pepa = new ClenCechu("Pepa");
            pepa.UtrzZraneni(20); // zdravi klesne na 80
            // energie je v základu 50
            pepa.Odpocivej();
            Assert.AreEqual(90, pepa.Zdravi); // 80 + 10
            Assert.AreEqual(55, pepa.Energie); // 50 + 5
        }
    }

    [TestClass]
    public class DobrodruhTesty
    {
        [TestMethod]
        public void NovyDobrodruh_NastaviSeSpravne()
        {
            var aragorn = new Dobrodruh("Aragorn", "Hraničář", Zbran.Mec, Brneni.Kozene);
            Assert.AreEqual("Hraničář", aragorn.Povolani);
            Assert.AreEqual(0, aragorn.Zkusenosti);
        }

        [TestMethod]
        public void Povolani_SpatneZadane_SeNeulozi()
        {
            var aragorn = new Dobrodruh("Aragorn", "Hraničář", Zbran.Mec, Brneni.Kozene);
            aragorn.Povolani = "Kuchař"; // To v zadání není povolené
            Assert.AreEqual("Hraničář", aragorn.Povolani);
        }

        [TestMethod]
        public void Zkusenosti_PridaniKladnych_SePrictou()
        {
            var aragorn = new Dobrodruh("Aragorn", "Válečník", Zbran.Mec, Brneni.Latove);
            aragorn.PridejZkusenosti(50);
            Assert.AreEqual(50, aragorn.Zkusenosti);
        }

        [TestMethod]
        public void Zkusenosti_LevelUp_ZvysiUroven()
        {
            var aragorn = new Dobrodruh("Aragorn", "Válečník", Zbran.Mec, Brneni.Latove);
            aragorn.PridejZkusenosti(100); // level 1 stoji 100 xp
            Assert.AreEqual(2, aragorn.Uroven);
            Assert.AreEqual(100, aragorn.Zkusenosti); // Xp se nemazou
        }

        [TestMethod]
        public void Schopnost_KdyzJeEnergie_PovedeSeASebereEnergii()
        {
            var aragorn = new Dobrodruh("Aragorn", "Mág", Zbran.Hul, Brneni.Latkove);
            bool vysledek = aragorn.PouzijSchopnost();
            Assert.IsTrue(vysledek);
            Assert.AreEqual(40, aragorn.Energie); // 50 - 10
        }
    }

    [TestClass]
    public class ObchodnikTesty
    {
        [TestMethod]
        public void Obchodnik_PlnyKonstruktor_MaSlevu()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Lektvary, true);
            Assert.IsTrue(karel.MaSlevu);
            Assert.AreEqual(10, karel.PocetPredmetu);
        }

        [TestMethod]
        public void Obchodnik_KratsiKonstruktor_NemaSlevuVZakladu()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Zbrane);
            Assert.IsFalse(karel.MaSlevu);
        }

        [TestMethod]
        public void Prodej_Kusu_SniziPocetZbozi()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Jidlo);
            karel.Prodej(4);
            Assert.AreEqual(6, karel.PocetPredmetu); // 10 - 4
        }

        [TestMethod]
        public void Prodej_VetsiNezJeSkladem_VynulujeSkladAleNejdeDoMinusu()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Jidlo);
            karel.Prodej(20); // Chce prodat 20, ale má jen 10
            Assert.AreEqual(0, karel.PocetPredmetu);
        }

        [TestMethod]
        public void Doplneni_Zbozi_PridaNaSklad()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Jidlo);
            karel.DoplnZbozi(5);
            Assert.AreEqual(15, karel.PocetPredmetu);
        }

        [TestMethod]
        public void Odpocinek_Obchodnika_DoplniJenZdraviANeEnergii()
        {
            var karel = new Obchodnik("Karel", TypObchodu.Jidlo);
            karel.UtrzZraneni(20); // Zdravi 80
            karel.Odpocivej();
            Assert.AreEqual(85, karel.Zdravi); // +5 zdravi podle zadani
            Assert.AreEqual(50, karel.Energie); // energie se nehne
        }
    }
}
