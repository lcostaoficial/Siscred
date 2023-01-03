using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public class PdfExtension
    {
        public static byte[] Render(string html)
        {
            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();
                    using (var srHtml = new StringReader(html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                    }
                }
                data = ms.ToArray();
            }
            return data;
        }             

        public static MemoryStream MergeFilesMemoryStream(List<byte[]> list)
        {
            MemoryStream outStream = new MemoryStream();
            using (Document document = new Document())
            using (PdfCopy copy = new PdfCopy(document, outStream))
            {
                document.Open();
                foreach (var item in list)
                {
                    copy.AddDocument(new PdfReader(item));

                }
            }
            return outStream;
        }

        public static byte[] MergeFilesByte(List<byte[]> list)
        {
            Document document = new Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();
                int documentPageCounter = 0;
                for (int fileCounter = 0; fileCounter < list.Count; fileCounter++)
                {
                    PdfReader reader = new PdfReader(list[fileCounter]);
                    int numberOfPages = reader.NumberOfPages;
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);
                        pageStamp.AlterContents();
                        copy.AddPage(importedPage);
                    }
                    copy.FreeReader(reader);
                    reader.Close();
                }
                document.Close();
                return ms.GetBuffer();
            }
        }

        public static void ReturnPDF(byte[] contents, string attachmentFilename)
        {
            var response = HttpContext.Current.Response;
            try
            {
                if (!string.IsNullOrEmpty(attachmentFilename))
                {
                    response.ContentType = "application/pdf";
                    response.AddHeader("Content-Disposition", $"inline; filename={attachmentFilename}.pdf");
                }

                response.ContentType = "application/pdf";
                response.BinaryWrite(contents);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                response.End();
                response.Flush();
                response.Clear();
            }
        }
    }
}