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
        
        public Node(){

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
        // void SayHi();
        //void SayHello(string greeting);
        // void SayHello();
    }

    public interface nuevo : IAlgo {
        void SayHi();
    }

    public class Tree{
        Node root = null;
        
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
        }
    }

    public class ClaseAbstracta: IAlgo{
        private ClaseAbstracta(){

        }
        public ClaseAbstracta(int x){
            
        }
        /*public abstract void SayHello(int val);
        public abstract void SayHello(int[] val);
        public abstract void SayHello(string greeting);*/
        public override void SayHi(){
            
        }

        public void hola(int a, NuevaClase nc){

        }
    }
}