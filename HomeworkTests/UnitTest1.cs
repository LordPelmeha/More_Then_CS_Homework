using NUnit.Framework.Internal;
using static Homework2.Program;
namespace HomeworkTests

{
    public class Tests
    {
        [Test]
        public void TestMasFilter()
        {
            Assert.That(MasFilter(new int[] { 2, 4 }, x => x % 2 == 1), Is.EqualTo(new int[] { }));
            Assert.That(MasFilter(new int[] { }, x => x < 0), Is.EqualTo(new int[] { }));
            Assert.That(MasFilter(new int[] { 1, 3, 2, -2, 4, 0, 5 }, x => x % 2 == 0), Is.EqualTo(new int[] { 2, -2, 4, 0 }));
        }
        [Test]
        public void TestSwapParts()
        {
            int[] s1 = new int[] { 1, 2, 3, 4 };
            SwapParts(ref s1);
            Assert.That(s1, Is.EqualTo(new int[] { 3, 4, 1, 2 }));
            int[] s2 = new int[] { };
            SwapParts(ref s2);
            Assert.That(s2, Is.EqualTo(new int[] { }));
        }
        [Test]
        public void TestMaxRowSum()
        {
            Assert.That(MaxRowSum(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5, 6 },
                new int[]{ 7, 8, 9 },
                new int[]{ 1, 4, 7 } }), Is.EqualTo((3, 24)));
            Assert.That(MaxRowSum(new int[][] { }), Is.EqualTo((0, 0)));
            Assert.That(MaxRowSum(null), Is.EqualTo((0, 0)));
            Assert.That(MaxRowSum(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 1, 2, 3 },
                new int[]{ 1, 2, 3 },
                new int[]{ 1, 2, 3 } }), Is.EqualTo((1, 6)));
        }
        [Test]
        public void TestMaxAverage()
        {
            Assert.That(MaxAverage(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5 },
                new int[]{ 9 },
                new int[]{ 1, 4, 7 } }), Is.EqualTo(9));
            Assert.That(MaxAverage(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5, 6 },
                new int[]{ 7, 8 },
                new int[]{ 1, 4, 7 } }), Is.EqualTo(7.5));
            Assert.That(MaxAverage(new int[][] { }), Is.EqualTo(0));
            Assert.That(MaxAverage(null), Is.EqualTo(0));
            Assert.That(MaxAverage(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 1, 2, 3 } }), Is.EqualTo(2));
        }
    }
}