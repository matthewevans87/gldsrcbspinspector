//using GldSrcBSPInspector.ConsoleApp.BSPFormat;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace GldSrcBSPInspector.ConsoleApp
//{
//    public class BSPFile
//    {



//        //private BSPLump LUMP_ENTITIES;
//        //private BSPLump LUMP_PLANES;
//        //private BSPLump LUMP_TEXTURES;
//        //private BSPLump LUMP_VERTICES;
//        //private BSPLump LUMP_VISIBILITY;
//        //private BSPLump LUMP_NODES;
//        //private BSPLump LUMP_TEXINFO;
//        //private BSPLump LUMP_FACES;
//        //private BSPLump LUMP_LIGHTING;
//        //private BSPLump LUMP_CLIPNODES;
//        //private BSPLump LUMP_LEAVES;
//        //private BSPLump LUMP_MARKSURFACES;
//        //private BSPLump LUMP_EDGES;
//        //private BSPLump LUMP_SURFEDGES;
//        //private BSPLump LUMP_MODELS;
//        //private BSPLump HEADER_LUMPS;


//        //public string Path { get; private set; }

//        //public BSPFile(string path)
//        //{
//        //    this.Path = path;
//        //    this.readAndInspectFile();
//        //}

//        //public int FileVersion { get; private set; }



//        //private void readAndInspectFile()
//        //{
//        //    using (FileStream fs = new FileStream(this.Path, FileMode.Open))
//        //    {
//        //        using (var br = new BinaryReader(fs))
//        //        {
//        //            // File Version
//        //            FileVersion = br.ReadInt32();

//        //            // Header Lumps
//        //            LUMP_ENTITIES = readBSPLump(br);
//        //            LUMP_PLANES = readBSPLump(br);
//        //            LUMP_TEXTURES = readBSPLump(br);
//        //            LUMP_VERTICES = readBSPLump(br);
//        //            LUMP_VISIBILITY = readBSPLump(br);
//        //            LUMP_NODES = readBSPLump(br);
//        //            LUMP_TEXINFO = readBSPLump(br);
//        //            LUMP_FACES = readBSPLump(br);
//        //            LUMP_LIGHTING = readBSPLump(br);
//        //            LUMP_CLIPNODES = readBSPLump(br);
//        //            LUMP_LEAVES = readBSPLump(br);
//        //            LUMP_MARKSURFACES = readBSPLump(br);
//        //            LUMP_EDGES = readBSPLump(br);
//        //            LUMP_SURFEDGES = readBSPLump(br);
//        //            LUMP_MODELS = readBSPLump(br);
//        //            HEADER_LUMPS = readBSPLump(br);
//        //        }
//        //    }

//        //}

//        //private BSPLump readBSPLump(BinaryReader br)
//        //{
//        //    BSPLump result;
//        //    result.nOffset = br.ReadInt32();
//        //    result.nLength = br.ReadInt32();
//        //    return result;
//        //}

//        //private TextureLump readTextureLump(BinaryReader br)
//        //{
//        //    var tl = new TextureLump();

//        //}


//    }

//}
