using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBench;
using WebAPI.Controllers;

namespace ProjectMgmt.UnitTest.Harness
{
    /// <summary>
    /// Summary description for APIPeformanceTest
    /// </summary>
    [TestClass]
    public class NBenchPeformanceTest
    {
        private Counter _counter;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }


        [TestMethod()]
        [Ignore]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void GetParentTasksTest()
        {
            ParentController controller = new ParentController();
            _counter.Increment();
            var result = controller.GetParentTasks();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        [Ignore]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
           RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 500)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 500)]
        [CounterMeasurement("TestCounter")]
        public void GetTaskControllerTest()
        {
            TaskController controller = new TaskController();
            _counter.Increment();
            var result = controller.GetTasks();
            Assert.IsNotNull(result);
        }


        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
