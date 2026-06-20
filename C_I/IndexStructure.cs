using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace C_I
{
    [Serializable()] // bu s�n�f�n seri halde dosyaya yazd�r�labilece�ini belirtir
    //Index yap�s�n�n class�
    class IndexStructure
    {
        BinaryTreeNode Root; // 1.seviyede kullan�lan binary tree'nin k�k�n� tutar
        bool isAdded = false; //a�aca ekleme s�ras�nda eklenecek d���m�n eklenip eklenmedi�ini bildirir
        int PointerBlockSize; // pointer blo�unun boyutu

        //A�aca ekleme fonksiyonu (1.seviye)
        public void Add(int val,long place)
        {
            Add_Cycle(Root, val, place); //recursive olan bu fonksiyon �a��r�l�r
            isAdded = false;
            
        }

        //a�a� �zerinde arama yaparak bulunan node'un ba�lant�l� oldu�u ilk pointer blo�unu d�nd�r�r
        public PointerBlock Find(int val)
        {
            BinaryTreeNode temp = Root;
            while (val != temp.value)
            {
                if (val < temp.value && temp.left!= null)
                    temp = temp.left;
                else if(val > temp.value && temp.right!= null)
                    temp = temp.right;
                if (temp.right == null && temp.left == null)
                    return new PointerBlock(1, -1);
            }
            return temp.block;
        }

        //constructor,k�k de�erini ve pointerblocksize'� al�r
        public IndexStructure(int firstValue,int PointerBlockSize)
        {
            this.PointerBlockSize = PointerBlockSize;
            Root = new BinaryTreeNode(firstValue, new PointerBlock(PointerBlockSize, firstValue));            
        }

        //bo� constructor;dosyadan okuma fonksiyonunu direk �a��rabilmek i�in
        public IndexStructure()
        {
            //bo�
        }

        //a�aca node ekleyen recursive fonksiyon
        private void Add_Cycle(BinaryTreeNode currentRoot,int val,long place)
        {
            if (currentRoot == null)
                return;

            bool isRight = true;

            if (currentRoot.value > val)
            {
                Add_Cycle(currentRoot.left, val, place);
                isRight = false;
            }

            else if (currentRoot.value < val)
                Add_Cycle(currentRoot.right, val, place);

            else
            {
                currentRoot.block.Insert(place);
                isAdded = true;
            }
            if (isAdded == false)
            {
                BinaryTreeNode newNode = new BinaryTreeNode(val, new PointerBlock(PointerBlockSize, place));
                if (isRight == true)
                    currentRoot.right = newNode;
                else
                    currentRoot.left = newNode;
                isAdded = true;
            }
        }

        //Serialization i�lemi,parametre olarak yaz�lacak dosyan�n yolunu al�r
        public void WriteIntoFile(string file)
        {
            BinaryFormatter bf = new BinaryFormatter();            
            bf.Serialize(new FileStream(file, FileMode.OpenOrCreate),this);
        }

        //Deserialization i�lemi,parametre olarak okunacak dosyan�n yolunu al�r
        public IndexStructure ReadFromFile(string file)
        {
            BinaryFormatter bf = new BinaryFormatter();            
            return (IndexStructure)bf.Deserialize(new FileStream(file, FileMode.Open));
        }
    }
}

