using System;
using System.Linq;


namespace Clones
{
    class Program
    {
        private static void Main()
        {
            CloneVersionSystem_should test = new CloneVersionSystem_should();

            test.CloneBasic();
            test.CloneLearned();
            test.ExecuteSample();
            test.Learn();
            test.LearnClone_DontChangeOriginal();
            test.RelearnAfterRollback();
            test.RollbackClone_DontChangeOriginal();
            test.RollbackToBasic();
            test.RollbackToPreviousProgram();


            //var n = (Console.ReadLine() ?? "")
            //    .Split().Select(int.Parse).First();
            //var clones = new CloneVersionSystem();
            //for (int i = 0; i < n; i++)
            //{
            //    var query = Console.ReadLine();
            //    if (query == null) continue;
            //    var result = clones.Execute(query);
            //    if (result != null)
            //        Console.WriteLine(result);
            //}
        }
    }
}
