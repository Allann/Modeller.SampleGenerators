﻿using System.Text;

internal static class Extensions
{
    internal static StringBuilder i(this StringBuilder sb, int value, int spaces = 4)
    {
        a(sb,new string(' ', value * spaces));
        return sb;
    }

    internal static StringBuilder a(this StringBuilder sb, string line)
    {
        sb.Append(line);
        return sb;
    }
    internal static StringBuilder al(this StringBuilder sb, string line)
    {
        sb.AppendLine(line);
        return sb;
    }
    internal static StringBuilder b(this StringBuilder sb)
    {
        sb.AppendLine();
        return sb;
    }
}
