using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            // This version works if you just want to add to a bag, but ultimately doesn't solve the parallelism problem
            //Parallel.ForEach(items, i =>
            //{
            //    bag.Add(i);
            //});
            //var list = bag.ToList();
            //return list;

            // This version works, but you can't make it look pretty
            //return (await Task.WhenAll(items.Select((item) => Task.Run(() => item)))).ToList();

            // This is basically the above version, but looks a little nicer (though I'm still not a big fan of the sql-looking syntax)
            //return (await Task.WhenAll(
            //    from item in items
            //    select Task.Run(() => item))).ToList();

            // This is using that classic ForEachAsync extension method, defined in the Extensions class
            // This is the most scalable version of this
            // Every single time I go for parallel processing of this type, I end up looking at ForEachAsync
            // I just decided to implement it this time
            await items.ForEachAsync(20, (item) => Task.Run(() => bag.Add(item)));
            return bag.ToList();
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            // This was my instinctual answer.
            //var po = new ParallelOptions { MaxDegreeOfParallelism = 3 };
            //Parallel.ForEach(itemsToInitialize, po, (item) =>
            //{
            //    concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
            //});

            var partitions = Partitioner.Create(itemsToInitialize).GetPartitions(3);
            var partEnum = partitions.GetEnumerator();
            var threads = Enumerable.Range(0, 3)
                .Select(i => new Thread(() =>
                {
                    var p = partitions[i];
                    while (p.MoveNext())
                    {
                        concurrentDictionary.AddOrUpdate(p.Current, getItem, (_, s) => s);
                    }
                    //foreach (var item in itemsToInitialize)
                    //{
                    //    concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    //}
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}