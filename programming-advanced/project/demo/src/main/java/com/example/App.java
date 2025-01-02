package com.example;
import picocli.CommandLine;
/**
 * Application name : Vulnerable Target
 */
public class App 
{
    public static void main( String[] args )
    {
        System.out.println(
        "\n  @@@@@@@@&###&@@@@@@@\n" +
        "  @@@@@@@@&###&@@@@@@@\n" +
        "  @@@@@@@#^...7@@@@@@@\n" +
        "  @@@@@@@&:   !@@@@@@@\n" +
        "  @@@@@@@@BGGGB?777P@@\n" +
        "  @@@@@@@@@@@@#    !@@\n" +
        "  @@&BBBB&BBBB#!^^^5@@\n" +
        "  @@J   .?.   ?:.. ?@@\n" +
        "  @@Y ..:Y....J:...?@@\n" +
        "  @@&####&####&####&@@\n" +
        "  @@ Happy  Hacking @@\n"
);
        // System.out.println( "Hello World!" );
        CommandLine commandLine = new CommandLine(new CLI());
        commandLine.execute(args);
    }
}

