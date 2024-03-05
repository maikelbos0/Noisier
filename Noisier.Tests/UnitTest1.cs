namespace Noisier.Tests {
    public class UnitTest1 {
        [Fact]
        public void Test1() {
            var x = new WaveCreator();

            x.Create(@"C:\Temp\test.wav");
        }
    }
}