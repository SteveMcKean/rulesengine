namespace RulesEngine;

public class Measurement
{
    public double Length { get; set; }
    public double Width { get; set; }
    
    public double Height { get; set; }
    
    public double Weight { get; set; }
    
    public Measurement(double length, double width, double height, double weight, bool adjustLength)
    {
        if(adjustLength && width > length)
        {
            Length = width;
            Width = length;
        }
        else
        {
            Length = length;
            Width = width;
        }
        
        Height = height;
        Weight = weight;
        
    }
    
}