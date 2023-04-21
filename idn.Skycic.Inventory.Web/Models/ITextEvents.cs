using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace idn.Skycic.Inventory.Web.Models
{
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer  
        PdfContentByte cb;

        // we will put the final number of pages in a template  
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer  
        BaseFont bf = null;

        // This keeps track of the creation time  
        DateTime PrintTime = DateTime.Now;

        #region Fields  
        private string _header;
        #endregion

        #region Properties  
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(FontPath.FontTimeNewRomanPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var strChangeColor = System.Web.HttpContext.Current.Session["ChangeColor"];
                if (strChangeColor != null)
                {
                    if (strChangeColor.ToString() == "footerNew")
                    {
                        bf = BaseFont.CreateFont(FontPath.FontArialPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    }
                    else if (strChangeColor.ToString() == "footerTCG")
                    {
                        bf = BaseFont.CreateFont(FontPath.FontTimeNewRomanPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    }
                    else
                    {
                        // Dùng font đặc biệt hỏi lại Lan
                        if (!CUtils.IsNullOrEmpty(strChangeColor))
                        {
                            bf = BaseFont.CreateFont(FontPath.FontChakraPetchBold, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        }
                        
                    }
                }
                
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            var imageVienBackground = System.Web.HttpContext.Current.Session["ImageVienBackground"];

            if (!CUtils.IsNullOrEmpty(imageVienBackground))
            {
                string imageFilePath = imageVienBackground.ToString().Trim();
                var lm = 0f;
                var rm = 0f;
                var tm = 0f;
                var bm = 0f;

                //creating Image object
                Image imageFile = Image.GetInstance(imageFilePath);
                var pageWidth = document.PageSize.Width - (lm + rm);
                var pageHeight = document.PageSize.Height - (bm + tm);
                imageFile.SetAbsolutePosition(lm, bm);
                imageFile.ScaleToFit(pageWidth, pageHeight);

                //If you want to choose image as background then,
                imageFile.Alignment = Image.UNDERLYING;

                //Scale Percent of image. 
                //Here we have 50% in float is 50f
                imageFile.ScalePercent(100f);
                document.Add(imageFile);
            }

            
        }
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            // Gạch ngang
            //Move the pointer and draw line to separate footer section from rest of page 
            //cb.SetColorFill(new BaseColor(255, 214, 193));
            var strChangeColor = System.Web.HttpContext.Current.Session["ChangeColor"] == null ? "" : System.Web.HttpContext.Current.Session["ChangeColor"].ToString();
            if(strChangeColor == "" || (strChangeColor != "" && strChangeColor.ToString() != "footerHTC"))
            {
                cb.MoveTo(20, document.PageSize.GetBottom(55));
                cb.LineTo(document.PageSize.Width - 20, document.PageSize.GetBottom(55));
                cb.SetColorStroke(new BaseColor(255, 214, 193));
            }
            Font fontNormal = new Font(bf, 10f, Font.NORMAL, new BaseColor(173, 151, 108));
            Font fontItalic = new Font(bf, 9f, Font.ITALIC, new BaseColor(173, 151, 108));
            Font fontBold = new Font(bf, 10f, Font.BOLD, new BaseColor(173, 151, 108));
            Font fontBoldItalic = new Font(bf, 10f, Font.BOLDITALIC, new BaseColor(173, 151, 108));
            Font font8Normal = new Font(bf, 9f, Font.NORMAL, new BaseColor(173, 151, 108));
            Font font8Italic = new Font(bf, 9f, Font.ITALIC, new BaseColor(173, 151, 108));
            Font font8Bold = new Font(bf, 9f, Font.BOLD, new BaseColor(173, 151, 108));
            Font font8BoldItalic = new Font(bf, 9f, Font.BOLDITALIC, new BaseColor(173, 151, 108));
            if (strChangeColor != "")
            {
                cb.SetColorStroke(new BaseColor(17, 17, 17));
                fontNormal = new Font(bf, 10f, Font.NORMAL, new BaseColor(17, 17, 17));
                fontItalic = new Font(bf, 9f, Font.ITALIC, new BaseColor(17, 17, 17));
                fontBold = new Font(bf, 10f, Font.BOLD, new BaseColor(17, 17, 17));
                fontBoldItalic = new Font(bf, 10f, Font.BOLDITALIC, new BaseColor(17, 17, 17));
                //strDonVicc = "Đơn vị cung cấp Hóa đơn điện tử: Công ty Cổ Phần Đầu Tư và Công Nghệ idocNet - MST: 0104614692 - Điện thoại: 024 6682 9843";

                font8Normal = new Font(bf, 9.5f, Font.NORMAL, new BaseColor(17, 17, 17));
                font8Italic = new Font(bf, 9.5f, Font.ITALIC, new BaseColor(17, 17, 17));
                font8Bold = new Font(bf, 9.5f, Font.BOLD, new BaseColor(17, 17, 17));
                font8BoldItalic = new Font(bf, 9.5f, Font.BOLDITALIC, new BaseColor(17, 17, 17));
            }
            else
            {
                cb.SetColorStroke(new BaseColor(255, 214, 193));
            }
            cb.Stroke();

            //iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            //iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            //Create PdfTable object  
            PdfPTable pdfTab = new PdfPTable(1);
            //float[] headers = { 255 };  //Header Widths
            //pdfTab.SetWidths(headers);        //Set the pdf headers
            pdfTab.WidthPercentage = 100;
            pdfTab.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            //We will have to create separate cells to include image logo and 2 separate strings  

            var invoiceCode = System.Web.HttpContext.Current.Session["SessionInvoiceCode"];
            if (CUtils.IsNullOrEmpty(invoiceCode))
            {
                invoiceCode = "";
            }
            //Row 1  

            PdfPCell row1 = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingTop = 2,
                PaddingBottom = 3,
            };             
            Font fontBoldItalicColorRed = new Font(bf, 10f, Font.BOLDITALIC, new BaseColor(254, 0, 18));
            if(strChangeColor.ToString() == "footerTCG")
            {
                fontBoldItalicColorRed = new Font(bf, 9.5f, Font.BOLDITALIC, new BaseColor(254, 0, 18));
            }            
            Phrase phrase1 = new Phrase(
                "Đơn vị cung cấp Hóa đơn điện tử: Công ty Cổ Phần Đầu Tư và Công Nghệ idocNet",
                fontItalic
                );
            PdfPCell row2 = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingTop = 0,
                PaddingBottom = 5,
            };

            if (CUtils.IsNullOrEmpty(invoiceCode))
            {
                if (strChangeColor.ToString() == "footerNew" || strChangeColor.ToString() == "footerHTC")
                {
                    invoiceCode = "...............";
                }
                else if (strChangeColor.ToString() == "footerTCG")
                {
                    invoiceCode = "";
                }
            }
            Chunk chunkRow2Text1 = new Chunk(
                String.Format("{0}", "Mã tra cứu hóa đơn (Invoice Code)"),
                fontItalic);
            Chunk chunkRow2Text2 = new Chunk(
                String.Format(" {0}", invoiceCode),
                fontBold);
            var strTraCuu = ". Tra cứu hóa đơn điện tử tại website:";
            if (strChangeColor != "")
            {
                if (strChangeColor.ToString() == "footerNew")
                {
                    chunkRow2Text2 = new Chunk(
                        String.Format(" {0}", invoiceCode),
                        fontBoldItalic);
                    strTraCuu = ". Tra cứu tại website:";
                }
                else if (strChangeColor.ToString() == "footerHTC")
                {
                    chunkRow2Text2 = new Chunk(
                        String.Format(" {0}", invoiceCode),
                        font8BoldItalic);
                    strTraCuu = ". Tra cứu tại website:";
                }
                else if (strChangeColor.ToString() == "footerTCG")
                {
                    chunkRow2Text2 = new Chunk(
                        String.Format(" {0}", invoiceCode),
                        fontBold);
                    strTraCuu = ". Tra cứu tại website:";
                }
            }
            Chunk chunkRow2Text3 = new Chunk(
                String.Format("{0} ", strTraCuu),
                fontItalic);
            var hoadontvan = "http://www.hoadontvan.com";
            var linkhoadontvan = System.Web.HttpContext.Current.Session["HoaDonTVAN"];
            if (!CUtils.IsNullOrEmpty(linkhoadontvan))
            {
                hoadontvan = linkhoadontvan.ToString().Trim();
            }
            Chunk chunkRow2Text4 = new Chunk(
                hoadontvan,
                fontBoldItalicColorRed);

            if (strChangeColor.ToString() == "footerTCG")
            {
                chunkRow2Text4 = new Chunk(
                hoadontvan,
                fontBoldItalicColorRed);
            }
            Phrase phrase2 = new Phrase();
            phrase2.Add(chunkRow2Text1);
            phrase2.Add(chunkRow2Text2);
            phrase2.Add(chunkRow2Text3);
            phrase2.Add(chunkRow2Text4);

            row1.Border = 0;
            row2.Border = 0;

            row1.Phrase = phrase2;
            //// Dòng thứ 2
            if (strChangeColor != "")
            {
                if(strChangeColor.ToString() == "footerNew")
                {
                    Chunk chunkR2Text1 = new Chunk(
                String.Format("{0}", "Khởi tạo từ phần mềm Q-invoice - "),
                fontItalic);
                    Chunk chunkR2Text2 = new Chunk(
                       String.Format("{0}", "Công ty CP Đầu Tư và Công Nghệ idocNet "),
                       fontBoldItalic);
                    Chunk chunkR2Text3 = new Chunk(
                      String.Format("{0}", " - MST: "),
                      fontItalic);
                    Chunk chunkR2Text4 = new Chunk(
                      String.Format("{0}", "0104614692"),
                      fontBoldItalic);
                    Chunk chunkR2Text5 = new Chunk(
                      String.Format("{0}", " - ĐT: "),
                      fontItalic);
                    Chunk chunkR2Text6 = new Chunk(
                     String.Format("{0}", "024 6682 9843"),
                     fontBoldItalic);
                    Phrase phraseRow2 = new Phrase();
                    phraseRow2.Add(chunkR2Text1);
                    phraseRow2.Add(chunkR2Text2);
                    phraseRow2.Add(chunkR2Text3);
                    phraseRow2.Add(chunkR2Text4);
                    phraseRow2.Add(chunkR2Text5);
                    phraseRow2.Add(chunkR2Text6);
                    row2.Phrase = phraseRow2;
                }
                else if (strChangeColor.ToString() == "footerTCG")
                {
                    Chunk chunkR2Text1 = new Chunk(
                                    String.Format("{0}", "Đơn vị cung cấp Hóa đơn điện tử: "),
                                    fontItalic);
                    Chunk chunkR2Text2 = new Chunk(
                       String.Format("{0}", "Công ty Cổ Phần Đầu Tư và Công Nghệ idocNet "),
                       fontBold);
                    Chunk chunkR2Text3 = new Chunk(
                      String.Format("{0}", " - MST: "),
                      fontItalic);
                    Chunk chunkR2Text4 = new Chunk(
                      String.Format("{0}", "0104614692"),
                      fontBold);
                    Chunk chunkR2Text5 = new Chunk(
                      String.Format("{0}", " - Điện thoại: "),
                      fontItalic);
                    Chunk chunkR2Text6 = new Chunk(
                     String.Format("{0}", "024 6682 9843"),
                     fontBold);
                    Phrase phraseRow2 = new Phrase();
                    phraseRow2.Add(chunkR2Text1);
                    phraseRow2.Add(chunkR2Text2);
                    phraseRow2.Add(chunkR2Text3);
                    phraseRow2.Add(chunkR2Text4);
                    phraseRow2.Add(chunkR2Text5);
                    phraseRow2.Add(chunkR2Text6);
                    row2.Phrase = phraseRow2;
                }  
                else if(strChangeColor.ToString() == "footerHTC")
                {
                    Chunk chunkR2Text1 = new Chunk(
                    String.Format("{0}", "Khởi tạo từ phần mềm Q-invoice - "),
                    font8Italic);
                    Chunk chunkR2Text2 = new Chunk(
                       String.Format("{0}", "Công ty CP Đầu Tư và Công Nghệ idocNet "),
                       font8BoldItalic);
                    Chunk chunkR2Text3 = new Chunk(
                      String.Format("{0}", " - MST: "),
                      font8Italic);
                    Chunk chunkR2Text4 = new Chunk(
                      String.Format("{0}", "0104614692"),
                      font8BoldItalic);
                    Chunk chunkR2Text5 = new Chunk(
                      String.Format("{0}", " - ĐT: "),
                      font8Italic);
                    Chunk chunkR2Text6 = new Chunk(
                     String.Format("{0}", "024 6682 9843"),
                     font8BoldItalic);
                    Phrase phraseRow2 = new Phrase();
                    phraseRow2.Add(chunkR2Text1);
                    phraseRow2.Add(chunkR2Text2);
                    phraseRow2.Add(chunkR2Text3);
                    phraseRow2.Add(chunkR2Text4);
                    phraseRow2.Add(chunkR2Text5);
                    phraseRow2.Add(chunkR2Text6);
                    row2.Phrase = phraseRow2;
                }

            }
            else
            {
                row2.Phrase = phrase1;
            }
            pdfTab.AddCell(row1);
            pdfTab.AddCell(row2);

            pdfTab.TotalWidth = document.PageSize.Width - 40f;
            pdfTab.WidthPercentage = 100;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;      

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable  
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write  
            //Third and fourth param is x and y position to start writing  

            pdfTab.WriteSelectedRows(0, -1, 20, 55, writer.DirectContent);
            String text = "Trang " + writer.PageNumber + " / ";
            //Add paging to footer  
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 10);
                if (!CUtils.IsNullOrEmpty(strChangeColor))
                {
                    cb.SetColorFill(new BaseColor(17, 17, 17));
                }
                else
                {
                    cb.SetColorFill(new BaseColor(173, 151, 108));
                }
                
                cb.SetTextMatrix(document.PageSize.GetRight(105), document.PageSize.GetBottom(17));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 10);
                var right = document.PageSize.GetRight(105);
                cb.AddTemplate(footerTemplate, right + len, document.PageSize.GetBottom(17));
            }


        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);


            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 10);
            var strChangeColor = System.Web.HttpContext.Current.Session["ChangeColor"];
            if (!CUtils.IsNullOrEmpty(strChangeColor))
            {
                footerTemplate.SetColorFill(new BaseColor(17, 17, 17));
            }
            else
            {
                footerTemplate.SetColorFill(new BaseColor(173, 151, 108));
            }
            
            footerTemplate.SetTextMatrix(0, 0);
            //footerTemplate.ShowText((writer.PageNumber).ToString());
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}