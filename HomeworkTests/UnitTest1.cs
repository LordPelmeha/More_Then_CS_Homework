using NUnit.Framework.Internal;
using static Homework2.Program;
using static Homework9.BinarySearchTree;
using static Homework9.TreeUtils;
using Homework9;
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


        [Test]
        public void TestLeafSum()
        {
            Assert.That(0, Is.EqualTo(LeafSum(null)));
            Assert.That(6, Is.EqualTo(LeafSum(GetSampleIntTree1())));
            Assert.That(-57, Is.EqualTo(LeafSum(GetSampleIntTree3())));
        }

        [Test]
        public void TestLevelWidth()
        {
            Assert.That(0, Is.EqualTo(LevelWidth(null, 5)));
            Assert.That(3, Is.EqualTo(LevelWidth(GetSampleIntTree1(), 2)));
            Assert.That(1, Is.EqualTo(LevelWidth(GetSampleIntTree3(), 1)));
        }

        [Test]
        public void TestIsTreeSum()
        {
            Assert.That(false, Is.EqualTo(IsTreeSum(GetSampleIntTree1())));
            Assert.That(true, Is.EqualTo(IsTreeSum(new TreeNode<int>(5))));
            Assert.That(true, Is.EqualTo(IsTreeSum(
                new TreeNode<int>(26,
                new TreeNode<int>(10,
                    new TreeNode<int>(4),
                    new TreeNode<int>(6)
                ),
                new TreeNode<int>(3,
                    right: new TreeNode<int>(3)
                )
            ))));
        }

        [Test]
        public void TestMin()
        {
            Assert.That(int.MaxValue, Is.EqualTo(Min(null)));
            Assert.That(-6, Is.EqualTo(Min(GetSampleIntTree1())));
            Assert.That(-1001, Is.EqualTo(Min(GetSampleIntTree3())));
        }

        [Test]
        public void TestMax()
        {
            Assert.That(int.MinValue, Is.EqualTo(Max(null)));
            Assert.That(32, Is.EqualTo(Max(GetSampleIntTree1())));
            Assert.That(999, Is.EqualTo(Max(GetSampleIntTree3())));
        }

        [Test]
        public void TestSumNMinTreeNum()
        {
            Assert.That(0, Is.EqualTo(SumNMinTreeNum(null, 2)));
            Assert.That(-11, Is.EqualTo(SumNMinTreeNum(GetSampleIntTree1(), 3)));
            Assert.That(-1058, Is.EqualTo(SumNMinTreeNum(GetSampleIntTree3(), 2)));
        }

        [Test]
        public void TestToSortedArray()
        {
            Assert.That(new int[] { }, Is.EqualTo(ToSortedArray(null)));
            Assert.That(new int[] { -6, -5, 0, 7, 11, 32 }, Is.EqualTo(ToSortedArray(GetSampleIntTree1())));
            Assert.That(new int[] { -1001, -57, 0, 999 }, Is.EqualTo(ToSortedArray(GetSampleIntTree3())));
        }
    }
}