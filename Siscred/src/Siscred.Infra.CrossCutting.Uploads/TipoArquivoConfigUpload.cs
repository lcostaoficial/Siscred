﻿using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Siscred.Infra.CrossCutting.Uploads
{
    public static class TipoArquivoConfigUpload
    {
        public const string DefaultPath = "~/Uploads/Anexos";

        public static int ByteSize = 1048576;

        public static int SizeInBytesMax => ByteSize * 10;

        public static IList<string> ValidExtensions => new List<string> { ".pdf", ".doc", ".docx" };

        public static bool ValidateExtension(HttpPostedFileBase file)
        {
            return ValidExtensions.Contains(Path.GetExtension(file.FileName));
        }

        public static bool ValidateSize(int sizeByte)
        {
            return sizeByte <= SizeInBytesMax;
        }
    }
}