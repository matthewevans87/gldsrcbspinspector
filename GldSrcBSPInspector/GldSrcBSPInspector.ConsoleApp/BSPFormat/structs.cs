using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GldSrcBSPInspector.ConsoleApp.BSPFormat
{

    public static class Constants
    {
        public const int LUMP_ENTITIES = 0;
        public const int LUMP_PLANES = 1;
        public const int LUMP_TEXTURES = 2;
        public const int LUMP_VERTICES = 3;
        public const int LUMP_VISIBILITY = 4;
        public const int LUMP_NODES = 5;
        public const int LUMP_TEXINFO = 6;
        public const int LUMP_FACES = 7;
        public const int LUMP_LIGHTING = 8;
        public const int LUMP_CLIPNODES = 9;
        public const int LUMP_LEAVES = 10;
        public const int LUMP_MARKSURFACES = 11;
        public const int LUMP_EDGES = 12;
        public const int LUMP_SURFEDGES = 13;
        public const int LUMP_MODELS = 14;
        public const int HEADER_LUMPS = 15;
    }

    public class BSPFile
    {
        public string Path { get; private set; }

        private Header Header { get; set; }
        public BSPFile(string path)
        {
            this.Path = path;
            Load();
        }

        public void Load()
        {
            this.Header = new Header();
            Header.loadFile(this.Path);
        }

        public int Version
        {
            get
            {
                return this.Header.Version;
            }
        }

        private List<Entity> _entities;
        public List<Entity> Entities
        {
            get
            {
                if(_entities == null)
                {
                    this._entities = this.Header.EntitiesLump.GetEntities().ToList();
                }

                return _entities;
            }
        }

        public List<string> WADs
        {
            get
            {
                var wads = new List<string>();
                var worldSpawn = this.Entities.Where(e => e.ClassName == "worldspawn").FirstOrDefault();
                if (null != worldSpawn)
                {
                    if (worldSpawn.Properties.ContainsKey("wad"))
                    {
                        wads = worldSpawn.Properties["wad"].Split(";").ToList();
                    }
                }
                return wads;
            }
        }
    }

    public class Header
    {
        public Int32 Version { get; private set; }
        public EntitiesLump EntitiesLump { get; private set; }
        public PlanesLump PlanesLump { get; private set; }

        public Header()
        {

        }

        public void loadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                this.Load(fs);
                if (this.Version != 30)
                {
                    throw new Exception("Not a valid GoldSource map");
                }
                this.LoadLumps(fs);
            }
        }

        private void Load(Stream s)
        {
            var br = new BinaryReader(s);
            Version = br.ReadInt32();
        }

        private void LoadLumps(Stream s)
        {
            var br = new BinaryReader(s);

            EntitiesLump = new EntitiesLump(
                startingPosition: br.ReadInt32(),
                length: br.ReadInt32(),
                stream: s
            );
            EntitiesLump.Load();


        }

        //#define LUMP_ENTITIES      0
        //#define LUMP_PLANES        1
        //#define LUMP_TEXTURES      2
        //#define LUMP_VERTICES      3
        //#define LUMP_VISIBILITY    4
        //#define LUMP_NODES         5
        //#define LUMP_TEXINFO       6
        //#define LUMP_FACES         7
        //#define LUMP_LIGHTING      8
        //#define LUMP_CLIPNODES     9
        //#define LUMP_LEAVES       10
        //#define LUMP_MARKSURFACES 11
        //#define LUMP_EDGES        12
        //#define LUMP_SURFEDGES    13
        //#define LUMP_MODELS       14
        //#define HEADER_LUMPS      15
    }

    public abstract class Lump
    {
        public int StartingPosition { get; private set; }
        public int Length { get; private set; }
        public Stream Stream { get; private set; }

        public Lump(int startingPosition, int length, Stream stream)
        {
            StartingPosition = startingPosition;
            Length = length;
            Stream = stream;
        }

        public virtual void Load()
        {
            Stream.Position = StartingPosition;
            Stream.Seek(Length, SeekOrigin.Current);
        }
    }

    public class EntitiesLump : Lump
    {
        public EntitiesLump(int startingPosition, int length, Stream stream) : base(startingPosition, length, stream)
        {
        }

        public string Content { get; private set; }
        public override void Load()
        {
            Stream.Position = StartingPosition;
            using (var sr = new StreamReader(Stream, Encoding.ASCII))
            {
                var buffer = new char[Length];
                sr.ReadBlock(buffer, 0, Length);
                Content = new string(buffer);
            }
        }

        public IEnumerable<Entity> GetEntities()
        {
            var entities = new List<Entity>();

            using (var sr = new StringReader(this.Content))
            {
                string line;
                Entity entity = new Entity();
                while ((line = sr.ReadLine()) != "\0")
                {
                    if (line == "{")
                    {
                        entity = new Entity();
                        continue;
                    }
                    else if (line == "}")
                    {
                        entities.Add(entity);
                        continue;
                    }
                    else
                    {
                        var pieces = line.Substring(1, line.Length - 2).Split("\" \"");
                        var key = pieces[0];
                        var value = pieces[1];
                        if (entity.Properties.ContainsKey(key))
                        {
                            entity.Properties[key] = value;
                        } else
                        {
                            entity.Properties.Add(key, value);
                        }

                        if (key == "classname")
                        {
                            entity.ClassName = value;
                        }
                    }

                }
            }
            return entities;
        }
    }

    public struct Vector3D
    {
        public float x, y, z;
    }

    public enum PlaneTypes
    {
        PLANE_X = 0,     // Plane is perpendicular to given axis
        PLANE_Y = 1,
        PLANE_Z = 2,
        PLANE_ANYX = 3,  // Non-axial plane is snapped to the nearest
        PLANE_ANYY = 4,
        PLANE_ANYZ = 5
    }

    public struct Plane
    {
        public Vector3D VNormal;
        public float Distance;
        public Int32 PlaneType;
    }

    public class PlanesLump : Lump
    {
        public PlanesLump(int startingPosition, int length, Stream stream) : base(startingPosition, length, stream)
        {
        }

        public string Content { get; private set; }
        public override void Load()
        {
            Stream.Position = StartingPosition;
            using (var br = new BinaryReader(Stream))
            {
                
            }

        }
    }


    public class Entity
    {
        public string ClassName { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public Entity()
        {
            this.Properties = new Dictionary<string, string>();
        }

    }

}
