# Easy Args

Easy utility to pull and parse command line arguments in a console application.

A nuget package is available, simply run the following in your package manager.

    Install-Package EasyArgs

## Named Arguments

Let's use the following command as an example.

    Application Email=git@er.dun

In our application, we would use EasyArgs like the following :

    static void Main(string[] args)
    {

        var easyArgs = new Args(args);

        var email = easyArgs["Email"].Value;

    }

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

        var email = easyArgs["Username"].Value;

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

## Parsing Named Arguments

Types currently supported :

* int _(AsInt())_
* double _(AsDouble())_
* decimal _(AsDecimal())_
* bool _(AsBool())_
* DateTime _(AsDateTime())_

Another example

    Application Email=git@er.dun -d KidneyCount=3

How to parse an integer

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        var kidneyCount = easyArgs["KidneyCount"].AsInt();
    }

## Default Arguments

By default EasyArgs will return null for a missing argument, let's use this command again.

    Application Hello=World

The default value can also be set like the following.

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args,
            Default = "default"
        };

        var shouldBeDefault = easyArgs["GoodBye"];
    }
