using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;

namespace TextNovelReader.TextTools;

public class Paginator(string text, double width, double height, double fontSize)
{
    public double PageHeight()
    {
        var result = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        return result - 80.0; 
    }

    public List<string> PaginateText()
    {
        var pages = new List<string>();
        var start = 0; 
    }

    private int MeasureMaxLength(int start)
    {
        int low = 1;
        int high = text.Length - start;
        int best = 1; 

        while (low <= high)
        {
            int mid = (low + high) / 2;
            var subString = text.Substring(start, mid);
            TextOptions s;
        }
    }

}
