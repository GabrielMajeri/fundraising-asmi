using System;
using System.IO;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    public class DownloadContractModel : PageModel
    {
        private readonly AppDbContext _context;

        public DownloadContractModel(AppDbContext context)
        {
            _context = context;
        }

        public int ContractNumber { get; set; }
        public DateTime SigningDate { get; set; }
        public string CompanyName { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sponsorship = await _context.Sponsorships
                .Include(s => s.Company)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sponsorship == null)
            {
                return NotFound();
            }

            ContractNumber = sponsorship.Id;
            SigningDate = sponsorship.SigningDate;
            CompanyName = sponsorship.Company.Name;

            byte[] fileContents;
            using (var stream = new MemoryStream(32 * 1024))
            {
                WriteContract(stream);
                fileContents = stream.ToArray();
            }

            var contentType = "application/vnd.openxmlformats-officedocument.wordprocessing";
            var fileName = $"Contract {ContractNumber}.docx";
            return File(fileContents, contentType, fileName);
        }

        private void WriteContract(Stream stream)
        {
            using var document = WordprocessingDocument.Create(
                stream,
                WordprocessingDocumentType.Document
            );

            var mainPart = document.AddMainDocumentPart();

            mainPart.Document = new Document();
            var body = mainPart.Document.AppendChild(new Body());

            var titlePara = body.AppendChild(new Paragraph());
            var titleRun = titlePara.AppendChild(new Run());
            titleRun.RunProperties = new RunProperties()
            {
                Bold = new Bold(),
                FontSize = new FontSize()
                {
                    Val = "28",
                }
            };
            titleRun.AppendChild(new Text($"Contract Nr. {ContractNumber}"));

            var para = body.AppendChild(new Paragraph());
            var run = para.AppendChild(new Run());
            var date = SigningDate.ToString("dd.MM.yyyy");
            run.AppendChild(new Text($"Încheiat pe data de {date} între {CompanyName} și Asociația Studenților la Matematică și Informatică."));

            document.Save();
        }
    }
}
