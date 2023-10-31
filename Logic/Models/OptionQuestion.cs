using System.Collections.Generic;

namespace Logic.Models;

public class OptionQuestion
{
    public string Text { get; set; }
    
    public List<string> Options { get; set; }
    
    public int RightOptionNumber { get; set; }
}