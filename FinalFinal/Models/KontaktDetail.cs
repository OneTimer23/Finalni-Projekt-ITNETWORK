namespace FinalFinal.Models
{
    public class KontaktDetail
    {
        public Kontakt Record { get; set; }
        public IEnumerable<Pojisteni> VypisPojisteni { get; set; }
    }
}
