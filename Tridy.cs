using System;

namespace CechDobrodruhu.Testy
{
    public class ClenCechu
    {
        public string Jmeno { get; set; }
        public int Zdravi { get; protected set; } = 100;
        public int Energie { get; protected set; } = 50;
        public int Uroven { get; protected set; } = 1;
        public bool JeAktivni { get; protected set; } = true;

        public ClenCechu(string jmeno) { Jmeno = jmeno; }

        public virtual void Trenuj(int pocet) => throw new NotImplementedException();
        public virtual void UtrzZraneni(int dmg) => throw new NotImplementedException();
        public virtual void Odpocivej() => throw new NotImplementedException();
    }

    public class Dobrodruh : ClenCechu
    {
        public string Povolani { get; set; }
        public int Zkusenosti { get; set; } = 0;

        public Dobrodruh(string jmeno, string povolani, Zbran zbran, Brneni brneni) : base(jmeno) { }

        public void PridejZkusenosti(int xp) => throw new NotImplementedException();
        public bool PouzijSchopnost() => throw new NotImplementedException();
    }

    public class Obchodnik : ClenCechu
    {
        public TypObchodu TypObchodu { get; set; }
        public bool MaSlevu { get; set; }
        public int PocetPredmetu { get; set; } = 10;

        public Obchodnik(string jmeno, TypObchodu typ, bool sleva) : base(jmeno) { }
        public Obchodnik(string jmeno, TypObchodu typ) : base(jmeno) { }

        public void Prodej(int pocet) => throw new NotImplementedException();
        public void DoplnZbozi(int pocet) => throw new NotImplementedException();
        public override void Odpocivej() => throw new NotImplementedException();
    }
}
