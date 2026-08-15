using System;

namespace ButcePlanlayicisiWeb.Models
{
    public class ButceKaydi
    {
        public int Id { get; set; }

        public double Gelir { get; set; }

        public double Gider { get; set; }

        public string Kategori { get; set; } = "";

        public DateTime Tarih { get; set; }
    }
}
