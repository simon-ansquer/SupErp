using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using WpfControlLibrarySalaire.ServiceSalaire;
using System.Linq;

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

            string subPath = "BS-" + now.Month + "-" + now.Year;

            bool exists = System.IO.Directory.Exists(path + subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(path + subPath);

            foreach (User u in lstUser)
            {
                generate(u, path + subPath);
            }

            return true;
        }

        private static Document document;

        public static bool generate(User user, string path)
        {



            document = new Document(PageSize.A4, 50, 50, 25, 25);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, new FileStream(path + "/BS-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + user.Firstname + "-" + user.Lastname + ".pdf", FileMode.Create));

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

            PdfPCell cellDUMec = new PdfPCell(new Paragraph(user.Lastname + " " + user.Firstname + "\n" + user.Address, subTitleFont));
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



            //******************************************************************************/
            //***********************   ABSENCES   *****************************************/
            //******************************************************************************/

            generateAbsences(tableTitle, user);

            generateTableSalary(user);




            document.Close();
            return true;
        }

        private static void generateTableSalary(User user){


            List<string[]> values = new List<string[]>();

            decimal taxe = .8m;
            decimal total = 0;
            
            // en-tête
            values.Add(new string[] { "Rubrique", "Montant (HT)", "Montant (TTC)" });

            decimal vraiSalaire = (decimal)user.Salaries.OrderByDescending(S => S.Date).First().NetSalary;

            // salaire
            values.Add(new string[] { "Salaire", vraiSalaire.ToString("#.00"), ((decimal)(vraiSalaire * taxe)).ToString("#.00") });
            total += vraiSalaire * taxe;


            values.Add(new string[] { " ", " ", " " });
            values.Add(new string[] { "Primes : ", " ", " " });


            // primes
            foreach(Prime P in user.Primes){

                if (P.EndDate.Value.Month >= DateTime.Now.Month && P.EndDate.Value.Year >= DateTime.Now.Year)
                {
                    values.Add(new string[] { "  " + P.Label, "-", ((decimal)P.Price).ToString("#.00") });
                    total += (decimal)P.Price;
                }
            }

            values.Add(new string[] { " ", " ", " " });

            // total
            values.Add(new string[] { "NET À PAYER", "", total.ToString("#.00") + " €" });




            PdfPTable table = new PdfPTable(3);

            bool first = true;

            foreach (string[] S in values)
            {

                PdfPCell cellA = new PdfPCell(new Paragraph(S[0]));
                PdfPCell cellB = new PdfPCell(new Paragraph(S[1]));
                PdfPCell cellC = new PdfPCell(new Paragraph(S[2]));

                if (first)
                {
                    cellA.Padding = 5;
                    cellA.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cellB.Padding = 5;
                    cellB.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cellC.Padding = 5;
                    cellC.BackgroundColor = BaseColor.LIGHT_GRAY;
                }
                else
                {
                    cellA.BorderColorTop = BaseColor.WHITE;
                    cellA.BorderColorBottom = BaseColor.WHITE;
                    cellB.BorderColorTop = BaseColor.WHITE;
                    cellB.BorderColorBottom = BaseColor.WHITE;
                    cellC.BorderColorTop = BaseColor.WHITE;
                    cellC.BorderColorBottom = BaseColor.WHITE;
                }

                table.AddCell(cellA);
                table.AddCell(cellB);
                table.AddCell(cellC);

                first = false;
            }

            table.WidthPercentage = 100;
            document.Add(table);
        }


        private static void generateAbsences(PdfPTable tableTitle, User user)
        {


            Paragraph titleAbs = new Paragraph("Total des abscences : " + user.Absences.Count, subTitleFont);
            titleAbs.Alignment = Element.ALIGN_LEFT;
            PdfPCell cellTitleAbs = new PdfPCell(titleAbs);
            cellTitleAbs.PaddingBottom = 5;
            cellTitleAbs.Colspan = 2;
            cellTitleAbs.HorizontalAlignment = 0;
            cellTitleAbs.Border = Rectangle.NO_BORDER;
            tableTitle.AddCell(cellTitleAbs);



            document.Add(tableTitle);
        }



        private static PdfPCell getTitle()
        {
            string[] mois = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

            Paragraph paragraph = new Paragraph(mois[DateTime.Now.Month - 1] + " " + DateTime.Now.Year, titleFont);
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
