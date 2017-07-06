using System;
namespace lol{
public class Shape  
{  
    public Shape(){}
    public float Width;  
    public float Height;  
    public void ShowDim()  
    {  
        Console.WriteLine("Width and height are " +  
        this.Width + " and " + this.Height);  
    }  
}   
}