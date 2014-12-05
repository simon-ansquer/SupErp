using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrarySalaire.ServiceSalaire;

namespace WpfControlLibrarySalaire.Helpers
{
    public static class PDFGenerator
    {

        private static Font titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD);
        private static Font subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        private static Font boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        private static Font endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
        private static Font bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);



        public static String OutputPath = @"C:\Users\Vincent del Valle\Desktop";
        public static bool generate(List<User> lstUser, string path)
        {
            DateTime now = DateTime.Now;

            string subPath = "BS-"+now.Month + "-" + now.Year;

            bool exists = System.IO.Directory.Exists(path + subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(path + subPath);

            foreach(User u in lstUser)
            {
                generate(u, path + subPath);
            }

            return true;
        }

        public static bool generate(User user, string path)
        {


           
            var document = new Document(PageSize.A4, 50, 50, 25, 25);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, new FileStream(path +"/BS-"+DateTime.Now.Month+"-"+DateTime.Now.Year+"-"+user.Firstname+"-"+user.Lastname+".pdf", FileMode.Create));

            document.Open();
            
            

            // création du logo
            Image logo = iTextSharp.text.Image.GetInstance("logo.png");
            logo.ScaleAbsoluteWidth(200);
            logo.ScaleAbsoluteHeight(50);
            PdfPCell cellLogo = new PdfPCell(logo);
            cellLogo.Border = Rectangle.NO_BORDER;
            cellLogo.PaddingBottom = 8;

            // création du titre
            var title = new Paragraph("BULLETIN DE SALAIRE", titleFont);
            title.Alignment = Element.ALIGN_RIGHT;
            PdfPCell cellTitle = new PdfPCell(title);
            cellTitle.Border = Rectangle.NO_BORDER;
            cellTitle.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right

            // création du tableau
            PdfPTable tableTitle = new PdfPTable(2);
            tableTitle.DefaultCell.Border = Rectangle.NO_BORDER;
            tableTitle.WidthPercentage = 100;

            PdfPCell cellAdresseSuperp = new PdfPCell(new Paragraph("89, quais des Chartrons \n33000 BORDEAUX", subTitleFont));
            cellAdresseSuperp.HorizontalAlignment = 0;
            cellAdresseSuperp.Border = Rectangle.NO_BORDER;

            PdfPCell cellDUMec = new PdfPCell(new Paragraph( user.Lastname + " " + user.Firstname + "\n" + user.Address, subTitleFont));
            cellDUMec.HorizontalAlignment = 2;
            cellDUMec.Border = Rectangle.NO_BORDER;

            // ajout de la cell du logo
            tableTitle.AddCell(cellLogo);

            // ajout de la cell du titre
            tableTitle.AddCell(cellTitle);

            tableTitle.AddCell(cellAdresseSuperp);
            tableTitle.AddCell(cellDUMec);

            // Ajout du titre principal
            tableTitle.AddCell(getTitle());

            Paragraph titleAbs = new Paragraph("Liste des abscences", titleFont);
            titleAbs.Alignment = Element.ALIGN_LEFT;
            PdfPCell cellTitleAbs = new PdfPCell(titleAbs);
            cellTitleAbs.PaddingBottom = 5;
            cellTitleAbs.Colspan = 2;
            cellTitleAbs.HorizontalAlignment = 0;
            cellTitleAbs.Border = Rectangle.NO_BORDER;
            tableTitle.AddCell(cellTitleAbs);



            PdfPTable tableAbs = new PdfPTable(3);
            tableAbs.AddCell("Début");
            tableAbs.AddCell("Fin");
            tableAbs.AddCell("Type");

            foreach (Absence a in user.Absences)
            {
                tableAbs.AddCell(a.StartDate.ToString());
                tableAbs.AddCell(a.EndDate.ToString());
                //tableAbs.AddCell(a.AbsenceType.ToString());
            }

            PdfPCell cellAbs = new PdfPCell(tableAbs);
            cellAbs.PaddingBottom = 5;
            cellAbs.Colspan = 2;
            cellAbs.HorizontalAlignment = 0;
            cellAbs.Border = Rectangle.NO_BORDER;
            tableTitle.AddCell(cellAbs);

            document.Add(tableTitle);





           

            document.Close();
            return true;
        }



        private static PdfPCell getTitle()
        {
            string[] mois = new string[]{"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"};

            Paragraph paragraph = new Paragraph(mois[DateTime.Now.Month - 1], titleFont);
            paragraph.Alignment = Element.ALIGN_CENTER;

            PdfPCell cell = new PdfPCell(paragraph);
            cell.PaddingBottom = 16;
            cell.PaddingTop = 16;
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

    }
}
