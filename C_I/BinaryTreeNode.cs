using System;
using System.Collections.Generic;
using System.Text;

namespace C_I
{
    [Serializable()] // seri olarak dosyaya yaz�labilir
    //binary tree nin her bir node'unun yap�s�
    class BinaryTreeNode
    {
        public int value; // tutulan de�er
        public BinaryTreeNode left; // sa�
        public BinaryTreeNode right; // sol
        public PointerBlock block; // ili�kili pointer blo�u

        //constructor,parametre olarak eklenecek de�eri ve ba�lanacak pointer blo�unu al�r
        public BinaryTreeNode(int value_T, PointerBlock block_T)
        {
            value = value_T;
            block = block_T;
            left = null;
            right = null;
        }
    }
}

