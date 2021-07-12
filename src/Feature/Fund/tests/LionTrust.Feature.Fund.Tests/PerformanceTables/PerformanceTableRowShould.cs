namespace LionTrust.Feature.Fund.Tests.PerformanceTables
{
    using LionTrust.Feature.Fund.PerformanceTables;
    using NUnit.Framework;    

    [TestFixture]
    public class PerformanceTableRowShould
    {
        [Test]
        public void CreateAnObjectWithANameAndColumns()
        {
            var target = new PerformanceTableRow("test row", new string[] { "1", "2", "6" });
            Assert.IsNotEmpty(target.Name);
            Assert.IsNotEmpty(target.Columns);
        }
    }
}
