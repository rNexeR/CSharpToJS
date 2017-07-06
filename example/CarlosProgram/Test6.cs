using System;
using lol;
namespace otronamespace{

public enum Styles 
{
	Isoceles,
	Right
}

public interface ITriangle
{
	float Area();
}

class Triangle : lol.Shape, ITriangle
{  
	public Triangle(){}
    public Styles Style; 
	
    public float Area()  
    {  
        return base.Width * base.Height / 2;  
    }  
	
	public int Prueba()
	{
		return 0;
	}
	
      
    public void ShowStyle()  
    {  
		if(this.Style == Styles.Isoceles){
			Console.WriteLine("Triangle is " + " Isoceles");
		}else if(this.Style == Styles.Right){
			Console.WriteLine("Triangle is "  + " Right");
		}
          
    }  
}  
}