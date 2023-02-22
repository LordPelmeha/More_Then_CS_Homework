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
    }
}