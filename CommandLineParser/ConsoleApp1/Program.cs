using System;
using CommandLine;

namespace ConsoleApp1
{
    internal class Program
    {
        // General repository options that are reused for multiple verbs
        public class RepositoryOptions
        {
            [Option('u', "user", Required = true, HelpText = "User")]
            public string User { get; set; }

            [Option('p', "password", Required = true, HelpText = "Password")]
            public string Password { get; set; }
        }

        [Verb("create", HelpText = "Create a new Person.")]
        public sealed class CreateOptions : RepositoryOptions
        {
            [Option('f', "firstname", Required = true, HelpText = "First Name")]
            public string FirstName { get; set; }
        }

        [Verb("update", HelpText = "Update an existing person.")]
        public sealed class UpdateOptions : RepositoryOptions
        {
            [Option('i', "id", Required = true, HelpText = "Person ID")]
            public string ID { get; set; }

            [Option('f', "firstname", Required = false, HelpText = "First Name")]
            public string FirstName { get; set; }
        }

        [Verb("delete", HelpText = "Delete an existing person.")]
        public sealed class DeleteOptions : RepositoryOptions
        {
            [Option('i', "id", Required = true, HelpText = "Person ID")]
            public string ID { get; set; }
        }

        static void Main(string[] args)
        {
            // Testing by overriding as an alternative to providing arguments under debug settings
            args = "create --user john --password secret --firstname Hugo".Split();

            Parser.Default.ParseArguments<CreateOptions, UpdateOptions, DeleteOptions>(args)
                .WithParsed<CreateOptions>(options => { Console.WriteLine($"Create called, options: {options.FirstName}"); })
                .WithParsed<UpdateOptions>(options => { Console.WriteLine("Update called"); })
                .WithParsed<DeleteOptions>(options => { Console.WriteLine("Delete called"); })
                .WithNotParsed(errors => { Console.WriteLine("Invalid arguments"); });
        }

        /* Alternativ måde at gøre det på ifølge manualen - det virker også
        static int Main(string[] args) =>
            Parser.Default.ParseArguments<CreateOptions, UpdateOptions, DeleteOptions>(args)
                .MapResult(
                    (CreateOptions options) => RunAddAndReturnExitCode(options),
                    (UpdateOptions options) => RunCommitAndReturnExitCode(options),
                    (DeleteOptions options) => RunCloneAndReturnExitCode(options),
                    errors => 1);

        private static int RunCloneAndReturnExitCode(DeleteOptions options)
        {
            System.Console.WriteLine("Satan");
            return 0;
        }

        private static int RunCommitAndReturnExitCode(object options)
        {
            System.Console.WriteLine("Helvede");
            return 0;
        }

        private static int RunAddAndReturnExitCode(object options)
        {
            System.Console.WriteLine("Fanden");
            return 0;
        }
        */

        /* Virker også, men hvordan opererer man med flere verbs
        public static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<CreateOptions>(args)
                .WithParsedAsync(RunAsync1);

            //await Parser.Default.ParseArguments<UpdateOptions>(args)
            //    .WithParsedAsync(RunAsync2);

            System.Console.WriteLine($"Exit code= {Environment.ExitCode}");
        }

        static async Task RunAsync1(CreateOptions options)
        {
            System.Console.WriteLine("Before Task in RunAsync1");
            await Task.Delay(20); //simulate async method 
            System.Console.WriteLine("After Task in RunAsync1");
        }

        static async Task RunAsync2(UpdateOptions options)
        {
            System.Console.WriteLine("Before Task in RunAsync2");
            await Task.Delay(20); //simulate async method 
            System.Console.WriteLine("After Task in RunAsync2");
        }
        */

        /* Det her virker, men hvordan igen . hvordan fanden opererer vi med flere verbs
        public static async Task Main(string[] args)
        {
            var retValue = await Parser.Default.ParseArguments<CreateOptions>(args)
                .MapResult(RunAndReturnExitCodeAsync1, _ => Task.FromResult(1));

            System.Console.WriteLine($"retValue= {retValue}");
        }
        static async Task<int> RunAndReturnExitCodeAsync1(CreateOptions options)
        {
            System.Console.WriteLine("Before Task (Create)");
            await Task.Delay(20); //simulate async method
            System.Console.WriteLine("After Task (Create)");
            return 0;
        }
        */

        /*
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<CreateOptions>(args)
                .MapResult(
                    options => RunAndReturnExitCode(options),
                    _ => 1);
        }

        static int RunAndReturnExitCode(CreateOptions options)
        {
            //options.Dump();
            return 0;
        }
        */
    }
}