using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.Web.Models
{
    public class Mst_SpecUI : Mst_Spec
    {
        public string BrandCode { get; set; }
    }
    public class RQ_Mst_SpecUI : RQ_Mst_Spec
    {
        public List<Mst_SpecImage_File> ListImages { get; set; }
        public List<Mst_SpecImage_File> ListImagesDelete { get; set; }

        public List<Mst_SpecImage_File> ListFiles { get; set; }
        public List<Mst_SpecImage_File> ListFilesDelete { get; set; }
    }
    public class Mst_SpecImage_File
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Src { get; set; }
        public string SrcLocal { get; set; }
        public string Base64 { get; set; }
        public string Status { get; set; }

        public string FlagPrimaryImage { get; set; }
    }

    public class Mst_SpecImageUI : Mst_SpecImage
    {
        public string SpecImagePath_Tem { get; set; }
    }

    public class Mst_SpecFilesUI : Mst_SpecFiles
    {
        public string SpecFilePath_Tem { get; set; }
    }

    public class MoveFiles
    {
        public string FolderUpload { get; set; }
        public string SourceFileName { get; set; }
        public string DestFileName { get; set; }
    }
}