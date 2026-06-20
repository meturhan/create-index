using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace C_I
{
    //Format file'�n okundu�u classt�r.
    class FormatFile
    {
        //format file'� okuyarak bir string martisi �eklinde d�nd�r�r
        public string[,] readIt(string formatFile)
        {
            int boyut = new StreamReader(formatFile).ReadToEnd().Split('\n').Length; //matris boyutu
            string[] FF_TEMP;//ge�i�i sat�r tutan de�i�ken
            string[,] FF = new string[boyut,5]; // esas o�lan
            int i = 0; // s�tun say�c�
            StreamReader Sr = File.OpenText(formatFile);
            while(Sr.EndOfStream == false)
            {
                FF_TEMP = Sr.ReadLine().Split(',');
                for (int j = 0; j < FF_TEMP.Length; j++)
                    FF[i, j] = FF_TEMP[j];
                i++;
            }
            Sr.Dispose();
            return FF;
        }

    }
}

