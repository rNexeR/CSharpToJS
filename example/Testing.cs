// using Text.Test.Test;
// using Nx;
// using Test2;
// public interface Nexer : Kevin, Kevin.Cantillano
// {
//     Nx sayHello(string[] msg);
// }
// namespace N1
// {
//     using Here;
//     namespace L2
//     {
//         using Here2;
//     }
// }
// namespace N2
// {
//     public class Nexer : Rodriguez
//     {
//         public int x1;
//         public int x, y, z;
//         public Nexer NewNexer()
//         {
//             if (x == 0)
//             {
//                 while (x <= 5)
//                 {
//                     ++x;
//                     x++;
//                 }
//             }
//         }
//         public Nexer(int x, Nexer nx) { }
//     }

//     public abstract class Rodriguez
//     {

//     }
// }
// namespace N3 { }
// namespace Hi
// {
//     public enum Hola
//     {
//         HOLA,
//         ADIOS,
//     }

//     public enum Hola2
//     {
//         HOLA,
//         ADIOS,
//     }

//     public interface Hola3 : Hola2, Hola1
//     {

//     }
// }

namespace Tests
{
    public class Node{
        int val;
        Node left;
        Node right;
        static int x = 6;
        
        public Node(int val){

        }

        public int SetVal(int x){

        }

    }

    namespace Testing{
        enum types{
            HOLA,
            ADIOS = 5,
            OTRO,
        }
    }

    public interface IAlgoMas {

    }

    public interface IAlgo : IAlgoMas{
        void SayHi();
        void SayHello(string greeting);
        void SayHello();
    }

    // public interface nuevo : IAlgo {
    //     void SayHi();
    // }


    public class Tree{
        Node root = null;
        int x = 5 + 5;
        Node temp = new Node(x);
        int val = new Node(x).SetVal(x + x);
        int y = 'a';
        int z = (int)3.5f;
        int w = y + 1;
        string a = null;
        int[] e = {1,2,3};
        // int[][] f = new int[][]{new int[]{1}, new int[]{2}};
        
        public Tree(){
            (x)++;
            ((int)x) = 5;
            (hola.adios).cambiarTodo();
            (hola.adios)++;
            ((Clase)child).funcion();
            a.x[a.x[0,0].y].setX( getY(x[fn(1,2)]));
            a.x.y;
            a[1,2];
            a[1][2];
            a = null;
            int[] a;
            int[][] b;
            int[,] c;
            h.Hi();
            int x = 5;
        }
    }

    public class ClaseAbstracta{
        private ClaseAbstracta(){

        }
        public ClaseAbstracta(int x){
            
        }
        /*public abstract void SayHello(int val);
        public abstract void SayHello(int[] val);
        public abstract void SayHello(string greeting);*/
        public void SayHi(){
            
        }

        public void hola(int a){

        }
    }

    public class X: IAlgo{
        public X(){

        }

        public X(int y){

        }

        public virtual void SayHi(){}
        public virtual void SayHello(){}
        public virtual void SayHello(string msg){}
    }

    public class Y : X{
        public Y(): base(){

        }

        public Y(int x): base(x){

        }

        public override void SayHi(){}
    }
}