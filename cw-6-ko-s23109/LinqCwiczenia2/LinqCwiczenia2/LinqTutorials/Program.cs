using System;

namespace LinqTutorials
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var res = LinqTasks.Task14();

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            
            /*
            int[] arr = { 1, 1, 1, 1, 1, 1, 10, 1, 1, 1, 1 };
            Console.WriteLine(LinqTasks.Task13(arr));
            */
        }
    }
}
