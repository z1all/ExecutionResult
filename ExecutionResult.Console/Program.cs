// See https://aka.ms/new-console-template for more information
using ExecutionResult;

Console.WriteLine("Hello, World!");


ExecutionResult<A> a = new(); 

if (a.TryGetResult(out A? o))
{
    Console.WriteLine(o.ToString());
}
else
{
    Console.WriteLine(o.ToString());
}


public class A
{

}

