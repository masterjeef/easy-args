![alt text](https://raw.githubusercontent.com/masterjeef/easy-args/master/easy-arg-icon.png "Easy Args")

# Easy Args

The lightest .Net utility for processing command line arguments

[![Build status](https://ci.appveyor.com/api/projects/status/w7vwd418k6ltur9k?svg=true)](https://ci.appveyor.com/project/masterjeef/easy-args)

Run the following in your package manager to install the package.

    Install-Package EasyArgs

## Named Arguments

Let's use the following command as an example. Application would be the exe that you are executing.

    Application Email=git@er.dun

In our application, we would use EasyArgs like the following :

    static void Main(string[] args)
    {

        var easyArgs = new Args(args);

        var email = easyArgs["Email"];

    }

The number of arguments, and the order of the arguments does not matter. Also, accessing named arguments and flags is not case sensitive. The following would also work for the example above.

      var email = easyArgs["email"];

## Object Initialization Works Too

Another example using object Initialization :

    Application Username=Iateyourcookie

Within our application :

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        var email = easyArgs["Username"];

    }

## Flags

EasyArgs also supports flags, take the following command for example :

    Application ChickensName=MotherClucker -d

The `-d` flag can be placed anywhere in the command and must be prepended with `-`

How to detect the presence of a flag in the code :

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        var hasFlag = easyArgs.HasFlag("d");

    }

## Handling Types

Types currently supported :

* int
* double
* decimal
* bool
* DateTime

Another example :

    Application -d KidneyCount=3

How to parse an integer :

    static void Main(string[] args)
    {

        var easyArgs = new Args
        {
            Arguments = args
        };

        int kidneyCount = easyArgs["KidneyCount"];
    }

EasyArgs utilizes implicit typing to parse the value to the requested type. If the string cannot be parsed to the requested type, then an exception will be thrown.

## Beyond Main

`Main(string[] args)` is not be the only place where we can use Easy Args. We can also do the following :

    var command = "Hello=World -d KidneyCount=3 Username=Iateyourcookie Email=git@er.dun";

    var args = new Args(command);

    string username = args["Username"];

## Default Arguments

By default EasyArgs will return null or default for a missing argument, another example.

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