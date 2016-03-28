# Easy Args

The easiest utility to pull and parse command line arguments.

[![Build status](https://ci.appveyor.com/api/projects/status/w7vwd418k6ltur9k?svg=true)](https://ci.appveyor.com/project/masterjeef/easy-args)

A nuget package is available, simply run the following in your package manager.

    Install-Package EasyArgs

## Named Arguments

Let's use the following command as an example.

    Application Email=git@er.dun

In our application, we would use EasyArgs like the following :

    static void Main(string[] args)
    {

        var easyArgs = new Args(args);

        var email = easyArgs["Email"];

    }

The number of arguments, and the order of the arguments does not matter. Also, accessing named arguments and flags is not case sensitive. The following would have also worked for the example above.

      var email = easyArgs["email"];

## Object Initialization Works Too

Another example with object Initialization

    Application Username=Iateyourcookie

In our application, we would use EasyArgs like the following :

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        var email = easyArgs["Username"];

    }

## Flags

EasyArgs also supports flags, take the following command for example

    Application ChickensName=MotherClucker -d

The `-d` flag can be placed anywhere in the command and must be prepended with `-`

How to detect flags in the code :

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        var hasFlag = easyArgs.HasFlag("d");

    }

## Types

Types currently supported :

* int
* double
* decimal
* bool
* DateTime

Another example

    Application -d KidneyCount=3

How to parse an integer

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        int kidneyCount = easyArgs["KidneyCount"];
    }

## Default Arguments

By default EasyArgs will return null for a missing argument, another example.

    Application Hello=World

The default value can also be set like the following.

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args,
            Default = "default"
        };
        
        // returns "default" in this case
        var shouldBeDefault = easyArgs["GoodBye"];
    }