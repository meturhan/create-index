using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace C_I
{
    //create_index
    class Program
    {
        static void Main(string[] args)
        {
            string field = args[2]; // hangi field
            int fieldSpan = 0; // bir kay�t i�erisinde indexlenecek field �ncesindeki alan 
            int PointerBlockSize = Convert.ToInt32(args[4]);
            long NumberOfRecords = Convert.ToInt64(args[3]);
            int totalSize = 0; // toplam bir kay�t boyutu
            string[,] FF = new FormatFile().readIt(args[1]);
            int PotF = 0; //Place of the Field
            for (; (PotF < (FF.Length/5)) && (FF[PotF, 0] != field); PotF++) 
                fieldSpan += Convert.ToInt32(FF[PotF, 2]);
            for (int i = 0; i < (FF.Length / 5); i++)
                totalSize += Convert.ToInt32(FF[i, 2]);
            fieldSpan += Convert.ToInt32(FF[PotF, 2]) - 4;
            BinaryReader Br = new BinaryReader(File.Open(args[0], FileMode.Open));
            Br.BaseStream.Seek(fieldSpan, SeekOrigin.Current);
            IndexStructure Idx = new IndexStructure(Br.ReadInt32(), PointerBlockSize);
            while ((Br.BaseStream.Position + totalSize) < (NumberOfRecords*totalSize))
            {
                Br.BaseStream.Seek(totalSize-4, SeekOrigin.Current);
                Idx.Add(Br.ReadInt32(), (Br.BaseStream.Position - (fieldSpan+4))/totalSize);
            }
            Idx.WriteIntoFile(args[5]);           
        }
    }
}

