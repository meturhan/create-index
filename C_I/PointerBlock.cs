using System;
using System.Collections.Generic;
using System.Text;

namespace C_I
{
    [Serializable()]

    //pointer blo�u yap�s�
    class PointerBlock
    {
        public long[] pointerList; // pointer dizisi
        public PointerBlock link; // varsa ta�ma bloklar�n�n g�stericisi
        int index = 1; // pointer blo�u i�erisindeki index
        int size; // pointer block size
        
        //constructor,parametre olarak boyutu ve ba�lang�� olarak gelen dosya pointer'�n� al�r
        public PointerBlock(int size,long firstPointer)
        {
            this.size = size;
            pointerList = new long[size];
            pointerList[0] = firstPointer;
            link = null;
        }
        
        //Pointer blo�una bir dosya g�stericisi ekler,ta�ma varsa
        //yeni bir blok olu�turarak �ncekiyle ba�lar
        public void Insert(long place)
        {
            if (index < size)
            {
                pointerList[index] = place;
                index++;
            }
            else
            {
                if (link == null)
                {
                    PointerBlock newPointerBlock = new PointerBlock(size, place);
                    link = newPointerBlock;
                }
                else
                {
                    link.Insert(place);
                }
            }
        }
    }
}

