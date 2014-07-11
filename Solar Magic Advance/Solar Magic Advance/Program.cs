using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Solar_Magic_Advance
{
    public static class LevelCard
    {
        //SMA4 Level Card Header
        public static string loaded_file;

        public struct header
        {
            public byte eCoin;
            public byte acecoins;
            public byte lvl_class;
            public byte lvl_num;
            public byte lvl_icon;

            public string lvl_name;
        }

        //SMA4 eCoin data
        public struct eCoin
        {
            public ushort[] pal;          //0x20 bytes
            public byte[] gfx;          //0x120 bytes
        }

        //SMA4 Room
        public struct room
        {
            public obj obj;
            public set set;
            public static List<warp> warps;
            public static List<spr> spr;
            public blkpth blkpth;
            public static List<auto_scroll> auto_scroll;
        }

        //SMA4 Object Header
        public struct obj
        {
            public UInt16 timeLimit;
            public UInt16 unk1;
            public byte unk2;           //4-bit
            public byte roomlength;
            public byte bgColor;
            public byte scrollSet;      //4-bit
            public byte unk3;           //4-bit
            public byte entryAction;    //3-bit
            public byte gfxset1;        //5-bit
            public byte gfxset2;        //4-bit
            public byte unk4;           //4-bit
            public byte extColor;       //4-bit
            public byte extEffect;      //4-bit
            public byte bgGFX;

            public static List<objd> data;
        }

        //SMA4 Object Data
        public struct objd
        {
            public byte bank;
            public byte length;
            public byte y;
            public byte x;
            public byte id;
            public byte slength;

            public void edit(byte bank, byte id, byte x, byte y, byte length)
            {
                this.bank = bank;
                this.id = id;
                this.x = x;
                this.y = y;
                this.length = length;
            }

            public void edit(byte bank, byte id, byte x, byte y, byte length, byte slength)
            {
                this.bank = bank;
                this.id = id;
                this.x = x;
                this.y = y;
                this.length = length;
                this.slength = slength;
            }
        }

        //SMA4 Room Settings
        public struct set
        {
            public UInt16 scr_y_bound;
            public UInt16 scr_centerYfix;
            public UInt16 scr_centerYply;
            public byte scr_distYmin;
            public byte scr_distYmax;
            public byte player_Y;
            public byte player_X;
            public byte screen_Y;
            public byte screen_X;
            public UInt16 objset;
            public UInt16 music;
            public byte gfxset1;
            public byte gfxset2;
            public byte gfxset3;
            public byte gfxset4;
            public byte gfxset5;
            public byte gfxset6;
            public UInt16 unk1;
            public UInt16 unk2;
            public byte unk3;
            public byte unk4;
            public byte unk5;
            public byte unk6;
            public byte unk7;
            public byte unk8;
        }

        //SMA4 Warp Data
        public struct warp
        {
            public byte srcX;
            public byte srcY;
            public byte roomdst;
            public byte unk1;
            public byte dstX;
            public byte dstY;
            public byte center_scrX;
            public byte center_scrY;
            public byte unk2;
            public byte exit_type;
            /*
                0 - Appear at X, Y (used for Doors)
                1 - Come up from pipe
                2 - Come down from pipe
                3 - Come right from pipe
                4 - Come Left from pipe
                5 - Drop from X, Y
             */
        }

        //SMA4 Sprite
        public struct spr
        {        
	        public byte bank;
	        public byte id;
	        public byte x;
	        public byte y;
	        public byte p1;
            public byte p2;
	
	        public void edit(byte bank, byte id, byte x, byte y)
	        {
		        this.bank = bank;
		        this.id = id;
		        this.x = x;
		        this.y = y;
	        }
	
	        public void edit(byte bank, byte id, byte x, byte y, byte p1)
	        {
		        this.bank = bank;
		        this.id = id;
		        this.x = x;
		        this.y = y;
		        this.p1 = p1;
	        }
	
	        public void edit(byte bank, byte id, byte x, byte y, byte p1, byte p2)
	        {
		        this.bank = bank;
		        this.id = id;
		        this.x = x;
		        this.y = y;
		        this.p1 = p1;
		        this.p2 = p2;
	        }

            public byte size()
            {
                return getSpriteSize(this.bank, this.id);
            }
        }

        //SMA4 Block Path Header
        public struct blkpth
        {
            public bool init_direction;     //false = Right; true = Left
            public byte length;             //4-bit
            public byte speed;              //3-bit

            public static List<blkpthd> data;
        }

        //SMA4 Block Path Data
        public struct blkpthd
        {
            public byte dist_blocks;        //6-bit (63 MAX)
            public byte direction;          //2-bit (0 = Right, 1 = Left, 2 = Up, 3 = Down)
        }

        //SMA4 Auto-Scroll
        public struct auto_scroll
        {
            public byte x;
            public byte y;
            public byte speed;
        }

        //------------------------------------Get stuff
        public static string get_eCoin_position(byte position)
        {
            //8 e-Coins per floor, 3 floors total, 8*3 = 24
            if (position == 0)
                return "No";
            else if (position <= 24)
            {
                int floor = (int)Math.Ceiling((double)(position/8));
                int pos = position - (floor*8);
                return floor.ToString() + "F, " + getOrdinal(pos);
            }
            else
                return "Invalid";
        }

        public static string getOrdinal(int num)
        {
            if ((num > 3) && (num < 21))
                return num.ToString() + "th";
            else
            {
                switch (num % 10)
                {
                    case 1:
                        return num.ToString() + "st";
                    case 2:
                        return num.ToString() + "nd";
                    case 3:
                        return num.ToString() + "rd";
                    default:
                        return num.ToString() + "th";
                }
            }
        }

        public static string getLevelSet(byte lvl_set)
        {
            if (lvl_set < levelSet.Length)
                return levelSet[lvl_set];
            else
                return "Invalid";
        }

        public static string getLevelIcon(byte lvl_icon)
        {
            if (lvl_icon < levelIcon.Length)
                return levelIcon[lvl_icon] + " (0x" + lvl_icon.ToString("X2") + ")";
            else
                return "Invalid" + " (0x" + lvl_icon.ToString("X2") + ")";
        }

        public static byte getSpriteSize(byte bank, byte id)
        {
            int fullID = (int)id ^ ((int)(bank << 8));
            if (fullID < spriteSize.Length)
                return spriteSize[fullID];
            else if (fullID == 0x01AB)
                return 5;
            else
                return 4;
        }

        public static string getLevelName(byte[] lvl_name)
        {
            string name = "";
            for (int i = 0; i < lvl_name.Length; i++)
            {
                if (lvl_name[i] == 0xFF)
                    break;
                name += USFont[lvl_name[i]];
            }
            return name;
        }

        //---------------------------------Arrays
        public static string[] USFont =
        {
            // # = Invalid
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            "#","#","'",",",".","#",

            "a","b","c","d","e","f","g","h","i","j","k","l","m",
            "n","o","p","q","r","s","t","u","v","w","x","y","z",

            "Ä","Ö","Ü","Â","À","Ç","É","È","Ê","Ë","Î","Ï","Ô","Œ",
            "Ù","Û","Á","Í","Ñ","Ó","Ú","Ì","Ò",
            "ä","ö","ü","ß","é","â","à","ç","è","ê","ë","î","ï","ô","œ",
            "ù","û","á","í","ñ","ó","ú","ì","ò",

            "°","[er]","[re]","e","¿","¡","a",
            "”","“","’","‘","«","»",

            "0","1","2","3","4","5","6","7","8","9",
            "…","#","„","“","‚","‘",

            "#","#","#","#","#","#","#","#","#","#",
            "#","#","#","#","#","#","#","#","#","#",
            "#","#","#","#","#","#","#","#","#","#",
            "#","#","#","#","#","#","#","#","#","#",
            "#","#","#","#","#","#","#","#","#","#",
            "#","#","#","#","#","#","#","#","#","#",
            "#","#",

            "[Mushroom]","⚘","★","●","♥",

            //Monospaced
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "N","O","P","Q","R","S","T","U","#","#",

            "?","!","-"," ",

            "⁰","¹","²","³","⁴","⁵","⁶","⁷","⁸","⁹",
            "₀","₁","₂","₃","₄","₅","₆","₇","₈","₉",

            //Monospaced (except e)
            "e","V","W","X","Y","Z",

            "[Promotional]","[Null]"
        };

        public static string[] JPFont =
        {
            "TODO"
        };

        public static byte[] spriteSize =
	    {
		    4,6,4,4,4,4,4,4,4,4,4,4,4,4,4,5,
		    4,4,4,4,4,4,4,4,4,4,4,4,6,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,5,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
		    4,4,4,4,4,4,4,4,4,5,5,4,4,4,4,4,
		    4,4,4,4,4,4,5,6,4,4,5,5,4,4,4,4,
		    4,4,4,5,4,4,4,4,4,4,4,6,4,4,4,5,
		    6,6,4,4,4,5,5,4,4,5,5,4,4,4,4,4,
		    4,4,4,4,4,5,5,6,6,5,5,4,4,5,5,5,
		    5,6,6,4,4,6,6,5,5,5,5,5,5,5,5,4,
		    4,4,5,6,6,4,6,4,4,4,6,6,4,6,6,5,
		    6,4,4,5,4,4,5,6,4,4,5,4,4,5,5,5,
		    5,5,5,5,5,5,5,4,4,4,4,5,5,5,5,5,
		    5,5,5,5,5,5,4,4,4,5,4,4,4,4,4,4,
		    4,5,5,5,5,5,5,5,5,5,5,5,6,5,5,5
	    };

        public static string[] levelSet =
        {
            "e","Star","Mushroom","Flower","Heart",
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            "Promotional"
        };

        public static string[] levelIcon =
        {
            "e+", "Star", "Desert", "Warp Zone", "Fortress", "Castle", "Tower", "Pyramid",
            "Toad House (Yellow)", "Ghost House", "Airship", "Tank", "Airship (Cannon)",
            "Airship (Small)", "Coin Ship", "Hand", "Cloud", "Plains", "Palm Tree", "Water",
            "Flower", "Ice", "Piranha Plant", "Volcano", "Skull", "Hammer Bro.", "Boomerang Bro.",
            "Sledge Bro.", "Fire Bro.", "Invalid", "Invalid", "No Icon"
        };

        //Level Card components
        public static header level_header;
        public static eCoin level_eCoin;
        public static List<room>[] roomData;
    }

    public static class Compression
    {
        //TODO
        /*
        void range()
        {

        }

        void compress()
        {

        }

        void decompress(Stream input)
        {

        }
        */
    }

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void InitLevel(Form1 _MainForm)
        {
            //TODO
            LevelCard.header header;
            header.eCoin = 0;
            header.acecoins = 0;
            header.lvl_class = 0;
            header.lvl_icon = 0;
            header.lvl_num = 0;
            header.lvl_name = "New Level Card";
            LevelCard.level_header = header;
        }

        public static void InitTreeView(Form1 _MainForm)
        {
            //Header
            _MainForm.treeView1.Nodes[0].Nodes[0].Text = "eCoin: ";
            _MainForm.treeView1.Nodes[0].Nodes[1].Text = "Ace Coins: ";
            _MainForm.treeView1.Nodes[0].Nodes[2].Text = "Level Set: ";
            _MainForm.treeView1.Nodes[0].Nodes[3].Text = "Level Number: ";
            _MainForm.treeView1.Nodes[0].Nodes[4].Text = "Level Icon: ";
            _MainForm.treeView1.Nodes[0].Nodes[5].Text = "Level Name: ";
        }

        public static void UpdateTreeView(Form1 _MainForm)
        {
            //Header
            _MainForm.treeView1.Nodes[0].Nodes[0].Text += LevelCard.get_eCoin_position(LevelCard.level_header.eCoin);
            _MainForm.treeView1.Nodes[0].Nodes[1].Text += LevelCard.level_header.acecoins.ToString();
            _MainForm.treeView1.Nodes[0].Nodes[2].Text += LevelCard.getLevelSet(LevelCard.level_header.lvl_class);
            _MainForm.treeView1.Nodes[0].Nodes[3].Text += LevelCard.level_header.lvl_num.ToString();
            _MainForm.treeView1.Nodes[0].Nodes[4].Text += LevelCard.getLevelIcon(LevelCard.level_header.lvl_icon);
            _MainForm.treeView1.Nodes[0].Nodes[5].Text += LevelCard.level_header.lvl_name;

            //Objects
        }

        public static void LoadLevel(Form1 _MainForm, string filename, Stream file)
        {
            //TODO
            //Load File (*.level)
            InitLevel(_MainForm);
            InitTreeView(_MainForm);
            LevelCard.loaded_file = filename;
            file.Seek(0, SeekOrigin.Begin);
            LevelCard.header header;

            //Load Header
            header.eCoin = (byte)file.ReadByte();
            header.acecoins = (byte)file.ReadByte();
            header.lvl_class = (byte)file.ReadByte();
            header.lvl_num = (byte)file.ReadByte();
            header.lvl_icon = (byte)file.ReadByte();

            file.Seek(0x40, SeekOrigin.Begin);
            if (header.eCoin != 0)
            {
                //Load eCoin
                LevelCard.eCoin eCoin;
                ushort[] pal = new ushort[16];
                byte[] gfx = new byte[0x120];

                for (int i = 0; i < pal.Length; i++)
                    pal[i] = (ushort)(file.ReadByte() | (file.ReadByte() << 8));

                for (int i = 0; i < gfx.Length; i++)
                    gfx[i] = (byte)file.ReadByte();

                eCoin.pal = pal;
                eCoin.gfx = gfx;

                LevelCard.level_eCoin = eCoin;

                file.Seek(0x180, SeekOrigin.Begin);
            }

                //Level Name
            byte[] name = new byte[21];
            for (int i = 0; i < 21; i++)
            {
                name[i] = (byte)file.ReadByte();
                if (name[i] == 0xFF)
                    break;
            }

            header.lvl_name = LevelCard.getLevelName(name);

            LevelCard.level_header = header;

            //Load Objects
            
            //Update TreeView
            UpdateTreeView(_MainForm);
        }
    }
}
