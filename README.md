# Easy Args

Easy utility to pull and parse command line arguments in a console application.

A nuget package is available, simply run the following in your package manager.

    Install-Package EasyArgs

## Named Arguments

Let's use the following command as an example
    
    Application MinPrice=200000 MaxPrice=300000

In our application, we would use EasyArgs like the following :

    static void Main(string[] args)
    {
    
        var easyArgs = new Args(args);

        var minPrice = easyArgs["MinPrice"];

        var maxPrice = easyArgs["MaxPrice"];
        
    }

## Flags

EasyArgs also supports flags, take the following command for example

    Application MinPrice=200000 MaxPrice=300000 -d
    
The `-d` flag can be placed anywhere in the command and must be prepended with `-`

How to detect flags in the code
    
    static void Main(string[] args)
    {
    
        var easyArgs = new Args(args);

        var hasFlag = easyArgs.HasFlag("d");
        
    }
