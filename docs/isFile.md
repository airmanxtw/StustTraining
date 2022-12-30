# is image  
```csharp
public static bool isImage(byte[] source)
{
    try
    {
        if (source[0] == 255 && source[1] == 216 && source[source.Length - 2] == 255 && source[source.Length - 1] == 217)
        {
            //jpeg
            return true;
        }
        else if (source[0] == 137 && source[1] == 80  && source[2] == 78 && source[3] == 71 && source[4] == 13 && source[5] == 10 && source[6] == 26 && source[7] == 10)
        {
            //png
            return true;
        }
        else if (source[0] == 66 && source[1] == 77)
        {
            //bmp
            return true;
        }
        else
        {
            return false;
        }
    }
    catch (Exception)
    {
        return false;
    }
}
```

# is PDF  
```csharp
public static bool isPDF(byte[] source)
{
    try
    {
        if (source[0] == 37 && source[1] == 80 && source[2] == 68 && source[3] == 70)
        {
           return true;
        }
        else
        {
            return false;
        }
    }
    catch (Exception)
    {
        return false;
    }
}
```