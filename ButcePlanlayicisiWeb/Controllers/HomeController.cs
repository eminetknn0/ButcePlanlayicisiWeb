using Microsoft.AspNetCore.Mvc;
using ButcePlanlayicisiWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ClosedXML.Excel;

namespace ButcePlanlayicisiWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ButceDbContext _context;

        public HomeController(ButceDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kayitlar = _context.ButceKayitlari
                .OrderByDescending(x => x.Tarih)
                .ToList();

            return View(kayitlar);
        }

        [HttpPost]
        public IActionResult Ekle(ButceKaydi kayit)
        {
            _context.ButceKayitlari.Add(kayit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            var kayit = _context.ButceKayitlari
                .FirstOrDefault(x => x.Id == id);

            if (kayit != null)
            {
                _context.ButceKayitlari.Remove(kayit);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Filtrele(string kategori)
        {
            if (string.IsNullOrEmpty(kategori))
            {
                return RedirectToAction("Index");
            }

            var filtreliKayitlar = _context.ButceKayitlari
                .Where(x => x.Kategori == kategori)
                .ToList();

            return View("Index", filtreliKayitlar);
        }

        public IActionResult Raporlar()
        {
            var kayitlar = _context.ButceKayitlari.ToList();

            return View(kayitlar);
        }

        public IActionResult ExcelAktar()
        {
            var kayitlar = _context.ButceKayitlari
                .OrderBy(x => x.Tarih)
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Bütçe Raporu");

                worksheet.Cell(1, 1).Value = "Gelir";
                worksheet.Cell(1, 2).Value = "Gider";
                worksheet.Cell(1, 3).Value = "Kategori";
                worksheet.Cell(1, 4).Value = "Tarih";

                int satir = 2;

                foreach (var kayit in kayitlar)
                {
                    worksheet.Cell(satir, 1).Value = kayit.Gelir;
                    worksheet.Cell(satir, 2).Value = kayit.Gider;
                    worksheet.Cell(satir, 3).Value = kayit.Kategori;
                    worksheet.Cell(satir, 4).Value = kayit.Tarih;

                    satir++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var dosya = stream.ToArray();

                    return File(
                        dosya,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ButceRaporu.xlsx"
                    );
                }
            }
        }

        public IActionResult Duzenle(int id)
        {
            var kayit = _context.ButceKayitlari
                .FirstOrDefault(x => x.Id == id);

            if (kayit == null)
            {
                return RedirectToAction("Index");
            }

            return View(kayit);
        }

        [HttpPost]
        public IActionResult Duzenle(ButceKaydi kayit)
        {
            var mevcutKayit = _context.ButceKayitlari
                .FirstOrDefault(x => x.Id == kayit.Id);

            if (mevcutKayit != null)
            {
                mevcutKayit.Gelir = kayit.Gelir;
                mevcutKayit.Gider = kayit.Gider;
                mevcutKayit.Kategori = kayit.Kategori;
                mevcutKayit.Tarih = kayit.Tarih;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}